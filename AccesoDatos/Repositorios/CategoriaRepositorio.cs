using AccesoDatos.Entidades;

namespace AccesoDatos.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly CafeteriaContext _context;

        public CategoriaRepositorio(CafeteriaContext context)
        {
            _context = context;
        }

        public void Agregar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public List<Categoria> ObtenerTodas()
        {
            return _context.Categorias.ToList();
        }

        public Categoria? ObtenerPorId(int id)
        {
            return _context.Categorias.Find(id);
        }
    }
}
