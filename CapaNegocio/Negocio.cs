using SalvarDatos.CapaDatos;
using SalvarDatos.CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvarDatos.CapaNegocio
{
    class Negocio
    {
        private string stringConnection = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source= C:\\ProgramData\\Tecnausa\\Gran Bolsa\\Bolsa.mdb ; Jet OLEDB:Database Password = N68H98A30";
        private string stringConnSave = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source= C:\\ProgramData\\Tecnausa\\Gran Bolsa\\totales.accdb ";
        AccesoDB accesBD = new();
        public void Start(Entidad En)
        {
            accesBD.GetConection(En, stringConnection);
            if (En.ErrorCode == 0)
            {
                accesBD.getDiario(En);
            }
            Console.WriteLine(En.ErrorMsg);
        }
        public void Save (Entidad En)
        {
            accesBD.GetConection(En, stringConnSave);
            if(En.ErrorCode==0)
            {
                accesBD.InserData(En);
            }
            Console.WriteLine(En.ErrorMsg);
        }

    }
}
