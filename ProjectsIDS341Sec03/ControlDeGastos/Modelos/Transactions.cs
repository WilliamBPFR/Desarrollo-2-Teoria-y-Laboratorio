using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeGastos.Modelos
{
    internal class Transactions
    {
        public string TypeTrans { get; set; }
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public DateTime DateOfTrans { get; set; }
        public string Concept { get; set; }
        public string Reference {get; set;}
        public float Amount { get; set; }
        public string Moneda { get; set;}
    }
}
