using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_4_AB.Models
{
	public class Farmaco
	{
		public int Id { get; set; }
		public String Nombre { get; set; }
		public string Descripcion { get; set; }
		public string Casa { get; set; }
		public double Precio { get; set; }
		public int Cant { get; set; }
	}
}