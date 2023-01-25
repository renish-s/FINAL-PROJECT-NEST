using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewComplaintController : ControllerBase
    {
        public DataContextClass objDataContextClass { get; set; }

        public NewComplaintController(DataContextClass newcomplaintcontext)
        {
            this.objDataContextClass = newcomplaintcontext;
        }

        [HttpPost("insertnewcomplaint")]
        public async Task<ActionResult>InsertNewComplaint(NewComplaint nc)
        {
            objDataContextClass.complaint_tbl.Add(nc);
            await objDataContextClass.SaveChangesAsync();
            return Ok(nc);
        }

        //view complaints at the user side
        [HttpGet("getstatuscomplaint/{id}")]
        public async Task<ActionResult<NewComplaint>>GetStatusComplaint(int id)
        {
            
            var data = await objDataContextClass.complaint_tbl.Where(x => x.LoginId == id).ToArrayAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

       

        [HttpPost("updatestatuspending")]
        public async Task<ActionResult>UpdateStatusPending(NewComplaint nc)
        {
            objDataContextClass.complaint_tbl.Update(nc);
            await objDataContextClass.SaveChangesAsync();
            return Ok(nc);
          
        }

        [HttpPost("updatestatuscompleted")]
        public async Task<ActionResult> UpdateStatusCompleted(NewComplaint nc)
        {
            objDataContextClass.complaint_tbl.Update(nc);
            await objDataContextClass.SaveChangesAsync();
            return Ok(nc);

        }

        


    }
}
