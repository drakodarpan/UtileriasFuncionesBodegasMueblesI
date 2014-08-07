using System;

namespace UtileriasFuncionesBodegasMueblesI
{
	public class ConexionComprasMuebles
	{
		public ConexionComprasMuebles ()
		{
		}

		private string sIp;
		private string sUsuario;
		private string sPass;
		private string sBaseDeDatos;
		private int nEmpleado;
		private int nOpcion;
		private string sNumTemporal;
		private string sNacoImp;
		private string sFechaServer;

		public string Ip {
			get {
				return sIp;
			}
			set {
				sIp = value;
			}
		}

		public string Usuario {
			get {
				return sUsuario;
			}
			set {
				sUsuario = value;
			}
		}

		public string Pass {
			get {
				return sPass;
			}
			set {
				sPass = value;
			}
		}

		public string BaseDeDatos {
			get {
				return sBaseDeDatos;
			}
			set {
				sBaseDeDatos = value;
			}
		}

		public int Empleado {
			get {
				return nEmpleado;
			}
			set {
				nEmpleado = value;
			}
		}

		public int Opcion {
			get {
				return nOpcion;
			}
			set {
				nOpcion = value;
			}
		}

		public string NumTemporal {
			get {
				return sNumTemporal;
			}
			set {
				sNumTemporal = value;
			}
		}

		public string NacoImp {
			get {
				return sNacoImp;
			}
			set {
				sNacoImp = value;
			}
		}

		public string FechaServer {
			get {
				return sFechaServer;
			}
			set {
				sFechaServer = value;
			}
		}
	}
}

