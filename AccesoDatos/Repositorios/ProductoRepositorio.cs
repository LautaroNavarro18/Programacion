using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly CafeteriaContext _context;

        public ProductoRepositorio(CafeteriaContext context)
        {
            _context = context;
        }

        public void Agregar(Producto producto)
        {
            _context.Productos
                .Add(producto);
            _context.SaveChanges();
        }

        public List<Producto> ObtenerTodos()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .ToList();
        }

        public List<Producto> ObtenerMasCaros()
        {
           List<Producto> productos = ObtenerTodos();

            if (productos.Count == 0)
            {
                return productos;
            }

            decimal precioMaximo = productos.Max(p => p.Precio);
            return productos
                .Where(p => p.Precio == precioMaximo)
                .ToList();
        }

        public int ContarProductos()
        {
            return _context.Productos.Count();
        }

        public List<Producto> ObtenerOrdenadosPorNombre()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public bool ExistenProductos()
        {
            return _context.Productos
                .Any();
        }
    }
}
