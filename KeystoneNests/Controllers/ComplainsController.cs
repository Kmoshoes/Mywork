using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplainsController : Controller
    {

        [HttpGet("Complains/{id}", Name = "GetComplain")]
        public IActionResult GetComplainById(int id)
        {

            return new ObjectResult(ComplainsCrud.GetComplainsbyId(id));
        }

        [HttpGet("AllComplains",Name = "GetAllCompplains")]
        public IActionResult GetAllComplains()
        {
            
            return new ObjectResult(ComplainsCrud.GetAllComplains());
        }

        [HttpPut("UpdateComplain{description}", Name = "UpdateComplain")]
        public int UdpateComplain(int id,string description)
        {

            return ComplainsCrud.ReEnterDescription(id,description);
        }


        [HttpPost("AddComplain", Name = "AddComplain")]
        public int AddComplain(int tenantsId, string description)
        {
           
            return ComplainsCrud.AddComplain(tenantsId,description);
        } 

        [HttpPost("AttendComplain{id}", Name = "AttendComplain")]
        public int attendComplain(int id,bool attend)
        {

            return ComplainsCrud.ComplainAttended(id, attend);
        }

        [HttpDelete("DeleteComplain{id}", Name = "DeleteComplain")]
        public int DeletingComplain(int id)
        {

            return ComplainsCrud.DeleteComplain(id);
        }
    }

}