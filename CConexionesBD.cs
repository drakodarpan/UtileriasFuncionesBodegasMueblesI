using System;
using System.Data.Odbc;
using System.Windows.Forms;

namespace UtileriasFuncionesBodegasMueblesI
{
	public class CConexionesBD
	{
		public CConexionesBD ()
		{
		}

		/// <summary>
		/// Funtion para conectar a SQL.
		/// </summary>
		/// <returns>
		/// Conexion a SQL.
		/// </returns>
		/// <param name='dg'>
		/// Parametros para realizar la conexion
		/// </param>
		/// <param name='odbcSQL'>
		/// ODBC, metodo por el cual se va a realizar la conexion
		/// </param>
		public static bool fAbrirConexionSQL(ConexionComprasMuebles dg, ref OdbcConnection odbcSQL)
		{
			bool bRegresa = false;
			string sCadenaConexion = "Driver={SQL Server}; database="+dg.BaseDeDatos.Trim()+";server="+dg.Ip.Trim()+";uid="+dg.Usuario.Trim()+";pwd="+dg.Pass.Trim();

			try{
				odbcSQL.ConnectionString = sCadenaConexion;
				odbcSQL.ConnectionTimeout = 1000000000;
				odbcSQL.Open();
				bRegresa = true;
			}
			catch(OdbcException oex)
			{
				MessageBox.Show("Se presento un error al abrir la conexión:\n"+oex.Message.ToString(),"Error ODBC", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch(Exception ex)
			{
				MessageBox.Show("No se pudo realizar la conexión Base de Datos [ "+ dg.BaseDeDatos.Trim() +" ]\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			return bRegresa;
		}
	}
}

