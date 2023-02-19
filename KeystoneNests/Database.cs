using Npgsql;

namespace Keystonenest;
internal class Database
    {
        private NpgsqlConnection connection;

        private string connectionString = @"Server=localhost;Port=1632;User Id=postgres;Password=1234;Database=Keystonenests;";

        

    public Database()
        {
            connection = new NpgsqlConnection(connectionString);
            
        }

        public NpgsqlConnection GetConnection()
        {
        connection.Open();
        Console.WriteLine("Connection Opened");
        return connection;
        }

        public void Disconnect() { 
        connection.Close();
        Console.WriteLine("Connection Closed");
        }
    }

