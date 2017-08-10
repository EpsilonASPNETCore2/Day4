using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EComm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;

namespace EComm.WebAPI.Controllers
{
    //[Produces("application/json")]
    [Route("api/Product")] 
    public class ProductController : Controller
    {
        public DataContext EcommDataContext { get; }
        public ProductController(DataContext dataContext)
        {
            EcommDataContext = dataContext;
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get() => EcommDataContext.Products.ToList();

        // GET: api/Product/5
        [HttpGet("{id}/{format?}", Name = "Get")]
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public IActionResult Get(int id)
        {
            var product = EcommDataContext.Products
                .Include(p => p.Supplier)
                .SingleOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return new ObjectResult(product);
            //return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (ProductExists(product.Id)) return StatusCode(StatusCodes.Status409Conflict);

            try
            {
                EcommDataContext.Products.Add(product);
                EcommDataContext.SaveChanges();
            }
            catch (DbUpdateException) { return StatusCode(StatusCodes.Status409Conflict); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError); }

            return CreatedAtAction("Get", new { id = product.Id }, product);
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (product == null || id != product.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!ProductExists(id)) return NotFound();

            var existing = EcommDataContext.Products.SingleOrDefault(p => p.Id == id);

            try { 
                existing.ProductName = product.ProductName;
                existing.UnitPrice = product.UnitPrice;
                existing.Package = product.Package;
                existing.IsDiscontinued = product.IsDiscontinued;
                existing.SupplierId = product.SupplierId;
                EcommDataContext.SaveChanges();
            }
            catch (DbUpdateException) { return StatusCode(StatusCodes.Status409Conflict); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError); }
            return Ok(existing);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ProductExists(id)) return NotFound();
            var existing = EcommDataContext.Products.SingleOrDefault(p => p.Id == id);

            try
            {
                EcommDataContext.Remove(existing);
                EcommDataContext.SaveChanges();
            }
            catch (DbUpdateException) { return StatusCode(StatusCodes.Status409Conflict); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError); }

            return Ok(existing);
        }

        private bool ProductExists(int id) => EcommDataContext.Products.Count(p => p.Id == id) > 0;
    }
}
