using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BArbol
{
	public class ListaMedicamentos
	{
		public Bnodo cabeza;
		public Bnodo cola;
		public int cont;

		public ListaMedicamentos()
		{
			this.cabeza = null;
			this.cola = null;
			this.cont = 0;
		}

		public void insertar(Medicamento medicamento)
		{
			if (this.cabeza == null)
			{
				this.cabeza = new Bnodo(medicamento);
				this.cola = this.cabeza;
				this.cont++;
			}
			else
			{
				this.cola.setSiguiente(new Bnodo(medicamento));
				this.cola.getSiguiente().setAnterior(this.cola);
				this.cola = this.cola.getSiguiente();
				this.cont++;
			}
		}

		public void eliminar(String identificador)
		{
			//CAMBIE EL INT DE IDENTIFICADOR POR STRING 
			Bnodo temporal = buscar(identificador);

			if (temporal != null)
			{
				if (temporal == this.cabeza && this.cabeza == this.cola)
				{
					this.cabeza = null;
					this.cola = null;
					this.cont--;
				}
				else if (temporal == this.cabeza)
				{
					this.cabeza = this.cabeza.getSiguiente();
					this.cabeza.setAnterior(null);
					this.cont--;
				}
				else if (temporal == this.cola)
				{
					this.cola = this.cola.getAnterior();
					this.cola.setSiguiente(null);
					this.cont--;
				}
				else
				{
					temporal.getAnterior().setSiguiente(temporal.getSiguiente());
					temporal.getSiguiente().setAnterior(temporal.getAnterior());
					this.cont--;
				}
			}
		}

		public Bnodo buscar(string identificador)
		{
			Bnodo temporal = this.cabeza;
			while (temporal != null)
			{
				//var tmp = temporal.getMedicamento();
				if (temporal.medicamento.Nombre == identificador)
					return temporal;

				temporal = temporal.getSiguiente();
			}

			return null;
		}
		public Bnodo getCabeza()
		{
			return cabeza;
		}
		public void setCabeza(Bnodo cabeza)
		{
			this.cabeza = cabeza;
		}
		public Bnodo getCola()
		{
			return cola;
		}
		public void setCola(Bnodo cola)
		{
			this.cola = cola;
		}
		public int getCont()
		{
			return cont;
		}
		public void setCont(int cont)
		{
			this.cont = cont;
		}
	}
}
