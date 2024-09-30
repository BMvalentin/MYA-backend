namespace MYABackend.Models;
public class Producto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int IdMarca { get; set; }
    public int IdCategoria { get; set; }
    public int IdTalle { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string RutaImagen { get; set; }
    public string DireccionActual { get; set; }
    public IFormFile File { get; set; }
}