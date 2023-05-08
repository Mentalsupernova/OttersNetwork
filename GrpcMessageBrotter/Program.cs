using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using GrpcMessageBrotter.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
using (var db =new SqliteConnection(@"Data Source=C:\Users\Две выдры\Project\OttersNetwork\GrpcMessageBrotter\dataset_db.db"))
{
    db.Open();
    FileInfo file = new FileInfo(@"C:\Users\Две выдры\Project\OttersNetwork\GrpcMessageBrotter\Context\MigrateDatabase.sql");
    string script = file.OpenText().ReadToEnd();
    var d =db.CreateCommand();
    d.CommandText = script;
    d.ExecuteNonQuery();
    
    }
// Configure the HTTP request pipeline.
app.MapGrpcService<DataDescriptionHandler>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();