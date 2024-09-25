public class Compra
{
    public int Id { get; set; }
    public string NumerodeFactura { get; set; }
    public DateTime Fecha { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int Cantidad { get; set; }

    // Estas propiedades son solo de lectura
    public decimal SubTotal { get; private set; } // Solo se puede establecer desde dentro de la clase
    public decimal Iva { get; private set; } // Solo se puede establecer desde dentro de la clase
    public decimal Total { get; private set; }
    
    public int IdProveedor {get; set;} // Solo se puede establecer desde dentro de la clase

    // Método para calcular los totales
    public void CalcularTotales()
    {
        SubTotal = PrecioUnitario * Cantidad;
        const decimal IVA_RATE = 0.13m; // Cambia según la tasa de IVA
        Iva = SubTotal * IVA_RATE;
        Total = SubTotal + Iva;
    }
}
