namespace GrpcMessageBrotter.Models;

public class WebsiteInfoRecord
{
    public int id { get; set; }
    public string url { get; set; }
    public string ImageName { get; set; }
    public List<UrlRecord> UrlRecords { get; set; }
    public List<WebsiteDescriptionRecord> WebsiteDescriptionRecords { get; set; }
}