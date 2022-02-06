using SalvarDatos.CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvarDatos.CapaDatos
{
    class AccesoDB
    {
        private string stringConnection, hoy, ConsulTotalDiario;
        private OleDbConnection _con { get; set; }
        private OleDbCommand _cmd { get; set; }
        private OleDbDataAdapter _ad { get; set; }
        private DataSet _DiarioDataSet = new DataSet();


        public AccesoDB(Entidad En)
        {
            stringConnection = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source= C:/ProgramData/Tecnausa/Gran Bolsa ; Jet OLEDB:Database Password = N68H98A30";
            GetConection(En);
            hoy = DateTime.Now.ToString("yyyy-MM-dd");
            ConsulTotalDiario = $"SELECT SUM(Entradas)-SUM(Salidas) AS TOTAL FROM JugadasHora  WHERE Format(modificado,'yyyy-MM-dd HH:nn:ss') >= Format('{hoy} 00:00:00','yyyy-MM-dd HH:nn:ss')";
        }

        private void GetConection(Entidad En)
        {
            try
            {
                _con = new OleDbConnection(stringConnection);
                if (_con.State == ConnectionState.Closed)
                    _con.Open();
                if (_con.State == ConnectionState.Open)
                    En._ErrorCode = 0;
                else
                {
                    En._ErrorCode = 999;
                    En._ErrorMsg = "Login Fail";
                }
            }
            catch (Exception ex)
            {
                En._ErrorCode = ex.HResult;
                En._ErrorMsg = ex.Message;
            }
        }
        public void getDiario(Entidad En)
        {
            _cmd = new OleDbCommand(ConsulTotalDiario, _con);
            _ad = new OleDbDataAdapter(_cmd);
            _ad.Fill(_DiarioDataSet, "Diario");
            En._TotalDiario = _DiarioDataSet.Tables["Diario"].Rows[0].IsNull(0) ? "0,0" : _DiarioDataSet.Tables["Diario"].Rows[0]["TOTAL"].ToString();
        }
    }
}
