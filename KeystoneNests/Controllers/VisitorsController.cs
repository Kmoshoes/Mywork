using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Keystonenest.CRUD;
using Keystonenest.Model;

namespace Keystonenest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitorController : Controller
    {

        [HttpGet("Visitors/{id}", Name = "GetVisitor")]
        public IActionResult Get(int id)
        {

            return new ObjectResult(VisitorCrud.GetVisitorbyId(id));
        }

        [HttpGet("AllVisitors", Name = "GetAllVisitors")]
        public IActionResult GetVisitors()
        {

            return new ObjectResult(VisitorCrud.GetAllVisitors());
        }




        [HttpPost("AddVisitor", Name = "AddVisitor")]
        public int AddVisitor(int userId,int tenantId)
        {
            DateTime dateOfVisit = DateTime.Now.Date;
            TimeSpan TimeIn = DateTime.Now.TimeOfDay;

            return VisitorCrud.AddVisitors(userId,tenantId,dateOfVisit,TimeIn);
        }

        [HttpPost("addTimeOut{id}", Name = "addTimeOut")]
        public int RemoveVisitor(int id)
        {
            TimeSpan TimeOut = DateTime.Now.TimeOfDay;

            return VisitorCrud.UpdateTimeOut(id,TimeOut);
        }

        [HttpDelete("HardDeleteVisitor{id}", Name = "DeleteVisitor")]
        public int DeletingVisitor(int id)
        {

            return VisitorCrud.DeleteVisitor(id);
        }
    }

}