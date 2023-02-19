using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class RoleCrud
{
    public const string TableName = "role";
    private static Database dB = new Database();

    

    public static Role GetRolebyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Role role = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return role;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new Role();
    }

    public static Role[] GetAllRoles()
    {
        List<Role> roleList = new List<Role>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        Role role = ReadManagementSystem(reader);
                        roleList.Add(role);

                    }
                dB.Disconnect();
                return roleList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return roleList.ToArray();
    }
    

 

    public static int AddRole(string typeOfRole)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(TypeOfRole) " +
                $"VALUES (@typeOfRole)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("typeOfRole", typeOfRole);
                


                Console.WriteLine("Inserted New Role");
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

    public static int ReEnterRole(int id, string role)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  TypeOfRole = @typeOfRole 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("typeOfRole", role);
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
    

    public static int DeleteRole(int id)
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

    public static Role ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        string? typeOfRole = reader["typeOfRole"] as string;

        Role role = new Role
        {
            ID = id.Value,
            TypeOfRole = typeOfRole,
            
        };
       
        return role;
    }
}