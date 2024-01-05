using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeGastos
{
    internal class References
    {
        public  string SourceType { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
