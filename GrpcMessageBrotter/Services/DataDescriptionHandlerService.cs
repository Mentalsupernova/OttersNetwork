using System.Data;
using Grpc.Core;
using GrpcMessageBrotter.Model;
using Microsoft.Data.Sqlite;

namespace GrpcMessageBrotter.Services;

public class DataDescriptionHandlerService : DataDescripionHandler.DataDescripionHandlerBase

{
    public override Task<Empty> UploadMessages(MessageUrlRecordStream request, ServerCallContext context)
    {
        foreach (var record in request.Records)
        {
            var urlRecord = new UrlRecord(record.Url,record.Description,record.KeyWords,record.WebsiteType,record.Mood,record.ColorScheme);
            urlRecord.UpdateData(); 
        }
        return Task.FromResult(new Empty()
        {
            
        });
    }

    public override Task<Empty> UploadImage(MessageUrlRecordImage request, ServerCallContext context)
    {
        
using (var connection = new SqliteConnection("Data Source=/Users/utsu/RiderProjects/OttersNetwork/GrpcMessageBrotter/dataset_db.db"))
        {
            connection.Open();

            // Create a parameterized insert command
            using (var insertCommand = new SqliteCommand("UPDATE UrlRecord SET RecordImage = @RecordImage WHERE RecordUrl = @RecordUrl", connection))
            {
                // Set the parameter values
                insertCommand.Parameters.AddWithValue("@RecordUrl", request.Url);

                // Read the image data from a file
                byte[] imageData = request.Images.ToByteArray();

                // Set the image parameter as a BLOB
                insertCommand.Parameters.Add("@RecordImage", (SqliteType)DbType.Binary, imageData.Length).Value = imageData;

                // Execute the command
                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Record inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Error inserting record.");
                }
            }
        }
        return Task.FromResult(new Empty()
        {
            
        });
        
    }
}


