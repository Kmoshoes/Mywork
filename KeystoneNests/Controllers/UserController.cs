using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        [HttpGet("Users/{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {

            return new ObjectResult(UserCrud.GetUserbyId(id));
        }

        [HttpGet("AllUseers",Name = "GetAllUsers")]
        public IActionResult GetUsers()
        {
            
            return new ObjectResult(UserCrud.GetAllUsers());
        }

        [HttpPut("UpdateName{name}", Name = "UpdateName")]
        public int UdpateName(int id,string name)
        {

            return UserCrud.UpdateName(id,name);
        }


        [HttpPost("AddUser", Name = "AddUser")]
        public int AddUser(string name, string  lastName, bool deleted)
        {
           
            return UserCrud.AddUser(name,lastName,deleted);
        } 

        [HttpPost("RemoveUser{id}", Name = "RemoveUser")]
        public int RemoveUser(int id, User user)
        {

            return UserCrud.RemoveUser(id);
        }

        [HttpDelete("DeleteUser{id}", Name = "DeleteUser")]
        public int DeletingUser(int id)
        {

            return UserCrud.DeleteUser(id);
        }
    }

}