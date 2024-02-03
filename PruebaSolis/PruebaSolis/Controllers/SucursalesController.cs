using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaSolis.Context;
using PruebaSolis.Models;

namespace PruebaSolis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public SucursalesController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
        {
            return await _dbContext.Sucursales.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int Id)
        {
            var sucursal = await _dbContext.Sucursales.FindAsync(Id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal(Sucursal sucursal)
        {
            //validamos si el tipo de dato de la las propiedades del cuerpo coinciden con el tipo de dato del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.Sucursales.Add(sucursal);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

            return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.Id }, sucursal);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutSucursal(int Id, Sucursal sucursal)
        {
            //validamos si el id de la url y el del body coinciden
            if (Id != sucursal.Id)
            {
                return BadRequest();
            }
            //validamos si el tipo de dato de la las propiedades del cuerpo coinciden con el tipo de dato del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.Entry(sucursal).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(sucursal);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSucursal(int Id)
        {
            var sucursal = await _dbContext.Roles.FindAsync(Id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(sucursal);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool SucursalExists(int Id)
        {
            return _dbContext.Roles.Any(e => e.Id == Id);
        }
    }
}
