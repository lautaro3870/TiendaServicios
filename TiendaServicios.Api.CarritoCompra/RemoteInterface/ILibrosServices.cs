using System;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.Libro.RemoteInterface
{
    public interface ILibrosServices
    {
        Task<(bool resultado, LibroRemote Libro, string ErrorMesage)> GetLibro(Guid LibroId);
    }
}
