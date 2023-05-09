using System.Data;
using Grpc.Core;
using GrpcMessageBrotter.Model;
using GrpcMessageBrotter.Private;
using Microsoft.Data.Sqlite;
using Npgsql;

namespace GrpcMessageBrotter.Services;

public class DataDescriptionHandler : DataDescripionHandler.DataDescripionHandlerBase
{
    public override Task<Empty> UploadMessages(MessageUrlRecordStream request, ServerCallContext context)
    {
        foreach (var record in request.Records)
        {
            var urlRecord = new UrlRecord(record.Url,record.Description,record.KeyWords,record.WebsiteType,record.Mood,record.ColorScheme,0,0);
            urlRecord.UpdateData(); 
        }
        return Task.FromResult(new Empty()
        {
            
        });
    }

    public override async Task<MessageDataChunk> GetDataChunk(Empty request, ServerCallContext context)
    {
        var res = new MessageDataChunk();
using (var connection =new NpgsqlConnection(Config.cs))
        {
            connection.Open();

            // Get the total number of rows in the table
            using (var countCommand = new NpgsqlCommand("SELECT COUNT(*) FROM UrlRecord", connection))
            {
                long rowCount = Convert.ToInt64(countCommand.ExecuteScalar());

                // Create a parameterized select command
                using (var selectCommand = new NpgsqlCommand("SELECT * FROM UrlRecord", connection))
                {
                    // If there are more than 100 rows, limit the results to 100
                    if (rowCount > 100)
                    {
                        selectCommand.CommandText += " LIMIT 100";
                    }

                    
                    // Execute the command and read the results
                    var reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {

                        if (!Convert.ToBoolean(Convert.ToInt32(reader["RecordImageProcessed"])) && !Convert.ToBoolean(Convert.ToInt32(reader["RecordTaken"])) )
                        {
                            Console.WriteLine("Writing Chunk");
                        var record = new MessageToDownloadRecord();
                                                     record.RecordId = Convert.ToInt32(reader["RecordId"]);
                                                     record.Url = (string)reader["RecordUrl"];
                                                     UpdateTakenStatus(record.Url, 1);
                                                     
                                                     
                                res.Records.Add(record);
                            
                        }
                                              
                        
                    }
                            // Loop through the records and do something with them
                            /*
                            while (reader.Read())
                            {
                                
                                Console.WriteLine("Writing Chunk");
                                var record = new MessageToDownloadRecord();
                                record.RecordId = reader.GetInt32(0);
                                record.Url = reader.GetString(1);
                                UpdateTakenStatus(record.Url, 1);
                                
                                res.Records.Add(record);
                                    


                            }
                            */
                        
                }
            }
        } 
        return res;
    }

    private async Task UpdateTakenStatus(string url,int status)
    {
using (var connection =new NpgsqlConnection(Config.cs))
        {
            connection.Open();

            // Create a parameterized insert command
            using (var insertCommand =
                   new NpgsqlCommand("UPDATE UrlRecord SET RecordTaken = @RecordTaken  WHERE RecordUrl = @RecordUrl",
                       connection))
            {
                // Set the parameter values
                insertCommand.Parameters.AddWithValue("@RecordUrl", url);

                // Read the image data from a file

                // Set the image parameter as a BLOB
                insertCommand.Parameters.AddWithValue("@RecordTaken", 1);

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
    }
    public override Task<Empty> UploadImage(MessageUrlRecordImage request, ServerCallContext context)
    {
        Console.WriteLine(234);
using (var connection =new NpgsqlConnection(Config.cs))
        {
            connection.Open();

            // Create a parameterized insert command
            using (var insertCommand = new NpgsqlCommand("UPDATE UrlRecord SET RecordImage = @RecordImage,RecordImageProcessed =@RecordImageProcessed  WHERE RecordUrl = @RecordUrl", connection))
            {
                // Set the parameter values
                insertCommand.Parameters.AddWithValue("@RecordUrl", request.Url);

                // Read the image data from a file
                byte[] imageData = request.Images.ToByteArray();

                // Set the image parameter as a BLOB
                insertCommand.Parameters.AddWithValue("@RecordImage",   imageData);
                insertCommand.Parameters.AddWithValue("@RecordImageProcessed",  1);

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


