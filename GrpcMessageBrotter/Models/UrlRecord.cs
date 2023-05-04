namespace GrpcMessageBrotter.Models;

public class UrlRecord
{
    public int chunkId { get; set; }
    public string url { get; set; }
    public string websiteType { get; set; }
    public int WebsiteInfoRecordId { get; set; }
    public WebsiteInfoRecord WebsiteInfoRecord { get; set; }
}
