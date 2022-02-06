using SalvarDatos.CapaEntidad;
using SalvarDatos.CapaNegocio;
using System;
using System.Threading.Tasks;

namespace SalvarDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            Negocio Ng = new();
            Negocio BySave = new();
            Entidad En = new();
            Ng.Start(En);
            BySave.Save(En);
        }
    }
}
