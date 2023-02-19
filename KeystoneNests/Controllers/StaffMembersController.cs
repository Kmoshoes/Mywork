using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffMembersController : Controller
    {

        [HttpGet("StaffMembers/{id}", Name = "GetStaffMembers")]
        public IActionResult GetStaffMembers(int id)
        {

            return new ObjectResult(StaffMembersCrud.GetStaffMemberbyId(id));
        }

        [HttpGet("AllStaffMembers", Name = "GetAllStaffMembers")]
        public IActionResult GetStaffMembers()
        {
            
            return new ObjectResult(StaffMembersCrud.GetAllStaffMembers());
        }

        [HttpPut("UdpateStaffMemeberRole{id},{roleId}", Name = "UdpateStaffMemeberRole")]
        public int UdpateStaffMemeberRole(int id,int roleId)
        {

            return StaffMembersCrud.UpdateStaffMemeberRole(id,roleId);
        }


        [HttpPost("AddStaffMember{userId},{roleId}", Name = "AddStaffMember")]
        public int AddStaffMember(int userId, int roleId)
        {
           
            return StaffMembersCrud.AddStaffMember(userId,roleId);
        } 

        [HttpPost("RemoveStaffMember{id}", Name = "RemoveStaffMembers")]
        public int RemoveStaffMembers(int id, bool employed)
        {

            return StaffMembersCrud.RemoveStaffMember(id,employed);
        }

        [HttpDelete("DeleteStaffMember{id}", Name = "DeleteStaffMember")]
        public int DeletingUser(int id)
        {

            return StaffMembersCrud.DeleteStaffMember(id);
        }
    }

}