using APIServer.Model;
using Microsoft.AspNetCore.Mvc;
using APIServer.DtContext.Data;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Controllers
{

    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
       
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product product)
        {
            if(ModelState.IsValid)
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromServices] DataContext context, [FromRoute] int id)
        {
            var prod = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            context.Products.Remove(prod);
            await context.SaveChangesAsync();

            return Ok("Produto deletad!");
            
        }

        
    }
}
