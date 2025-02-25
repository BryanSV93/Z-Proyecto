using AppBackend.Models;
using AppBackend.Services;
using Microsoft.AspNetCore.Mvc;





namespace AppBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly IInterfaceProductoService _productoService;

        public ProductosController(IInterfaceProductoService productoService)
        {
            _productoService = productoService;
        }




       
        [HttpGet("listar")]
        public async Task<IActionResult> ListarProductos()
        {
            var productos = await _productoService.ListarProductos();
            return Ok(productos);
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> MostrarDetallesProducto(int id)
        {
            var producto = await _productoService.ObtenerDetallesProducto(id);

            if (producto == null)
            {
                return NotFound($"No se encontró el producto con ID {id}");
            }

            return Ok(producto);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarProducto([FromBody] AgregarProductoDto nuevoProducto)
        {

            if (nuevoProducto == null)
            {
                return BadRequest("Producto inválido.");
            }
            try
            {
                await _productoService.AgregarProducto(nuevoProducto);
                return Ok("Producto agregado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("vender/{id}")]
        public async Task<IActionResult> EliminarProducto(int id, [FromQuery] int cantidadVender)
        {

            var producto = await _productoService.ObtenerDetallesProducto(id);

            if (producto == null)
            {
                return NotFound($"No se encontró el producto con ID {id}");
            }

            if (producto.Stock < cantidadVender)
            {
                return BadRequest("La cantidad a vender es mayor a la existente");
            }
            await _productoService.VenderProducto(id, cantidadVender);
            return Ok("Producto vendido.");
        }

        [HttpPut("editar/{id}")]

        public async Task<IActionResult> EditarProducto(int id, [FromBody] AgregarProductoDto nuevoProducto)

        {
            var producto = await _productoService.ObtenerDetallesProducto(id);

            if (producto == null)
            {
                return NotFound($"No se encontró el producto con ID {id}");
            }

            await _productoService.EditarProducto(id, nuevoProducto);

            return Ok("Producto modificado.");
        }



        //TRABAJANDOOOOOOOOOOOOO//



        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {

            var producto = await _productoService.ObtenerDetallesProducto(id);

            if (producto == null)
            {
                return NotFound($"No se encontró el producto con ID {id}");
            }

            else
            {
                await _productoService.EliminarProducto(id);
                return Ok("Producto eliminado.");
            }
    
        }


        //TRABAJANDOOOOOOOOOOOOO//

    }
}
