using AccesoDatos.Entidades;

namespace AccesoDatos.Repositorios
{
    public interface IProductoRepositorio
    {
        void Agregar(Producto producto);
        List<Producto> ObtenerTodos();
        List<Producto> ObtenerMasCaros();
        int ContarProductos();
        List<Producto> ObtenerOrdenadosPorNombre();
        bool ExistenProductos();
    }
}
