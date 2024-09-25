namespace ProductosAPI.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public string NumeroDeFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int IdProveedor { get; set; }
    }

}
