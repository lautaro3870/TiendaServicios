using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        //un carrito sesion tiene muchos detalles
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}
