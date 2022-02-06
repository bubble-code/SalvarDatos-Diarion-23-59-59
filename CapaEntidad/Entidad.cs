using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvarDatos.CapaEntidad
{
    class Entidad
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string TotalDiario { get; set; }
        public DataTable curr = new();
    }
}
