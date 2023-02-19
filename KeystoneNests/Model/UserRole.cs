using Npgsql;

namespace Keystonenest.Model;


public class UserRole
{
    public int ID { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }


}