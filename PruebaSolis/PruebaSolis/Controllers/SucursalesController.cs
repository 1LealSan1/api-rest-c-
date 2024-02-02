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
            _dbContext.Sucursales.Add(sucursal);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.Id }, sucursal);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutSucursal(int Id, Sucursal sucursal)
        {
            if (Id != sucursal.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(sucursal).State = EntityState.Modified;

            try
            {
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
