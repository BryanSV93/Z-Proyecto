using System.Text.Json;
using System.Text;
using AppFrontend.Models;

namespace AppFrontend.Services
{
    public class ProductoService
    {

        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Producto>> ListarProductos()
        {
           var response = await _httpClient.GetAsync("https://localhost:7206/api/Productos/listar");
            response.EnsureSuccessStatusCode(); 

           var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
            return productos;
        }
        //TRABAJANDO00000000000000000000000//

        public async Task AgregarProducto(Producto producto)
        {
            var productoDto = new ProductoDto
            {
                Nombre = producto.Nombre,
                Description = producto.Description,
                Precio = producto.Price,
                Stock = producto.Stock
            };
            var json = JsonSerializer.Serialize(productoDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7206/api/Productos/agregar",content);
            response.EnsureSuccessStatusCode();


        }

        public async Task EditarProducto(Producto productoEditado)
        {
            var productoDto = new ProductoDto
            {
                Nombre = productoEditado.Nombre,
                Description = productoEditado.Description,
                Precio = productoEditado.Price,
                Stock = productoEditado.Stock
            };
            var json = JsonSerializer.Serialize(productoDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7206/api/Productos/editar/{productoEditado.Id}", content);
            response.EnsureSuccessStatusCode();
        }


        //TRABAJANDO00000000000000000000000//

        public async Task VenderProducto(int id)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7206/api/Productos/vender/{id}?cantidadVender=1"); //TODO: mejorar con venta dinamica donde se cambia la cantidad de prod a vender

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al vender producto: {errorMessage}");
            }

            Console.WriteLine($"Producto con ID {id} vendido exitosamente.");
        }


        public async Task<Producto> ObtenerProducto(int id)
        {
            var response = await _httpClient.GetAsync("https://localhost:7206/api/Productos/detalles/"+id);
            response.EnsureSuccessStatusCode();

            var producto = await response.Content.ReadFromJsonAsync <Producto>();
            return producto;
        }

        public async Task EliminarProducto(int id)
        {
            var response = await _httpClient.DeleteAsync("https://localhost:7206/api/Productos/eliminar/" + id);
            response.EnsureSuccessStatusCode();
        }



    }
}
