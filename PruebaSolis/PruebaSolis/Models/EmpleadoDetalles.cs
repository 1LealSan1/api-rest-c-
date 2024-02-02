namespace PruebaSolis.Models
{
    public class EmpleadoDetalles
    {
        public required int Id { get; set; }
        public required string NombreEmpleado { get; set; }
        public required DateOnly FechaIngreso { get; set; }
        public required string RFC { get; set; }
        public required int StatusEmpleado { get; set; }
        public required int IdRol { get; set; }
        public string nombreRol { get; set; }
        public required int IdSucursal { get; set; }
        public string nombreSucursal { get; set; }
    }
}
