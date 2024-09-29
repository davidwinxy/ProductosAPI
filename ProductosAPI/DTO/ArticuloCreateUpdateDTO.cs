namespace ProductosAPI.DTO
{
    public class ArticuloCreateUpdateDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public bool Disponibilidad { get; set; }
        // No incluir el campo "imagenes"
    }
}
