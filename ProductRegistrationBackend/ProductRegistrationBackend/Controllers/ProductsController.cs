using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistrationBackend.Data;
using ProductRegistrationBackend.Models;

namespace ProductRegistrationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context) 
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<ProductsModel>>> Add(ProductsModel products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductsModel>>> GetAll()
        {            
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsModel>> GetById([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }

            return Ok(product); 
        }
        
        [HttpPut]
        public async Task<ActionResult<ProductsModel>> UpdateProduct([FromBody] ProductsModel model)
        {

            if (model.Id == 0)
            {
                return BadRequest("O ID fornecido na rota não corresponde ao ID do produto no corpo da requisição.");
            }

            var existingProduct = await _context.Products.FindAsync(model.Id);

            if (existingProduct == null)
            {
                return NotFound("Produto não encontrado.");
            }

            existingProduct.Name = model.Name;
            existingProduct.Price = model.Price;
            existingProduct.Quantity = model.Quantity;
            
            try
            {
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
                return Ok(existingProduct); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o produto. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound("Produto não encontrado.");
            }

            try
            {
                _context.Products.Remove(existingProduct); 
                await _context.SaveChangesAsync(); 
                return Ok(); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir o produto. Tente novamente mais tarde.");
            }
        }
    }
}
