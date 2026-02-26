using PRUEBA_TECNICA_SETI.Models;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PRUEBA_TECNICA_SETI.Services
{
    public class PedidoSoapService
    {
        private readonly HttpClient _httpClient;
        private const string SoapEndpoint = "https://smb2b095807450.free.beeceptor.com";

        public PedidoSoapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<EnviarPedidoResponse> EnviarPedidoAsync(EnviarPedidoRequest pedido)
        {
    
            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" 
                  xmlns:env=""http://WSDLs/EnvioPedidos/EnvioPedidosAcme"">
   <soapenv:Header/>
   <soapenv:Body>
      <env:EnvioPedidoAcme>
         <EnvioPedidoRequest>
            <pedido>{EscapeXml(pedido.numPedido)}</pedido>
            <Cantidad>{EscapeXml(pedido.cantidadPedido)}</Cantidad>
            <EAN>{EscapeXml(pedido.codigoEAN)}</EAN>
            <Producto>{EscapeXml(pedido.nombreProducto)}</Producto>
            <Cedula>{EscapeXml(pedido.numDocumento)}</Cedula>
            <Direccion>{EscapeXml(pedido.direccion)}</Direccion>
         </EnvioPedidoRequest>
      </env:EnvioPedidoAcme>
   </soapenv:Body>
</soapenv:Envelope>";


            var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://WSDLs/EnvioPedidos/EnvioPedidosAcme/EnvioPedidoAcme");

            var response = await _httpClient.PostAsync(SoapEndpoint, content);

            response.EnsureSuccessStatusCode();

            string xmlResponse = await response.Content.ReadAsStringAsync();


            return ParseSoapResponse(xmlResponse);
        }

        private static EnviarPedidoResponse ParseSoapResponse(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";

            var codigo = doc.Descendants(env + "Codigo").FirstOrDefault()?.Value;
            var mensaje = doc.Descendants(env + "Mensaje").FirstOrDefault()?.Value;

            return new EnviarPedidoResponse
            {
                codigoEnvio = codigo,
                estado = mensaje
            };
        }

        private static string EscapeXml(string? value)
            => value is null ? "" : XmlConvert.EncodeName(value); 
    }

}