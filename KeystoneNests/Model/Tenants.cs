using Npgsql;

namespace Keystonenest.Model;


public class Tenants
{
    public int? ID { get; set; }
    public int? UserId { get; set; }

    public string? HouseAddress { get; set; }
    public bool? Deleted { get; set; }

}