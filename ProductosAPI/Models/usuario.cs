namespace ProductosAPI.Models
{
    public class usuario
    {
        
            public int Id { get; set; } // Clave primaria
            public string Nombre { get; set; } // Nombre del usuario
            public string Apellido { get; set; } // Apellido del usuario
            public string Email { get; set; } // Correo electrónico del usuario
            public string Telefono { get; set; } // Teléfono del usuario (opcional)
            public string Direccion { get; set; } // Dirección del usuario (opcional)
        
    }
}
