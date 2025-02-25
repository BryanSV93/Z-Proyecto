using AppBackend.Data;
using AppBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppBackend.Services
{
    public class ProductoService : IInterfaceProductoService
    {
        private readonly ApplicationDBContext _context;
        public ProductoService(ApplicationDBContext context)
        {
            _context = context;
        }



      
        public async Task<List<ProductoSinDescripcionDto>> ListarProductos()
        {
            var productos = await _context.ProductoSinDescripcionDto
        .FromSqlRaw("EXEC ListarProductos")
        .ToListAsync();
            return productos;
        }

        public async Task VenderProducto(int id, int cantidadVender)
        {
            var idParam = new SqlParameter("@Id", id);
            var cantidadVenderParam = new SqlParameter("@cantidadVender", cantidadVender);

            await _context.Database.ExecuteSqlRawAsync("EXEC VenderProducto @Id, @cantidadVender", idParam, cantidadVenderParam);
        }

        public async Task <Producto> ObtenerDetallesProducto(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            var producto = await _context.Set<Producto>()
                .FromSqlRaw("EXEC ObtenerDetallesProducto @Id", idParam)
                .AsNoTracking()
                .ToListAsync();

            return producto.FirstOrDefault(); // Evita el error de composición
        }

        public async Task AgregarProducto(AgregarProductoDto nuevoProducto)
        {

            var existeProducto = await _context.Productos
            .AnyAsync(p => p.Nombre == nuevoProducto.Nombre);

            if (existeProducto)
            {
                throw new Exception("Ya existe un producto con este nombre.");
            }


            var nombreParam = new SqlParameter("@Nombre", nuevoProducto.Nombre);
            var precioParam = new SqlParameter("@Precio", nuevoProducto.Precio);
            var descripcionParam = new SqlParameter("@Description", nuevoProducto.Description);
            var stockParam = new SqlParameter("@stock", nuevoProducto.Stock);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AgregarProducto @Nombre, @Precio, @Description, @stock",
                nombreParam, precioParam, descripcionParam, stockParam);
        }

        public async Task EditarProducto(int id, AgregarProductoDto nuevoProducto)
        {
            var idParam = new SqlParameter("@Id", id);
            var nombreParam = new SqlParameter("@Nombre", nuevoProducto.Nombre);
            var precioParam = new SqlParameter("@Precio", nuevoProducto.Precio);
            var descripcionParam = new SqlParameter("@Description", nuevoProducto.Description);
            var stockParam = new SqlParameter("@stock", nuevoProducto.Stock);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditarProducto @Id, @Nombre, @Precio, @Description, @stock",
                idParam, nombreParam, precioParam, descripcionParam, stockParam
            );
        }
        //TRABAJANDOOOOOOOOOOOOO//

        public async Task EliminarProducto(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarProducto @Id", idParam);
        }



        //TRABAJANDOOOOOOOOOOOOO//

    }
}
