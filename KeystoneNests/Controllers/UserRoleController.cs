using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : Controller
    {

        [HttpGet("UserRole/{id}", Name = "GetUserRoleById")]
        public IActionResult GetUserRoleById(int id)
        {

            return new ObjectResult(UserRoleCrud.GetUserRolebyId(id));
        }

        [HttpGet("AllUserRoles", Name = "GetAllUserRoles")]
        public IActionResult GetAllUserRoles()
        {
            
            return new ObjectResult(UserRoleCrud.GetAllUserRoles());
        }

        [HttpPut("UpdateUserRole{id},{userId},{userRole}", Name = "UpdateUserRole")]
        public int UpdateUser(int id,int userId, int userRole)
        {

            return UserRoleCrud.ReEnterUserRole(id,userId,userRole);
        }


        [HttpPost("AddUserRole", Name = "AddUserRole")]
        public int AddUserRole(int userId, int roleId)
        {
           
            return UserRoleCrud.AddUserRole(userId,roleId);
        } 

        [HttpDelete("DeleteUserRole{id}", Name = "DeleteUserRole")]
        public int DeletingUserRole(int id)
        {

            return UserRoleCrud.DeleteUserRole(id);
        }
    }

}