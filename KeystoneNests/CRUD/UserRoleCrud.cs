using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class UserRoleCrud
{
    public const string TableName = "userrole";
    private static Database dB = new Database();

    

    public static UserRole GetUserRolebyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    UserRole userRole = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return userRole;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new UserRole();
    }

    public static UserRole[] GetAllUserRoles()
    {
        List<UserRole> userRoleList = new List<UserRole>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        UserRole userRole = ReadManagementSystem(reader);
                        userRoleList.Add(userRole);

                    }
                dB.Disconnect();
                return userRoleList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return userRoleList.ToArray();
    }
    

 

    public static int AddUserRole(int userId,int roleId)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(UserId , RoleId) " +
                $"VALUES (@userId,@roleId)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("roleId", roleId);



                Console.WriteLine("Inserted New User Role");
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

    public static int ReEnterUserRole(int id,int userId,int roleId)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  UserId = @userId , RoleId = @roleId
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("roleId", roleId);


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
    

    public static int DeleteUserRole(int id)
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

    public static UserRole ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        int? userId = reader["userId"] as int?;
        int? roleId = reader["roleId"] as int?;


        UserRole userRole = new UserRole
        {
            ID = id.Value,
            UserId = userId.Value,
            RoleId = roleId.Value
            
        };
       
        return userRole;
    }
}