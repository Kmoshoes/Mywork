using Npgsql;

namespace Keystonenest.Model;


public class StaffMembers
{
    public int? ID { get; set; }
    public int? UserId { get; set; }

    public int? RoleId { get; set; }
    public bool? Employed { get; set; }

}