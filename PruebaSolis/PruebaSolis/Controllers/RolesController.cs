using Microsoft.AspNetCore.Mvc;
using PruebaSolis.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PruebaSolis.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var rol = await _dbContext.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _dbContext.Roles.Add(rol);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRol), new { id = rol.Id }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(rol).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var person = await _dbContext.Roles.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(person);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _dbContext.Roles.Any(e => e.Id == id);
        }
    }
}
