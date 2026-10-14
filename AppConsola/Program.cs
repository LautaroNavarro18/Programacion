using AccesoDatos;
using AccesoDatos.Entidades;
using AccesoDatos.Repositorios;
using Microsoft.EntityFrameworkCore;

CafeteriaContext context = new CafeteriaContext();

context.Database.Migrate();

ICategoriaRepositorio categoriaRepositorio = new CategoriaRepositorio(context);
IProductoRepositorio productoRepositorio = new ProductoRepositorio(context);

int opcion;

do
{
    Console.WriteLine();
    Console.WriteLine(" CAFETERÍA ");
    Console.WriteLine("1. Alta categoría");
    Console.WriteLine("2. Alta producto");
    Console.WriteLine("3. Ver productos");
    Console.WriteLine("4. Mostrar productos más caros");
    Console.WriteLine("5. Cantidad total de productos");
    Console.WriteLine("6. Mostrar productos ordenados alfabéticamente por nombre");
    Console.WriteLine("7. Verificar si existen productos registrados");
    Console.WriteLine("0. Salir");
    Console.Write("Elija una opción: ");
    opcion = int.Parse(Console.ReadLine());
    Console.WriteLine();

    switch (opcion)
    {
        case 1:
            AltaCategoria();
            break;
        case 2:
            AltaProducto();
            break;
        case 3:
            MostrarProductos(productoRepositorio.ObtenerTodos());
            break;
        case 4:
            Console.WriteLine("Productos más caros:");
            MostrarProductos(productoRepositorio.ObtenerMasCaros());
            break;
        case 5:
            Console.WriteLine($"Cantidad total de productos: {productoRepositorio.ContarProductos()}");
            break;
        case 6:
            MostrarProductos(productoRepositorio.ObtenerOrdenadosPorNombre());
            break;
        case 7:
            if (productoRepositorio.ExistenProductos())
            {
                Console.WriteLine("Sí, hay productos registrados.");
            }
            else
            {
                Console.WriteLine("No hay productos registrados.");
            }
            break;
        case 0:
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
} while (opcion != 0);


void AltaCategoria()
{
    Console.Write("Nombre de la categoría: ");
    string nombre = Console.ReadLine() ?? "";

    if (nombre == "")
    {
        Console.WriteLine("El nombre no puede estar vacío.");
        return;
    }

    Categoria categoria = new Categoria();
    categoria.Nombre = nombre;
    categoriaRepositorio.Agregar(categoria);

    Console.WriteLine("Categoría registrada.");
}

void AltaProducto()
{
    List<Categoria> categorias = categoriaRepositorio.ObtenerTodas();

    if (categorias.Count == 0)
    {
        Console.WriteLine("Primero tiene que registrar una categoría.");
        return;
    }

    Console.Write("Nombre del producto: ");
    string nombre = Console.ReadLine() ?? "";

    if (nombre == "")
    {
        Console.WriteLine("El nombre no puede estar vacío.");
        return;
    }

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio) || precio <= 0)
    {
        Console.WriteLine("Precio inválido.");
        return;
    }

    Console.WriteLine("Categorías:");
    foreach (Categoria c in categorias)
    {
        Console.WriteLine($"{c.Id}. {c.Nombre}");
    }

    Console.Write("Id de la categoría: ");
    if (!int.TryParse(Console.ReadLine(), out int categoriaId) || categoriaRepositorio.ObtenerPorId(categoriaId) == null)
    {
        Console.WriteLine("La categoría no existe.");
        return;
    }

    Producto producto = new Producto();
    producto.Nombre = nombre;
    producto.Precio = precio;
    producto.CategoriaId = categoriaId;
    productoRepositorio.Agregar(producto);

    Console.WriteLine("Producto registrado.");
}

void MostrarProductos(List<Producto> productos)
{
    if (productos.Count == 0)
    {
        Console.WriteLine("No hay productos registrados.");
        return;
    }

    foreach (Producto p in productos)
    {
        Console.WriteLine($"{p.Id}. {p.Nombre} - ${p.Precio} - Categoría: {p.Categoria?.Nombre}");
    }
}
