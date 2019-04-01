using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BArbol
{
	public class Nodob
	{
		public Nodob[] apuntadores;
		public Medicamento[] medicamento;
		public static int ORDEN { get; set; }
		public int usados;

		public Nodob(int pOrden)
		{
			ORDEN = pOrden;
			this.usados = 0;
			apuntadores = new Nodob[ORDEN + 1];
			medicamento = new Medicamento[ORDEN];

			for (int i = 0; i < ORDEN; i++)
			{
				apuntadores[i] = null;
				medicamento[i] = null;
			}
			apuntadores[ORDEN] = null;
		}

		public bool isFull()
		{
			return (usados == (ORDEN - 1));
		}

		public bool isEmpty()
		{
			return (usados == 0);
		}

		public bool isHalfEmpty()
		{
			return (usados < ORDEN / 2);
		}

		public bool isHalfFull()
		{
			return (usados > ORDEN / 2);
		}

		public bool esHoja()
		{
			for (int i = 0; i < ORDEN; i++)
				if (apuntadores[i] != null)
					return false;
			return true;
		}

		public void incUsados()
		{
			this.usados++;
		}

		public void decUsados()
		{
			this.usados--;
		}

		public Nodob getApuntadores(int indice)
		{
			return apuntadores[indice];
		}
		public void setApuntadores(int indice, Nodob apuntador)
		{
			this.apuntadores[indice] = apuntador;
		}
		public Medicamento getMedicamento(int indice)
		{
			return medicamento[indice];
		}
		public void setMedicamento(int indice, Medicamento medicamento)
		{
			this.medicamento[indice] = medicamento;
		}
		public int getOrden()
		{
			return ORDEN;
		}
		public int getUsados()
		{
			return usados;
		}
		public void setUsados(int usados)
		{
			this.usados = usados;
		}
	}
}
