using PRUEBA_TECNICA_SETI;
using PRUEBA_TECNICA_SETI.Models;
using PRUEBA_TECNICA_SETI.Services;
using PedidoRequestWrapper = PRUEBA_TECNICA_SETI.PedidoRequestWrapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<PedidoSoapService>();

var app = builder.Build();



app.MapPost("/api/enviar-pedido", async (PedidoRequestWrapper wrapper, PedidoSoapService service) =>
{
    var pedido = wrapper?.enviarPedido;

    if (pedido == null || string.IsNullOrWhiteSpace(pedido.numPedido))
        return Results.BadRequest("numPedido es requerido");

    try
    {
        var respuesta = await service.EnviarPedidoAsync(pedido);
        return Results.Ok(new { enviarPedidoRespuesta = respuesta });
    }
    catch (HttpRequestException ex)
    {
        return Results.StatusCode(502);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
})
.WithName("EnviarPedido");


app.Run();