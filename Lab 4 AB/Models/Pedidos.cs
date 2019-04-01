using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_4_AB.Models
{
	public class Pedidos
	{
		public string Nombre { get; set; }
		public string Direccion { get; set; }
		public int NIT { get; set; }
		public double Total { get; set; }
	}
}