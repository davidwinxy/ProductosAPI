using System.Text.Json.Serialization;

namespace ProductosAPI.Models
{
    public class ImagenArticulo
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string FileName { get; set; }

        [JsonIgnore]
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }


        [JsonIgnore]
        public Articulo Articulo { get; set; }
    }
}
