using AppBackend.Models;

namespace AppBackend.Services
{
    public interface IInterfaceProductoService
    {
        Task<List<ProductoSinDescripcionDto>> ListarProductos(); 

        Task VenderProducto(int id, int cantidadVender);

        Task<Producto> ObtenerDetallesProducto(int id);

        Task AgregarProducto(AgregarProductoDto nuevoProducto);

        Task EditarProducto(int id, AgregarProductoDto nuevoProducto);

        Task EliminarProducto(int id);

    }
}
