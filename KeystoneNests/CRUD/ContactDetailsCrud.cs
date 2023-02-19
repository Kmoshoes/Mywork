using Microsoft.VisualBasic;
using Npgsql;
using Keystonenest.Model;

namespace Keystonenest.CRUD;


public class ContactDetailsCrud
{
    public const string TableName = "contactdetails";
    private static Database dB = new Database();

    

    public static ContactDetails GetContactDetailsbyId(int id)
    {
    try { 
        string command = $"SELECT * FROM {TableName} WHERE ID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ContactDetails contact = ReadManagementSystem(reader);
                    dB.Disconnect();
                    return contact;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
        return new ContactDetails();
    }

    public static ContactDetails[] GetAllContactDetails()
    {
        List<ContactDetails> ContactDetailsList = new List<ContactDetails>();

        try
        {
            string command = $"SELECT * FROM {TableName}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        ContactDetails contactDetails = ReadManagementSystem(reader);
                        ContactDetailsList.Add(contactDetails);

                    }
                dB.Disconnect();
                return ContactDetailsList.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            dB.Disconnect();
        }
    return ContactDetailsList.ToArray();
    }
    

    public static string deletedConvertor(bool deleted)
    {
        if (deleted.Equals(true)) {
            return "CAST(1 AS BIT)"; }
        else {
            return "CAST(0 AS BIT)";} 
    }

    public static int AddContactDetails(int userId, string phoneNumber, string email)
    {
        int numberOfRows = 0;
        try
        {
            string command = $"INSERT INTO {TableName}(UserId, PhoneNumber,email) " +
                $"VALUES (@userId, @phoneNumber,@email)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("phoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("email", email);


                Console.WriteLine("INSERTED NEW contact");
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

    public static int UpdatePhoneNumber(int id, string phoneNumber)
    {
        int numberOfRows = 0;
        try
        {
            String command = $@"UPDATE {TableName}
            SET  Phonenumber = @phonenumber 
            WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(command, dB.GetConnection()))
            {

                cmd.Parameters.AddWithValue("phonenumber", phoneNumber);
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
    

    public static int RemoveContact(int id)

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

    public static int DeleteContact(int id)
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

    public static ContactDetails ReadManagementSystem(NpgsqlDataReader reader)
    {
        int? id = reader["id"] as int?;
        int? userId = reader["userId"] as int?;
        string? phoneNumber = reader["phoneNumber"] as string;
        string? email = reader["email"] as string;

        ContactDetails contactDetails = new ContactDetails
        {
            ID = id.Value,
            UserId = userId,
            PhoneNumber = phoneNumber,
            Email = email
        };
        
        return contactDetails;
    }
}