namespace AppBackend.Models
{
    public class AgregarProductoDto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
