using System;

namespace lab_1_POE_U20200218.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DUI { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Cargo { get; set; }
    }
}
