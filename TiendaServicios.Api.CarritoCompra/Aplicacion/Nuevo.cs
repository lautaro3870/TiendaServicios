using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejectuta : IRequest
        {
            public DateTime FcehaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejectuta>
        {
            private readonly CarritoContexto _contexto;

            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejectuta request, CancellationToken cancellationToken)
            {
                //primero creamos la sesion
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FcehaCreacionSesion
                };

                await _contexto.CarritoSesion.AddAsync(carritoSesion);
                var valor = await _contexto.SaveChangesAsync();

                if (valor == 0)
                {
                    throw new Exception("No se pudo insertar");
                }

                //id de la sesion
                int id = carritoSesion.CarritoSesionId;

                foreach (var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        CarritoSesionId = id,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = obj
                    };

                    await _contexto.CarritoSesionDetalle.AddAsync(detalleSesion);
                }

                valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle");

            }
        }

    }
}
