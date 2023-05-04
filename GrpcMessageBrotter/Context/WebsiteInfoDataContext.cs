using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GrpcMessageBrotter.Context;

public class WebsiteInfoDataContext: DbContext
{
    public System.Data.Entity.DbSet<Models.UrlRecord> UrlRecords { get; set; }
    public System.Data.Entity.DbSet<Models.WebsiteDescriptionRecord> WebsiteDescriptionRecords { get; set; }
    public System.Data.Entity.DbSet<Models.WebsiteInfoRecord> WebsiteInfoRecords { get; set; }

    public WebsiteInfoDataContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("/Users/utsu/RiderProjects/OttersNetwork/GrpcMessageBrotter/dataset.db");
        
    }    
    
}