using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvarDatos.CapaDatos
{
    class SaveFile
    {
        private string insertSql;
        private DateTime hoy;
        public async Task SaveData(string data)
        {
            hoy = DateTime.Now;
            insertSql = $"INSERT INTO Diario(Fecha, Total) VALUES('{hoy}', {data})";
        }
       
    }
}
