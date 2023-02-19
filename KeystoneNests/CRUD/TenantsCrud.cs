using Microsoft.VisualBasic;
using Npgsql;
using Npgsql.Internal.TypeHandlers;
using System.Text;
using Keystonenest.Model;
using static System.Net.WebRequestMethods;

namespace Keystonenest.CRUD;


public class TenantsCrud
{
    public const string TableName = "tenants";
    private static Database dB = new Database();


    public static Tenants GetTenantsbyId(int id)
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
                        Tenants tenants = ReadManagementSystem(reader);
                        dB.Disconnect();
                        return tenants;
                    }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }

        return new Tenants();
    }

    public static Tenants[] GetAllTenants()
    {
        List<Tenants> tenantsList = new List<Tenants>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        Tenants tenants = ReadManagementSystem(reader);
                        tenantsList.Add(tenants);

                    }
                dB.Disconnect();
                return tenantsList.ToArray();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message); 
            dB.Disconnect();
        }
        return tenantsList.ToArray();   

    }
    

    public static string deletedConvertor(bool deleted)
    {
        if (deleted.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";
        } 
    }

    public static int AddTenants(int userId, string houseAddress, bool deleted)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(UserId, HouseAddress,Deleted) " +
                $"VALUES (@userId, @houseaddress," + deletedConvertor(deleted) + ")";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("houseaddress", houseAddress);
                cmd.Parameters.AddWithValue("deleted", deleted);


                Console.WriteLine("INSERTED NEW Tenants");
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

    public static int UpdateAddress(int id ,string HouseAddress)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}"+
           "SET  houseaddress = @houseaddress WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("houseaddress", HouseAddress);
                cmd.Parameters.AddWithValue("id", id);


                numberOfRows = cmd.ExecuteNonQuery();
                dB.Disconnect();
            }
            return numberOfRows;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return numberOfRows;

    }
    


    public static int DeleteTenants(int id)
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
            return numberOfRows;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return numberOfRows;

    }

    public static Tenants ReadManagementSystem(NpgsqlDataReader reader)
    {


        int? id = reader["id"] as int?;
        int? userId = reader["userId"] as int?;
        string? houseAddress = reader["houseAddress"] as string;
        bool? deleted = reader["deleted"] as bool?;

        Tenants tenants = new Tenants
        {
            ID = id.Value,
            UserId = userId.Value,
            HouseAddress = houseAddress,
            Deleted = (bool) deleted
        };
        return tenants;
    }

    
}