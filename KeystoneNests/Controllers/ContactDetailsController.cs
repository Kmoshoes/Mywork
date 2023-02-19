using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactDetailsController : Controller
    {

        [HttpGet("ContactDetails/{id}", Name = "GetContactDetails")]
        public IActionResult GetContactDetailsById(int id)
        {

            return new ObjectResult(ContactDetailsCrud.GetContactDetailsbyId(id));
        }

        [HttpGet("AllContactsDetails",Name = "GetAllContactDetails")]
        public IActionResult GetContactsDetails()
        {
            
            return new ObjectResult(ContactDetailsCrud.GetAllContactDetails());
        }

        [HttpPut("UpdatePhoneNumber{phonenumber}", Name = "UpdatePhoneNumber")]
        public int UdpatePhoneNumber(int id,string phonenumber)
        {

            return ContactDetailsCrud.UpdatePhoneNumber(id,phonenumber);
        }


        [HttpPost("AddContactDetails", Name = "AddContactDetails")]
        public int AddUser(int userId, string  phoneNumber, string email)
        {
           
            return ContactDetailsCrud.AddContactDetails(userId,phoneNumber,email);
        } 

        [HttpPost("RemoveContactDetails{id}", Name = "RemoveContactDetails")]
        public int RemoveContact(int id)
        {

            return ContactDetailsCrud.RemoveContact(id);
        }

        [HttpDelete("DeleteContactDetails{id}", Name = "DeleteContactDetails")]
        public int DeletingUser(int id)
        {

            return ContactDetailsCrud.DeleteContact(id);
        }
    }

}