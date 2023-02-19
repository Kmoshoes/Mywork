using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class ComplainsCrud
{
    public const string TableName = "complains";
    private static Database dB = new Database();

    

    public static Complains GetComplainsbyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Complains complains = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return complains;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new Complains();
    }

    public static Complains[] GetAllComplains()
    {
        List<Complains> ComplainsList = new List<Complains>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        Complains complain = ReadManagementSystem(reader);
                        ComplainsList.Add(complain);

                    }
                dB.Disconnect();
                return ComplainsList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return ComplainsList.ToArray();
    }
    

    public static string attendedConvertor(bool attended)
    {
        if (attended.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";} 
    }

    public static int AddComplain(int tenantsId, string description)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(TenantsId, Description,Attended) " +
                $"VALUES (@tenantsId, @description,CAST(0 AS BIT) )";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("tenantsId", tenantsId);
                cmd.Parameters.AddWithValue("description", description);


                Console.WriteLine("INSERTED NEW Complains");
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
    

    public static int ComplainAttended(int id,bool attended)

    {
        int numberOfRows = 0;
        try
        {
            string command = $@"UPDATE {TableName}
            SET  Attended = "+ attendedConvertor(attended)
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

    public static int DeleteComplain(int id)
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

    public static Complains ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        int? tenantsId = reader["tenantsId"] as int?;
        string? description = reader["description"] as string;
        bool? attended = reader["attended"] as bool?;

        Complains complain = new Complains
        {
            ID = id.Value,
            TenantsId = tenantsId,
            Description = description,
            Attended = (bool)attended
        };
       
        return complain;
    }
}