using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class RequestedServiceCrud
{
    public const string TableName = "requestedservices";
    private static Database dB = new Database();

    

    public static RequestedService GetRequestedServicebyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    RequestedService requestedServices = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return requestedServices;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new RequestedService();
    }

    public static RequestedService[] GetAllRequestedServices()
    {
        List<RequestedService> requestedServicesList = new List<RequestedService>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        RequestedService requestedServices = ReadManagementSystem(reader);
                        requestedServicesList.Add(requestedServices);

                    }
                dB.Disconnect();
                return requestedServicesList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return requestedServicesList.ToArray();
    }
    

    public static string completedConvertor(bool attended)
    {
        if (attended.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";} 
    }

    public static int AddServiceRequest(int tenantsId, string description)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(TenantsId, Description,Completed) " +
                $"VALUES (@tenantsId, @description,CAST(0 AS BIT) )";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("tenantsId", tenantsId);
                cmd.Parameters.AddWithValue("description", description);


                Console.WriteLine("INSERTED NEW Service Request");
                numberOfRows = cmd.ExecuteNonQuery();
                dB.Disconnect();
                return numberOfRows;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return numberOfRows;
    }

    public static int ReEnterDescription(int id, string description)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  Description = @description 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("description", description);
                cmd.Parameters.AddWithValue("id", id);


                numberOfRows = cmd.ExecuteNonQuery();
                dB.Disconnect();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return numberOfRows;

    }
    

    public static int ServiceCompleted(int id,bool complete)

    {
        int numberOfRows = 0;
        try
        {
            string command = $@"UPDATE {TableName}
            SET  Completed = "+ completedConvertor(complete)
            + " WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("id", id);
                numberOfRows = cmd.ExecuteNonQuery();
                dB.Disconnect();
            }
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return numberOfRows;
    }

    public static int DeleteRequestService(int id)
    {
        int numberOfRows = 0;
        try
        {
            string command = $@"DELETE FROM {TableName} WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))

            {
                cmd.Parameters.AddWithValue("id", id);

                numberOfRows = cmd.ExecuteNonQuery();
                dB.Disconnect();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return numberOfRows;

    }

    public static RequestedService ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        int? tenantsId = reader["tenantsId"] as int?;
        string? description = reader["description"] as string;
        bool? completed = reader["completed"] as bool?;

        RequestedService requestedServices = new RequestedService
        {
            ID = id.Value,
            TenantsId = tenantsId,
            Description = description,
            Completed = (bool)completed
        };
       
        return requestedServices;
    }
}