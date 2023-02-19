using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {

        [HttpGet("Role/{id}", Name = "GetRoleById")]
        public IActionResult GetRoleById(int id)
        {

            return new ObjectResult(RoleCrud.GetRolebyId(id));
        }

        [HttpGet("AllRoles",Name = "GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            
            return new ObjectResult(RoleCrud.GetAllRoles());
        }

        [HttpPut("UpdateRole{role}", Name = "UpdateRole")]
        public int UpdateRole(int id,string role)
        {

            return RoleCrud.ReEnterRole(id,role);
        }


        [HttpPost("AddRole", Name = "AddRole")]
        public int AddRole(string role)
        {
           
            return RoleCrud.AddRole(role);
        } 

        [HttpDelete("DeleteRole{id}", Name = "DeleteRole")]
        public int DeletingRole(int id)
        {

            return RoleCrud.DeleteRole(id);
        }
    }

}