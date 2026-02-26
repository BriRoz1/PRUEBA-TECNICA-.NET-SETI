# Prueba Técnica - Consultor ASP.NET (.NET) - ACME Pedidos

Implementación de una **API REST** en **.NET** que actúa como intermediario (proxy) entre un cliente que envía pedidos en formato JSON y un servicio SOAP legacy.

La API recibe el pedido en JSON, lo transforma a XML SOAP según el mapeo especificado, lo envía al endpoint proporcionado, recibe la respuesta XML, la transforma de vuelta a JSON y la devuelve al cliente en el formato requerido.

## Objetivo de la prueba
Exponer una API REST con mensajería JSON que:
- Transforme JSON → XML SOAP (Request)
- Envíe al endpoint: `https://smb2b095807450.free.beeceptor.com`
- Transforme XML → JSON (Response)
- Ejecución en contenedores Docker

## Tecnologías utilizadas
- **.NET 8** (Minimal API)
- **System.Text.Json** para serialización/deserialización
- **XDocument** (LINQ to XML) para transformación y parsing de XML SOAP
- **HttpClient** para llamada al servicio SOAP
- **Docker** para contenedorización
- **Git** para versionamiento

## Estructura del proyecto
.
├── Program.cs                  # Punto de entrada y definición de endpoints
├── Models/
│   ├── EnviarPedidoRequest.cs
│   └── EnviarPedidoResponse.cs
├── Services/
│   └── PedidoSoapService.cs    # Lógica de transformación y llamada SOAP
├── Dockerfile                  # Definición del contenedor
├── README.md
└── PRUEBA TECNICA SETI.csproj  # (o el nombre real de tu csproj)
text## Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Docker (Docker Desktop recomendado en Windows/macOS)

## Ejecución local (sin Docker)

```bash
# Restaurar paquetes
dotnet restore

# Compilar y ejecutar
dotnet run
La API estará disponible en:
http://localhost:5000 (o el puerto que muestre la consola)
Probar endpoint principal (POST)
Bashcurl -X POST http://localhost:5000/api/enviar-pedido \
-H "Content-Type: application/json" \
-d '{
  "enviarPedido": {
    "numPedido": "75630275",
    "cantidadPedido": "1",
    "codigoEAN": "00110000765191002104587",
    "nombreProducto": "Armario INVAL",
    "numDocumento": "1113987400",
    "direccion": "CR 72B 45 12 APT 301"
  }
}'
Ejecución con Docker
Construir la imagen
Bashdocker build -t prueba-tecnica-seti:latest .
Ejecutar el contenedor
Bashdocker run -d -p 8080:8080 --name prueba-seti prueba-tecnica-seti:latest
La API estará disponible en:
http://localhost:8080
Probar endpoint principal (POST) desde el contenedor
Bashcurl -X POST http://localhost:8080/api/enviar-pedido \
-H "Content-Type: application/json" \
-d '{
  "enviarPedido": {
    "numPedido": "75630275",
    "cantidadPedido": "1",
    "codigoEAN": "00110000765191002104587",
    "nombreProducto": "Armario INVAL",
    "numDocumento": "1113987400",
    "direccion": "CR 72B 45 12 APT 301"
  }
}'
Respuesta esperada (cuando el mock responda con el XML correcto):
JSON{
  "enviarPedidoRespuesta": {
    "codigoEnvio": "80375472",
    "estado": "Entregado exitosamente al cliente"
  }
}
