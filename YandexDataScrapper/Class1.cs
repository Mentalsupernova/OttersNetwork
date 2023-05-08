using System.Net;
using System.Net.Http.Json;

namespace YandexDataScrapper;

// oauth y0_AgAAAAAxqjDdAAnZswAAAADiWdzZFvQ9ozFGQ96DYdtn7QVOp9xfE54

public class YDavDataScrapper
{
   protected String OAuthToken;
   private HttpClient client;

   public YDavDataScrapper(string token)
   {
       OAuthToken = token;
       client = new HttpClient();
       PerformHeader();
   }

   public async void GetFileAsync(string FilePath)
   {
       var request = HttpUtilities.CreateRequest(this.accessToken, path);
       request.Method = WebdavResources.PropfindMethod;
       var requestState = new RequestState { Request = request, RequestArgument = WebdavResources.ItemDetailsBody, ResponseArgument = path };
       HttpUtilities.SendFullRequest(requestState, this.ProcessGetListResponse); 
   }
   
   

   private void PerformHeader()
   {
        client.DefaultRequestHeaders.Clear();    
        client.DefaultRequestHeaders.Add("Host","webdav.yandex.ru");
        client.DefaultRequestHeaders.Add("Accept","*/*");
        client.DefaultRequestHeaders.Add("Authorization", "OAuth y0_AgAAAAAxqjDdAAnZswAAAADiWdzZFvQ9ozFGQ96DYdtn7QVOp9xfE54");
   }
}