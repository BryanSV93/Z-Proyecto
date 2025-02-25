using AppFrontend.Models;
using AppFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppFrontend.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpGet]
        [Route("productos/lista")] 

        public async Task<IActionResult> ListarProductos()
        {
            var productos = await _productoService.ListarProductos();
            return View("ListaProductos",productos);
        }


        [HttpGet]
        [Route("productos/AgregarProducto")]
        public IActionResult AgregarProducto()
        {
            return View("Agregar");
        }

        [HttpPost]
        [Route("productos/AgregarProducto")]
        public async Task<IActionResult> AgregarProducto(Producto productoNuevo)
        {

            try
            {
                await _productoService.AgregarProducto(productoNuevo);
                TempData["SuccessMessage"] = $"Se ha agregado el producto {productoNuevo.Nombre}";

                return RedirectToAction("AgregarProducto");

            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error inesperado al procesar al intentar agregar el producto.";
                return RedirectToAction("AgregarProducto");
            }
        }


        [HttpPost]
        public async Task<IActionResult> VenderProducto(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerProducto(id);
                if (producto == null)
                {
                    TempData["ErrorMessage"] = $"No se encontró el producto con ID {id}";
                    return RedirectToAction("ListarProductos");
                }

                if (producto.Stock <= 0)
                {
                    TempData["ErrorMessage"] = "Stock insuficiente. No hay productos disponibles.";
                    return RedirectToAction("ListarProductos");
                }

                await _productoService.VenderProducto(id);

                TempData["SuccessMessage"] = $"Venta realizada. Quedan {producto.Stock - 1} unidades.";
                return RedirectToAction("ListarProductos");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error inesperado al procesar la venta.";
                return RedirectToAction("ListarProductos");
            }
        }

        [HttpGet]
        [Route("productos/EditarProducto")]
        public async Task<IActionResult> EditarProducto(int id)
        {
            var producto = await _productoService.ObtenerProducto(id);

            if (producto == null)
            {
                TempData["ErrorMessage"] = "Producto no encontrado.";
                return RedirectToAction("Editar");
            }

            return View("Editar", producto);
        }

        [HttpPost]
        [Route("productos/EditarProducto")]
        public async Task<IActionResult> EditarProducto(Producto productoEditado)
        {

            try
            {
                await _productoService.EditarProducto(productoEditado);
                TempData["SuccessMessage"] = $"Se ha editado el producto corectamente {productoEditado.Nombre}";

                return RedirectToAction("ListarProductos");

            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error inesperado al procesar al intentar editar el producto.";
                return RedirectToAction("ListarProductos");
            }
        }

        [HttpPost]
        [Route("productos/EliminarProducto")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                await _productoService.EliminarProducto(id);
                TempData["SuccessMessage"] = "Se ha eliminado el producto corectamente";
                return RedirectToAction("ListarProductos");
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error inesperado al intentar eliminar el producto.";
                return RedirectToAction("ListarProductos");
            }
        }

    }
}
