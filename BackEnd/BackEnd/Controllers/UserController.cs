using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public DataContextClass objDataContextClass { get; set; }
        public UserController(DataContextClass usercontext)
        {
            this.objDataContextClass = usercontext;
        }
        [HttpPost("insertuser")]
        public async Task<ActionResult>InsertUser(User us)
        {
            objDataContextClass.tbluser.Add(us);
            await objDataContextClass.SaveChangesAsync();
            return Ok(us);
        }

        [HttpGet("viewuser")]
        public async Task<List<User>> ViewUser()
        {
            // return objDataContextClass.tbluser.Where(u => u.Status == "NotConfirmed").ToList();
            return objDataContextClass.tbluser.OrderByDescending(u => u.LoginId).Where(u => u.Status == "NotConfirmed").ToList();
        }

        [HttpGet("viewconfirmeduser")]
        public async Task<List<User>> ViewConfirmedUser()
        {
            return objDataContextClass.tbluser.ToList();
        }

        [HttpPost("confirmuser")]
        public async Task<ActionResult> ConfirmUser(User pu)
        {
            objDataContextClass.tbluser.Update(pu);
            await objDataContextClass.SaveChangesAsync();
            return Ok(pu);
        }

        [HttpPost("removeuser")]
        public async Task<ActionResult>RemoveUser(User pu)
        {
            objDataContextClass.tbluser.Update(pu);
            await objDataContextClass.SaveChangesAsync();
            return Ok(pu);
        }

        [HttpPost("login")]
        public IActionResult Login(User us)
        {
            var userAvailable = objDataContextClass.tbluser.Where(u => u.Username == us.Username && u.Password == us.Password && u.Status=="Confirmed").FirstOrDefault();
            if (userAvailable != null)
            {
                return Ok(userAvailable);
            }
            return Ok("Failed");
        }

        //to view complaints at the admin side
        [HttpGet("getcomplaints")]
        public IActionResult GetComplaints()
        {
            var result = (from C in objDataContextClass.complaint_tbl
                          join U in objDataContextClass.tbluser on C.LoginId equals U.LoginId
                          where C.Status == "NotChecked" && U.Status == "confirmed"
                          select new { C.Productname, C.Complaints, U.Name, U.Email, U.Phone, C.Status, C.ComplaintId, C.LoginId }).OrderByDescending(x => x.ComplaintId).ToList();
            return Ok(result);
        }

        [HttpGet("getpendingcomplaints")]
        public IActionResult GetPendingComplaints()
        {
            var result = (from C in objDataContextClass.complaint_tbl
                          join U in objDataContextClass.tbluser on C.LoginId equals U.LoginId
                          where C.Status == "Pending" && U.Status == "confirmed"
                          select new { C.Productname, C.Complaints, U.Name, U.Email, U.Phone, C.Status, C.ComplaintId, C.LoginId }).OrderByDescending(x => x.ComplaintId).ToList();
            return Ok(result);
        }

        [HttpGet("getcompletedcomplaints")]
        public IActionResult GetCompletedComplaints()
        {
            var result = (from C in objDataContextClass.complaint_tbl
                          join U in objDataContextClass.tbluser on C.LoginId equals U.LoginId
                          where C.Status == "Completed" && U.Status == "confirmed"
                          select new { C.Productname, C.Complaints, U.Name, U.Email, U.Phone, C.Status, C.ComplaintId, C.LoginId }).OrderByDescending(x => x.ComplaintId).ToList();
            return Ok(result);
        }

        [HttpGet("showallcomplaints")]
        public IActionResult ShowAllComplaints()
        {
            var result = (from C in objDataContextClass.complaint_tbl
                          join U in objDataContextClass.tbluser on C.LoginId equals U.LoginId
                          where U.Status == "confirmed"
                          select new { C.Productname, C.Complaints, U.Name, U.Email, U.Phone, C.Status, C.ComplaintId, C.LoginId }).OrderByDescending(x => x.ComplaintId).ToList();
            return Ok(result);
        }
        
    }
}
