using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaSolis.Context;
using PruebaSolis.Models;

namespace PruebaSolis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
       
        public EmpleadosController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDetalles>>> GetEmpleados([FromQuery] string? nombre, [FromQuery] int? idRol, [FromQuery] int? idSucursal, [FromQuery] string? rfc, [FromQuery] DateTime? fechaIngreso)
        {
            var empleados = await _dbContext.EmpleadosDetalles.FromSqlInterpolated($"SELECT e.*, r.Id as RolId, r.NombreRol, s.Id as SucursalId, s.NombreSucursal FROM Empleados e INNER JOIN Roles r ON e.IdRol = r.Id INNER JOIN Sucursales s ON e.IdSucursal = s.Id WHERE ({nombre} IS NULL OR e.NombreEmpleado = {nombre}) AND ({idRol} IS NULL OR e.IdRol = {idRol}) AND ({idSucursal} IS NULL OR e.IdSucursal = {idSucursal}) AND ({rfc} IS NULL OR e.RFC = {rfc}) AND ({fechaIngreso} IS NULL OR e.FechaIngreso = {fechaIngreso})").ToListAsync();

            if (empleados == null || empleados.Count == 0)
            {
                return NotFound();
            }

            return empleados;
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int Id)
        {
            var empleado = await _dbContext.Empleados.FindAsync(Id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }


        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            _dbContext.Empleados.Add(empleado);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEmpleado(int Id, Empleado empleado)
        {
            if (Id != empleado.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(empleado).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(empleado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmpleado(int Id)
        {
            var empleado = await _dbContext.Empleados.FindAsync(Id);
            if (empleado == null)
            {
                return NotFound();
            }

            _dbContext.Empleados.Remove(empleado);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool EmpleadoExists(int Id)
        {
            return _dbContext.Empleados.Any(e => e.Id == Id);
        }
    }
}
