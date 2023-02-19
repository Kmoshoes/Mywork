using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class UserCrud
{
    public const string TableName = "users";
    private static Database dB = new Database();

    

    public static User GetUserbyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    User user = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return user;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new User();
    }

    public static User[] GetAllUsers()
    {
        List<User> UserList = new List<User>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        User user = ReadManagementSystem(reader);
                        UserList.Add(user);

                    }
                dB.Disconnect();
                return UserList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return UserList.ToArray();
    }
    

    public static string deletedConvertor(bool deleted)
    {
        if (deleted.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";} 
    }

    public static int AddUser(string name, string lastName, bool deleted)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(Name, LastName,Deleted) " +
                $"VALUES (@name, @lastName," + deletedConvertor(deleted) + ")";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("lastName", lastName);


                Console.WriteLine("INSERTED NEW USER");
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

    public static int UpdateName(int id, string name)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  Name = @name 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("name", name);
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
    

    public static int RemoveUser(int id)

    {
        int numberOfRows = 0;
        try
        {
            string command = $@"UPDATE {TableName}
            SET  Deleted = B'1' 
            WHERE id = @id";

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

    public static int DeleteUser(int id)
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

    public static User ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        string? name = reader["name"] as string;
        string? lastName = reader["lastName"] as string;
        bool? deleted = reader["deleted"] as bool?;

        User user = new User
        {
            ID = id.Value,
            Name = name,
            LastName = lastName,
            Deleted = (bool)deleted
        };
        Console.WriteLine(user);
        return user;
    }
}