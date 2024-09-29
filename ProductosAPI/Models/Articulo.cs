using System;
using System.Text.Json.Serialization;

namespace ProductosAPI.Models;

public class Articulo
{        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; } 
        public string Descripcion { get; set; } 
        public string Categoria { get; set; } 
        public bool Disponibilidad { get; set; } = true;


  
    public ICollection<ImagenArticulo> Imagenes { get; set; } = new List<ImagenArticulo>();
}
