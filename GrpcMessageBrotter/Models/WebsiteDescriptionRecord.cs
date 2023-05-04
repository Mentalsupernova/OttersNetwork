namespace GrpcMessageBrotter.Models;

public class WebsiteDescriptionRecord
{
    public int DescriptionId { get; set; }
    public string url { get; set; }
    public string description { get; set; }
    public string features { get; set; }
    public string modelVersion { get; set; }
    
    public int WebsiteInfoRecordId { get; set; }
    public WebsiteInfoRecord WebsiteInfoRecord { get; set; }
}