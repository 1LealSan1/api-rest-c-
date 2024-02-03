using Microsoft.AspNetCore.Mvc;
using PruebaSolis.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PruebaSolis.Models;
using System;

namespace PruebaSolis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public RolesController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Rol>> GetRol(int Id)
        {
            var rol = await _dbContext.Roles.FindAsync(Id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            //validamos si el tipo de dato de la las propiedades del cuerpo coinciden con el tipo de dato del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.Roles.Add(rol);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

            return CreatedAtAction(nameof(GetRol), new { id = rol.Id }, rol);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutRol(int Id, Rol rol)
        {
            //validamos si el id de la url y el del body coinciden
            if (Id != rol.Id)
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
                _dbContext.Entry(rol).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(rol);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRol(int Id)
        {
            var rol = await _dbContext.Roles.FindAsync(Id);
            if (rol == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(rol);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool RolExists(int Id)
        {
            return _dbContext.Roles.Any(e => e.Id == Id);
        }
    }
}
