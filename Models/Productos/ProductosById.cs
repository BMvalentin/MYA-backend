namespace  MYABackend.Models.Productos;
public class ProductosById
{
    public int id_producto { get; set; }
    public int id_categoria { get; set; }
    public int id_marca { get; set; }
    public string nombre { get; set; } = string.Empty;
    public decimal precio { get; set; }
    public string descripcion_producto { get; set;} = string.Empty;
    public string descripcion_marca { get; set;} = string.Empty;
    public string descripcion_categoria { get; set; } = string.Empty;
    public string url_imagen { get; set; } = string.Empty;
    public string talle { get; set; } = string.Empty;
}