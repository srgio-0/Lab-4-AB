using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BArbol
{
	public class Medicamento
	{
		public string Nombre { get; set; }
		public int id { get; set; }

		public Medicamento(int ID, string Nombre)
		{
			this.Nombre = Nombre;
			this.id = ID;
		}
		public int getId()
		{
			return id;
		}
		public string getNombre()
		{
			return Nombre;
		}
	}
}
