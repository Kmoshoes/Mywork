using Npgsql;

namespace Keystonenest.Model;


public class Complains
{

    public int ID { get; set; }
    public int? TenantsId { get; set; }
    public string? Description { get; set; }
    public bool? Attended { get; set; }



}