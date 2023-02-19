using Npgsql;
using Keystonenest.Model;


namespace Keystonenest.CRUD;


public class StaffMembersCrud
{
    public const string TableName = "staffmembers";
    private static Database dB = new Database();

    

    public static StaffMembers GetStaffMemberbyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                        StaffMembers user = ReadManagementSystem(reader);
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
        return new StaffMembers();
    }

    public static StaffMembers[] GetAllStaffMembers()
    {
        List<StaffMembers> staffMembersList = new List<StaffMembers>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        StaffMembers staffMembers = ReadManagementSystem(reader);
                        staffMembersList.Add(staffMembers);

                    }
                dB.Disconnect();
                return staffMembersList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return staffMembersList.ToArray();
    }
    

    public static string employmentConvertor(bool employed)
    {
        if (employed.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";} 
    }

    public static int AddStaffMember(int userId, int roleId)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(UserId, RoleId,Employed) " +
                $"VALUES (@userId, @roleId, CAST(1 AS BIT) )";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("roleId", roleId);


                Console.WriteLine("Inserted New Staff Memeber");
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

    public static int UpdateStaffMemeberRole(int id, int roleId)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  RoleId = @roleId 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("roleId", roleId);
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
    

    public static int RemoveStaffMember(int id,bool employed)

    {
        int numberOfRows = 0;
        try
        {
            string command = $@"UPDATE {TableName}
            SET  employed = "+employmentConvertor(employed)+"WHERE id = @id";

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

    public static int DeleteStaffMember(int id)
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

    public static StaffMembers ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        int? userId = reader["userId"] as int?;
        int? roleId = reader["roleId"] as int?;
        bool? employed = reader["employed"] as bool?;

        StaffMembers user = new StaffMembers
        {
            ID = id.Value,
            UserId = userId.Value,
            RoleId = roleId.Value,
            Employed = (bool)employed
        };
        Console.WriteLine(user);
        return user;
    }
}