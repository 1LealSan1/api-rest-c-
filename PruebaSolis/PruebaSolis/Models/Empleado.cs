namespace PruebaSolis.Models
{
    public class Empleado
    {
        public required int Id { get; set; }
        public required string NombreEmpleado { get; set; }
        public required DateOnly FechaIngreso { get; set; }
        public required string RFC { get; set;}
        public required bool StatusEmpleado { get; set;}
        public required int IdRol {  get; set; }
        public required int IdSucursal { get; set; }

    }
}
