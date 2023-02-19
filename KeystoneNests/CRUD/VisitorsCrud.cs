using Npgsql;
using System.Linq.Expressions;
using Keystonenest.Model;

namespace Keystonenest.CRUD;


public class VisitorCrud
{   

    public static string TableName = "visitors";
    private static Database dB = new Database();


    public static Visitors GetVisitorbyId(int id)
    {
        try
        {
            string command = $"SELECT * FROM {TableName} WHERE ID = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Visitors visitor = ReadManagementSystem(reader);
                        dB.Disconnect();
                        return visitor;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }

        return new Visitors();
    }


    public static Visitors[] GetAllVisitors()
    {
        List<Visitors> visitorsList = new List<Visitors>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        Visitors visitor = ReadManagementSystem(reader);
                        visitorsList.Add(visitor);

                    }
                 dB.Disconnect();
                 return visitorsList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return visitorsList.ToArray();
    }


    public static string deletedConvertor(bool deleted)
    {
        if (deleted.Equals(true)) {
            return "CAST(1 AS BIT)";}
        else { 
            return "CAST(0 AS BIT)";}
    }

    public static int AddVisitors(int userId,int tenantId, DateTime dateOfVisit, TimeSpan timeIn)
    {
        int numberOfRows = 0;

        try
        {
            
            string command = $"INSERT INTO {TableName}(UserId,TenantId,DateOfVisit,TimeIn) " +
                $"VALUES (@userId,@tenantId,@dateOfVisit,@timeIn)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("tenantId", tenantId);
                cmd.Parameters.AddWithValue("dateOfVisit", dateOfVisit);
                cmd.Parameters.AddWithValue("timeIn", timeIn);


                numberOfRows = cmd.ExecuteNonQuery();
                Console.WriteLine("INSERTED NEW visitor");
                dB.Disconnect();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            dB.Disconnect();
        }
        return numberOfRows;
     }

    public static int UpdateTimeOut(int id, TimeSpan TimeOut)
    {
        int numberOfRows = 0;
        try {
            String command = $@"UPDATE {TableName}
            SET  timeout = @timeout 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("timeout", TimeOut);
                cmd.Parameters.AddWithValue("id", id);


                numberOfRows = cmd.ExecuteNonQuery();
            }
            
        }
        catch(Exception ex) 
        { 
            dB.Disconnect();
            Console.WriteLine(ex.Message);
        }
        return numberOfRows;
    }

    public static int DeleteVisitor(int id)
    {
        int numberOfRows = 0;
        try
        {
            string command = $@"DELETE FROM {TableName} WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))

            {
                cmd.Parameters.AddWithValue("id", id);

                numberOfRows = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return numberOfRows;
    }

    public static void TimeOutFiller(TimeSpan timeOut)
    {
        try
        {
            if (timeOut.Equals(null))
            {
                timeOut = TimeSpan.Zero;
            }
            
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            timeOut= TimeSpan.Zero;
        }
    }

    public static Visitors ReadManagementSystem(NpgsqlDataReader reader)
    {
        
        
        int? id = reader["id"] as int?;
        int? userId = reader["userId"] as int?;
        int? tenantId = reader["tenantId"] as int?;
        DateTime? dateOfVisit = (DateTime)reader["dateOfVisit"];
        TimeSpan? timeIn = reader["timeIn"] as TimeSpan?;
        try
        {
            TimeSpan? timeOut = reader["timeOut"] as TimeSpan?;


            Visitors visitor = new Visitors
            {
                ID = id.Value,
                UserId = userId.Value,
                TenantId = tenantId.Value,
                DateOfVisit = dateOfVisit.Value,
                TimeIn = timeIn.Value,
                TimeOut = timeOut.Value,

            };
            return visitor;
        }
        catch(Exception ex)
        {

            Console.WriteLine(ex.Message);
            Visitors visitor = new Visitors
            {
                ID = id.Value,
                UserId = userId.Value,
                TenantId = tenantId.Value,
                DateOfVisit = dateOfVisit.Value,
                TimeIn = timeIn.Value,
                TimeOut = TimeSpan.Zero,

            };
            return visitor;

        }
       
    }


}