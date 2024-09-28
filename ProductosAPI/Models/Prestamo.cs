using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace ProductosAPI.Models;

public class Prestamo
{
    public int Id { get; set; }
    public int Usuario_Id { get; set; }
    public int Articulo_Id { get; set; }
    public DateTime Fecha_Prestamo { get; set; }
    public DateTime Fecha_devolucion { get; set; }
    [BindNever]
    public string Estado { get; set; } = "En Prestamo";
}
