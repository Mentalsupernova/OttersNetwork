using System.Data;
using Grpc.Core;
using GrpcMessageBrotter.Model;
using Microsoft.Data.Sqlite;

namespace GrpcMessageBrotter.Services;

public class DataDescriptionHandlerService : DataDescripionHandler

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

    public override Task<MessageDataChunk> GetDataChunk(Empty request, ServerCallContext context)
    {
        var res = new MessageDataChunk();
        using (var connection = new SqliteConnection("Data Source=/Users/utsu/RiderProjects/OttersNetwork/GrpcMessageBrotter/dataset_db.db"))
        {
            connection.Open();

            // Get the total number of rows in the table
            using (var countCommand = new SqliteCommand("SELECT COUNT(*) FROM UrlRecord", connection))
            {
                int rowCount = Convert.ToInt32(countCommand.ExecuteScalar());

                // Create a parameterized select command
                using (var selectCommand = new SqliteCommand("SELECT * FROM UrlRecord", connection))
                {
                    // If there are more than 100 rows, limit the results to 100
                    if (rowCount > 100)
                    {
                        selectCommand.CommandText += " LIMIT 100";
                    }

                    // Execute the command and read the results
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        // Check if there are any records returned
                        if (reader.HasRows)
                        {
                            // Loop through the records and do something with them
                            while (reader.Read())
                            {
                                var record = new MessageToDownloadRecord();
                                record.RecordId = reader.GetInt32(0);
                                record.Url = reader.GetString(1);
                                res.Records.Add(record);


                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                }
            }
        } 
        return Task.FromResult(res
       );
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


