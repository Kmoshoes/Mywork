using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantsController : Controller
    {

        [HttpGet("Tenants/{id}", Name = "GetTenants")]
        public IActionResult Get(int id)
        {

            return new ObjectResult(TenantsCrud.GetTenantsbyId(id));
        }

        [HttpGet("AllTenants", Name = "GetAllTenants")]
        public IActionResult GetTenants()
        {

            return new ObjectResult(TenantsCrud.GetAllTenants());
        }

        


        [HttpPost("AddTenants", Name = "AddTenants")]
        public int AddTenants(int userId, string houseaddress, bool deleted)
        {

            return TenantsCrud.AddTenants(userId, houseaddress, deleted);
        }


        [HttpDelete("HardDeleteTenants{id}", Name = "DeleteTenants")]
        public int DeletingVisitor(int id)
        {

            return TenantsCrud.DeleteTenants(id);
        }
    }

}