using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.IO;
using Google.Protobuf;
using Grpc.Net.Client;
using GrpcMessageBrotterClient;
using PuppeteerSharp;

public  class WebsiteScreenshotRecord
{
    public ByteString Screenshot { get; set; }
    public string Url { get; set; }
}

public  class ScreenShooter
{
    public  async Task<byte[]> MakeScreenshot(string url)
    {
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(
            new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync(); 
        await page.GoToAsync(url);
        var opt = new ScreenshotOptions();
        opt.FullPage = true;
        opt.Type = ScreenshotType.Png;
        var screen = await page.ScreenshotDataAsync(opt);
        return screen;
    }
}

static class Program
{
    private static int counter = 0;
    private static int TasksCounter = 0;
    private  const int TASK_LIMIT = 10;
    static async Task SendToServer(WebsiteScreenshotRecord record)
    {
        using (var channel = GrpcChannel.ForAddress("http://localhost:5128"))
        {
            var client = new DataDescripionHandler.DataDescripionHandlerClient(channel);
            var reply = await client.UploadImageAsync(
            
                new MessageUrlRecordImage() {Id = 0,RecordId = 0,Url =record.Url,Images =record.Screenshot});    //Url = record.Url,RecordId = 0,Images = record.Screenshot,Id = 0

        }

        TasksCounter--;

    }

    static async Task<List<string>> GetChunks()
    {
        List<string> urls = new List<string>();
        using (var channel = GrpcChannel.ForAddress("http://localhost:5128"))
        {
            var client = new DataDescripionHandler.DataDescripionHandlerClient(channel);
            var reply = await client.GetDataChunkAsync(new Empty());
            foreach (var record in reply.Records)
            {
                urls.Add(record.Url);
            }

            counter = reply.Records.Count;
        }
        return urls;

    }
    static async Task ProcessScreenShot(string url)
    {
        var u = new ScreenShooter();
        var t = new WebsiteScreenshotRecord();
        t.Url = url;
        var y =  await u.MakeScreenshot(t.Url);
        Console.WriteLine(y.Length);
        var bs = ByteString.CopyFrom(y);
        t.Screenshot = bs;
        await SendToServer(t);
        counter--;
        
        Console.WriteLine("LOG closing Task with : {0}",url);
        
    }

    static async Task ProcessAllChunks()
    {
        foreach (var url in await GetChunks())
        {
            //if (TasksCounter < TASK_LIMIT)
            //{
                await ProcessScreenShot(url);
                TasksCounter++;
                Console.WriteLine("LOG adding Task with : {0}",url);

            
        //}
        }
    }
    
    static async Task Main()
    {
                await ProcessAllChunks();
    }
}