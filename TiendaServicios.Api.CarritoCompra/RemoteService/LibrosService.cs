using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteModel;
using TiendaServicios.Api.Libro.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibrosServices
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote Libro, string ErrorMesage)> GetLibro(Guid LibroId)
        {
            try
            {
                //esto viene del servicio agregado en el startup 
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync(); //esto es un json
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    
                    //pasamos los datos del json a la clase dto que creamos
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
