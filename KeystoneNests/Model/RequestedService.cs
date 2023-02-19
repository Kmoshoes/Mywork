using Npgsql;

namespace Keystonenest.Model;


public class RequestedService
{
    public int? ID { get; set; }
    public int? TenantsId { get; set; }
    public string? Description { get; set; }
    public bool? Completed { get; set; }
}