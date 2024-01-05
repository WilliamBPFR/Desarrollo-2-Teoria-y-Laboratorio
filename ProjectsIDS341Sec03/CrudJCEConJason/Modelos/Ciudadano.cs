using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudJCEConJason.Modelos
{
    internal class Ciudadano
    {
        public string Name { get; set; }
        public string Apellido { get; set; } 
        public string Cedula { get; set; }
        public string Nacionalidad { get; set; }
        public string LugNacimiento { get; set; }
        public DateTime FechNaci { get; set; }
        public string EstCivil { get; set; }
        public string Sexo { get; set; }
        public string TipoSangre { get; set; }
        public string Ocupacion { get; set; }
        public string ColegioElectoral { get; set; }
        public string DireccionResi { get; set; }
        public string Sector { get; set; }
        public string Municipio { get; set; }
        public string CodPostal { get; set; }
        public string Foto { get; set; }
    }
}