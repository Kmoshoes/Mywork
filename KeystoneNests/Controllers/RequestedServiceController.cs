using Microsoft.AspNetCore.Mvc;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestedServiceController : Controller
    {

        [HttpGet("RequestedService/{id}", Name = "GetRequestedServiceById")]
        public IActionResult GetRequestedServiceById(int id)
        {

            return new ObjectResult(RequestedServiceCrud.GetRequestedServicebyId(id));
        }

        [HttpGet("AllRequestedServices", Name = "GetAllRequestedServices")]
        public IActionResult GetAllRequestedServices()
        {
            
            return new ObjectResult(RequestedServiceCrud.GetAllRequestedServices());
        }

        [HttpPut("UpdateRequestedService{description}", Name = "UpdateRequestedService")]
        public int UdpateComplain(int id,string description)
        {

            return RequestedServiceCrud.ReEnterDescription(id,description);
        }


        [HttpPost("RequestService", Name = "RequestService")]
        public int RequestService(int tenantsId, string description)
        {
           
            return RequestedServiceCrud.AddServiceRequest(tenantsId,description);
        } 

        [HttpPost("CompleteRequestedService{id}", Name = "CompleteRequestedService")]
        public int CompleteRequestedService(int id,bool completed)
        {

            return RequestedServiceCrud.ServiceCompleted(id, completed);
        }

        [HttpDelete("DeleteRequestedService{id}", Name = "DeleteRequestedService")]
        public int DeletingRequestedService(int id)
        {

            return RequestedServiceCrud.DeleteRequestService(id);
        }
    }

}