namespace ProductosAPI.Models
{
    public class ImagenArticulo
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        public Articulo Articulo { get; set; }
    }
}
