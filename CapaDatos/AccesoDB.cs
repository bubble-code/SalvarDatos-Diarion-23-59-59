using SalvarDatos.CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalvarDatos.CapaDatos
{
    class AccesoDB
    {
        private readonly string stringConnection, hoy, ConsulTotalDiario, stringConnSave;
        private OleDbConnection Con { get; set; }
        private OleDbCommand Cmd { get; set; }
        private OleDbDataAdapter Ad { get; set; }
        private readonly DataSet _DiarioDataSet = new();


        public AccesoDB()
        {  
            hoy = DateTime.Now.ToString("yyyy-MM-dd");
            ConsulTotalDiario = $"SELECT SUM(Entradas)-SUM(Salidas) AS TOTAL FROM JugadasHora  WHERE Format(modificado,'yyyy-MM-dd HH:nn:ss') >= Format('{hoy} 00:00:00','yyyy-MM-dd HH:nn:ss')";
        }

        public void GetConection(Entidad En, string stringConnection)
        {
            try
            {
                Con = new OleDbConnection(stringConnection);
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                if (Con.State == ConnectionState.Open)
                    En.ErrorCode = 0;
                else
                {
                    En.ErrorCode = 999;
                    En.ErrorMsg = "Login Fail";
                }
            }
            catch (Exception ex)
            {
                En.ErrorCode = ex.HResult;
                En.ErrorMsg = ex.Message;
            }
        }
        public void getDiario(Entidad En)
        {
            Cmd = new OleDbCommand(ConsulTotalDiario, Con);
            Ad = new OleDbDataAdapter(Cmd);
            Ad.Fill(_DiarioDataSet, "Diario");
            En.TotalDiario = _DiarioDataSet.Tables["Diario"].Rows[0].IsNull(0) ? "0,0" : _DiarioDataSet.Tables["Diario"].Rows[0]["TOTAL"].ToString();
            Thread.Sleep(6000);
        }

        public void InserData(Entidad En)
        {
            DateTime doy = DateTime.Now;
            string insertSql = $"INSERT INTO Diario (Fecha,Total) VALUES('{doy}','{En.TotalDiario}')";
            Cmd = new OleDbCommand(insertSql, Con);
            var gg = Cmd.ExecuteNonQuery();
            Thread.Sleep(6000);

        }
    }
}
