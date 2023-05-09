using GrpcMessageBrotter.Private;
using Microsoft.Data.Sqlite;
using Npgsql;

namespace GrpcMessageBrotter.Model;

public class UrlRecord
{
   public long RecordId { get; set; } 
   public string RecordUrl { get; set; }
   public string RecordDescription { get; set; }
   public string RecordKeyWords { get; set; }
   public string RecordWebsiteType { get; set; }
   public string RecordMood { get; set; }
   public string RecordColorScheme { get; set; }
   public byte[] RecordImage { get; set; }
   public int RecordImageProcessed { get; set; }
   public int RecordTaken { get; set; }
   
   public UrlRecord(string recordUrl, string recordDescription, string recordKeyWords, string recordWebsiteType, string recordMood, string recordColorScheme,int recordTaken,int recordImageProcessed)
   {
      RecordUrl = recordUrl;
      RecordDescription = recordDescription;
      RecordKeyWords = recordKeyWords;
      RecordWebsiteType = recordWebsiteType;
      RecordMood = recordMood;
      RecordColorScheme = recordColorScheme;
      RecordTaken = recordTaken;
      RecordImageProcessed = recordImageProcessed;

   }

   public void UpdateData()
   {
using (var connection =new NpgsqlConnection(Config.cs))
{
    connection.Open();

    // Check if the record already exists
    using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM urlrecord WHERE recordurl = @url", connection))
    {
        checkCommand.Parameters.AddWithValue("@url", this.RecordUrl);
        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (count > 0)
        {
            Console.WriteLine("Record already exists. Updating data...");
            
            // Update the record
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE UrlRecord SET RecordDescription = @RecordDescription, RecordKeyWords = @RecordKeyWords, RecordWebsiteType = @RecordWebsiteType, RecordMood = @RecordMood,RecordColorScheme = @RecordColorScheme WHERE RecordUrl = @RecordUrl";
            cmd.Parameters.AddWithValue("RecordDescription", this.RecordDescription);
            cmd.Parameters.AddWithValue("RecordKeyWords", this.RecordKeyWords);
            cmd.Parameters.AddWithValue("RecordWebsiteType", this.RecordWebsiteType);
            cmd.Parameters.AddWithValue("RecordUrl", this.RecordUrl);
            cmd.Parameters.AddWithValue("RecordMood", this.RecordMood);
            cmd.Parameters.AddWithValue("RecordColorScheme", this.RecordColorScheme);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Rows affected: {rowsAffected}");
            

        }
        else
        {
            
    using (var insertCommand = new NpgsqlCommand("INSERT INTO UrlRecord (RecordUrl, RecordDescription,RecordKeyWords,RecordWebsiteType,RecordMood,RecordColorScheme,RecordImageProcessed,RecordTaken) VALUES (@RecordUrl, @RecordDescription,@RecordKeyWords,@RecordWebsiteType,@RecordMood,@RecordColorScheme,@RecordImageProcessed,@RecordTaken)", connection))
    {
        // Set the parameter values
        insertCommand.Parameters.AddWithValue("@RecordUrl", this.RecordUrl);
        insertCommand.Parameters.AddWithValue("@RecordDescription", this.RecordDescription);
        insertCommand.Parameters.AddWithValue("@RecordKeyWords", this.RecordKeyWords);
        insertCommand.Parameters.AddWithValue("@RecordWebsiteType", this.RecordWebsiteType);
        insertCommand.Parameters.AddWithValue("@RecordMood", this.RecordMood);
        insertCommand.Parameters.AddWithValue("@RecordColorScheme", this.RecordColorScheme);
        insertCommand.Parameters.AddWithValue("@RecordTaken", this.RecordTaken);
        insertCommand.Parameters.AddWithValue("@RecordImageProcessed", this.RecordImageProcessed);
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

}   }
   
   

}
