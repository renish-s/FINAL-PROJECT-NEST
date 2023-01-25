using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public DataContextClass objDataContextClass { get; set; }

        public ProductController(DataContextClass productcontext)
        {
            this.objDataContextClass = productcontext;
        }

        [HttpPost("insertproduct")]
        public async Task<ActionResult>InsertProduct(Product pu)
        {
            objDataContextClass.tblproduct.Add(pu);
            await objDataContextClass.SaveChangesAsync();
            return Ok(pu);
        }

        [HttpGet("viewproduct")]
        public async Task<List<Product>> ViewProduct()
        {
            //return objDataContextClass.tblproduct.ToList();
            return objDataContextClass.tblproduct.OrderByDescending(x => x.ProductId).ToList();
        }

        [HttpGet("ViewProductByid/{id}")]
        public IActionResult ViewProductByid(int id)
        {
            return Ok(objDataContextClass.tblproduct.Find(id));
        }

        [HttpPost("updateproduct")]
        public async Task<ActionResult>UpdateProduct(Product pu)
        {
            objDataContextClass.tblproduct.Update(pu);
            await objDataContextClass.SaveChangesAsync();
            return Ok(pu);
        }

        [HttpPost("deleteproduct")]
        public async Task<ActionResult>DeleteProduct(Product pu)
        {
            objDataContextClass.tblproduct.Remove(pu);
            await objDataContextClass.SaveChangesAsync();
            return Ok(pu);
        }

       

    }
}
