using Microsoft.EntityFrameworkCore;
using PruebaSolis.Context;

var builder = WebApplication.CreateBuilder(args);

//obtenemos la linea de conexion de la base de datos de appsettings
var connectionString = builder.Configuration.GetConnectionString("connectionDB");

//establecemos el contexto a la bd en sql server
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
var app = builder.Build();
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


