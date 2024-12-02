using Dapper;

namespace MYABackend.Models;
public class Producto
{
    public int IdProducto { get; set; } = int.MinValue;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int IdMarca { get; set; } = int.MinValue;
    public int IdCategoria { get; set; } = int.MinValue;
    public int IdTalle { get; set; } = int.MinValue;
    public decimal Precio { get; set; } = decimal.MinValue;
    public int Stock { get; set; } = int.MinValue;
    public string RutaImagen { get; set; } = string.Empty;
    public IFormFile[] File { get; set; } = new IFormFile[0];

    public DynamicParameters crearProducto()
    {
        DynamicParameters dp = new DynamicParameters();
        dp.Add("Nombre", Nombre);
        dp.Add("Descripcion", Descripcion);
        dp.Add("IdMarca", IdMarca);
        dp.Add("IdCategoria", IdCategoria);
        dp.Add("Precio", Precio);
        dp.Add("IdTalle", IdTalle);
        dp.Add("Stock", Stock);
        dp.Add("RutaImagen", RutaImagen);

        return dp;
    }
}