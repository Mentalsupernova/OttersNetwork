using Microsoft.Data.Sqlite;

namespace GrpcMessageBrotter.Model;

public class UrlRecord
{
   public int RecordId { get; set; } 
   public string RecordUrl { get; set; }
   public string RecordDescription { get; set; }
   public string RecordKeyWords { get; set; }
   public string RecordWebsiteType { get; set; }
   public string RecordMood { get; set; }
   public string RecordColorScheme { get; set; }
   public byte[] RecordImage { get; set; }
   public bool RecordImageProcessed { get; set; }
   
   public UrlRecord(string recordUrl, string recordDescription, string recordKeyWords, string recordWebsiteType, string recordMood, string recordColorScheme)
   {
      RecordUrl = recordUrl;
      RecordDescription = recordDescription;
      RecordKeyWords = recordKeyWords;
      RecordWebsiteType = recordWebsiteType;
      RecordMood = recordMood;
      RecordColorScheme = recordColorScheme;
   }

   public void UpdateData()
   {
using (var connection = new SqliteConnection("Data Source=/Users/utsu/RiderProjects/OttersNetwork/GrpcMessageBrotter/dataset_db.db"))
{
    connection.Open();

    // Check if the record already exists
    using (var checkCommand = new SqliteCommand("SELECT COUNT(*) FROM UrlRecord WHERE RecordUrl = @url", connection))
    {
        checkCommand.Parameters.AddWithValue("@url", this.RecordUrl);
        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (count > 0)
        {
            Console.WriteLine("Record already exists. Updating data...");
            
            // Update the record
            using (var updateCommand = new SqliteCommand("UPDATE UrlRecord SET  RecordDescription = @RecordDescription,RecordKeyWords = @RecordKeyWords,RecordWebsiteType = @RecordWebsiteType,RecordMood = @RecordMood,RecordColorScheme = @RecordColorScheme, WHERE RecordUrl = @RecordUrl", connection))
            {
        updateCommand.Parameters.AddWithValue("@RecordDescription", this.RecordDescription);
        updateCommand.Parameters.AddWithValue("@RecordKeyWords", this.RecordKeyWords);
        updateCommand.Parameters.AddWithValue("@RecordWebsiteType", this.RecordWebsiteType);
        updateCommand.Parameters.AddWithValue("@RecordMood", this.RecordMood);
        updateCommand.Parameters.AddWithValue("@RecordColorScheme", this.RecordColorScheme);

                // Execute the command
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Record updated successfully.");
                }
                else
                {
                    Console.WriteLine("Error updating record.");
                }
            }

        }
        else
        {
            
    using (var insertCommand = new SqliteCommand("INSERT INTO UrlRecord (RecordUrl, RecordDescription,RecordKeyWords,RecordWebsiteType,RecordMood,RecordColorScheme) VALUES (@RecordUrl, @RecordDescription,@RecordKeyWords,@RecordWebsiteType,@RecordMood,@RecordColorScheme)", connection))
    {
        // Set the parameter values
        insertCommand.Parameters.AddWithValue("@RecordUrl", this.RecordUrl);
        insertCommand.Parameters.AddWithValue("@RecordDescription", this.RecordDescription);
        insertCommand.Parameters.AddWithValue("@RecordKeyWords", this.RecordKeyWords);
        insertCommand.Parameters.AddWithValue("@RecordWebsiteType", this.RecordWebsiteType);
        insertCommand.Parameters.AddWithValue("@RecordMood", this.RecordMood);
        insertCommand.Parameters.AddWithValue("@RecordColorScheme", this.RecordColorScheme);
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
