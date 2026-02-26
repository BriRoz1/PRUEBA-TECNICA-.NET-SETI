namespace PRUEBA_TECNICA_SETI.Models
{
    public class EnviarPedidoRequest
    {
        public string? numPedido { get; set; }
        public string? cantidadPedido { get; set; }
        public string? codigoEAN { get; set; }
        public string? nombreProducto { get; set; }
        public string? numDocumento { get; set; }
        public string? direccion { get; set; }
    }
}
