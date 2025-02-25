using AppFrontend.Models;

namespace AppFrontend.Services
{
    public interface IProductoService
    {
        Task<List<ProductosSinDescripcion>> ListarProductos();

      Task VenderProducto(int id, int cantidadVender);

        Task<Producto> ObtenerDetallesProducto(int id);

  //      Task AgregarProducto(AgregarProductoDto nuevoProducto);

   //     Task EditarProducto(int id, AgregarProductoDto nuevoProducto);

     //   Task EliminarProducto(int id);
    }
}
 