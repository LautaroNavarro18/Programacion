using AccesoDatos.Entidades;

namespace AccesoDatos.Repositorios
{
    public interface ICategoriaRepositorio
    {
        void Agregar(Categoria categoria);
        List<Categoria> ObtenerTodas();
        Categoria? ObtenerPorId(int id);
    }
}
