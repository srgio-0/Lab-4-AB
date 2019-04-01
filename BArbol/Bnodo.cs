using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BArbol
{
	public class Bnodo
	{
		public Bnodo siguiente;
		public Bnodo anterior;
		public Medicamento medicamento { get; set; }

		public Bnodo(Medicamento medicamento)
		{
			this.medicamento = medicamento;
			this.siguiente = null;
		}
		public Bnodo getSiguiente()
		{
			return siguiente;
		}
		public void setSiguiente(Bnodo siguiente)
		{
			this.siguiente = siguiente;
		}
		public Medicamento getMedicamento()
		{
			return medicamento;
		}
		public void setMedicamento(Medicamento medicamento)
		{
			this.medicamento = medicamento;
		}
		public Bnodo getAnterior()
		{
			return anterior;
		}
		public void setAnterior(Bnodo anterior)
		{
			this.anterior = anterior;
		}
	}
}
