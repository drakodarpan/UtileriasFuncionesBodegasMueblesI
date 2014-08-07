using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data;

namespace UtileriasFuncionesBodegasMueblesI
{
	public class CFuncionesGenerales
	{
		public CFuncionesGenerales ()
		{
		}

		/// <summary>
		/// Genera un password
		/// </summary>
		/// <returns>
		/// Genera un MD5 el cual funciona como password.
		/// </returns>
		/// <param name='sUsuario'>
		/// Usuario del que se desea generar el MD5
		/// </param>
		/// <param name='sBaseDeDatos'>
		/// Base de datos a la que se desea conectar
		/// </param>
		public static string fGenerarPassword(string sUsuario,string sBaseDeDatos)
		{
			string sCadena="";
			sCadena=sUsuario+"-"+sBaseDeDatos;
			MD5 md5Hasher=MD5.Create();
			byte[] data=md5Hasher.ComputeHash(Encoding.Default.GetBytes(sCadena));
			StringBuilder sBuilder=new StringBuilder();
			for(int i=0;i<data.Length;i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}            
			return sBuilder.ToString();
		}

		/// <summary>
		/// Funcion encargada de dar formato a las cadenas
		/// </summary>
		/// <returns>
		/// Cadena formateada, ejemplo: 1,200.00.
		/// </returns>
		/// <param name='sImporte'>
		/// Importe a formatear
		/// </param>
		/// <param name='nMascara'>
		/// Mascara que se desea que tenga el importe formateado
		/// </param>
		/// <param name='sImporteFormateado'>
		/// Importe que se retorna, ya formateado
		/// </param>
		/// <param name='nDecimales'>
		/// Si se desean decimales en la función formateada
		/// </param>
		public static void fFormatearCadena(string sImporte, Int32 nMascara, ref string sImporteFormateado, Int32 nDecimales)
		{
			Int32 i = 0, nCaracteres = 0, nTotalRegistros = 0, nMascaraFinal = 0;
			char[] cImporteFormatear = new char[nMascara];
			decimal dImporte = 0;
			
			dImporte = Convert.ToDecimal(sImporte.ToString().Trim());
			if (nDecimales == 2)
			{
				sImporte = dImporte.ToString("###,###,##0.00");
			}
			else if (nDecimales == 0)
			{
				sImporte = dImporte.ToString("###,###,##0");
			}
			nTotalRegistros = sImporte.Length;
			nMascaraFinal = nMascara - nTotalRegistros;
			
			try
			{
				for (i = 0; i <= nMascaraFinal - 1; i++)
				{
					cImporteFormatear[i] = ' ';
				}
				
				for (i = nMascaraFinal; i < nMascara; i++)
				{
					cImporteFormatear[i] = sImporte[nCaracteres];
					nCaracteres++;
				}
				string sImporteFormatear = new string(cImporteFormatear);
				sImporteFormateado = sImporteFormatear;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		/// <summary>
		/// Funcion encargada de indicar si el usuario es Nacional o Importacion
		/// </summary>
		/// <returns>
		/// N o I
		/// </returns>
		/// <param name='dg'>
		/// Dg datos para la conexion
		/// </param>
		/// <param name='sTitulo'>
		/// Titulo de la aplicacion que manda llamar a la funcion
		/// </param>
		public static string ValidarNacionalImportacion(ConexionComprasMuebles dg, string sTitulo)
		{ 
			OdbcConnection odbcMaestroMuebles = new OdbcConnection();
			string sConsulta = "", sNacoImp = "";
			
			if ( CConexionesBD.fAbrirConexionSQL(dg, ref odbcMaestroMuebles))
			{
				try
				{
					// El [ proc_ConsultarNacoImp ] se encuentra en la carpeta scripts del ComprasMuebles
					sConsulta = string.Format("EXECUTE MaestroMuebles.dbo.proc_ConsultarNacoImp {0}", dg.Empleado);
					OdbcDataReader reader;
					OdbcCommand com = new OdbcCommand(sConsulta, odbcMaestroMuebles);
					
					reader = com.ExecuteReader();
					while (reader.Read())
					{
						sNacoImp = reader["NombreOpcion"].ToString().Trim().Substring(0, 1);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("El proceso [ MaestroMuebles.dbo.proc_ConsultarNacoImp ] no se puede ejecutar.", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			
			if (odbcMaestroMuebles.State == ConnectionState.Open)
			{
				odbcMaestroMuebles.Close();
			}
			
			return sNacoImp;
		}

		/// <summary>
		/// Funcion encargada de limpiar los controles de un group box
		/// </summary>
		/// <returns>
		/// Controles limpios.
		/// </returns>
		/// <param name='gb'>
		/// GroupBox a limpiar
		/// </param>
		public static void fLimpiarControles(GroupBox gb)
		{
			foreach (Control c in gb.Controls)
			{
				if (c is TextBox)
				{
					c.Text = "";
				}
				else if (c is ComboBox)
				{
					var tmp = c as ComboBox;
					tmp.Items.Clear();
				}
				else if (c is DataGridView)
				{
					var tmp = c as DataGridView;
					tmp.Rows.Clear();
					tmp.Columns.Clear();
				}
				else if (c is CheckBox)
				{
					var tmp = c as CheckBox;
					tmp.Checked = false;
				}
			}
		}
	}
}

