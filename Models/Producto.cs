using Dapper;

namespace MYABackend.Models;
public class Producto
{
    public int? IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int IdMarca { get; set; }
    public int IdCategoria { get; set; }
    public int IdTalle { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string? RutaImagen { get; set; }
    public IFormFile[] File { get; set; }

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