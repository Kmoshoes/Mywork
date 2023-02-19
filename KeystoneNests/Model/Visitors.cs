using Npgsql;

namespace Keystonenest.Model;


public class Visitors
{
    public int ID { get; set; }
    public int UserId { get; set; }

    public int TenantId { get; set; }
    public DateTime DateOfVisit { get; set; }

    public TimeSpan TimeIn { get; set; }
    public TimeSpan TimeOut { get; set; }

}