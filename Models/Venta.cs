public class Venta
{
    public int IdVenta { get; set; }
    public int IdCliente { get; set; }
    public int IdProvincia{ get; set; }
    public int IdTransaccion { get; set; }
    public int TotalProducto { get; set; }
    public decimal MontoTotal { get; set; }
    public string Contacto { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
}