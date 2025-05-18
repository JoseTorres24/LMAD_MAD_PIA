using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesData
{
    public class Reservacion
    {
        public string CodigoReservacion { get; set; }
        public string RFC_Cliente { get; set; }
        public int ID_Hotel { get; set; }
        public int ID_Habitacion { get; set; } // Nuevo campo //para savesao la habitzao
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Anticipo { get; set; }
        public float Total { get; set; } // Tenemos que agregar este campo para poder ver el total
        public int UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estatus { get; set; } //ESTO Para disque eliminar 
        public List<Servicios> Servicios { get; set; } = new List<Servicios>(); // Tenemos una elemento en la tabla que recibe caracteres lo que tenemos que obtener aqui es para poder apoyarnos
        // De aqui tengo que tener referencia con la tabla de serviciosReservacion
        public string DisplayInfo
        {
            get { return $"{CodigoReservacion} - {RFC_Cliente} - {FechaInicio:dd/MM/yyyy}"; }
        }

        public string DisplayServicios
        {
            get
            {
                if (Servicios != null && Servicios.Count > 0)
                {
                    return string.Join(", ", Servicios.Select(s => $"Servicio ID: {s.ID_Servicio}"));
                }
                return "No hay servicios reservados.";
            }
        }

    }

}
