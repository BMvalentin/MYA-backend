namespace MYABackend.Models;
public class ProductoCarrito
{
    public int id_producto { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public string? url_imagen { get; set; }
}