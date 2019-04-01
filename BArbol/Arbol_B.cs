using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BArbol
{
	public class Arbol_B
	{
		private static int ORDEN { get; set; }
		public Nodob raiz;
		Medicamento mediana;
		Nodob ndtemporal;
		bool aumentarNivel;
		bool encontrado;
		private int nivel;
		private int nodos;

		public Arbol_B(int pOrden)
		{
			ORDEN = pOrden;
			raiz = null;
			nivel = 0;
			nodos = 0;
		}

		public void insertar(int id, String titulo)
		{
			Medicamento medi = new Medicamento(id, titulo);
			this.raiz = insertar(this.raiz, medi);
			this.nodos++;
		}

		public Nodob insertar(Nodob actual, Medicamento medicamento)
		{
			empujar(actual, medicamento);

			if (aumentarNivel)
			{
				Nodob temporal = new Nodob(ORDEN);
				temporal.incUsados();
				temporal.setMedicamento(0, mediana);
				temporal.setApuntadores(0, this.raiz);
				temporal.setApuntadores(1, ndtemporal);
				this.raiz = temporal;
				this.nivel++;
			}
			return this.raiz;
		}

		public void empujar(Nodob actual, Medicamento medicamento)
		{
			int k = 0;
			encontrado = false;
			if (actual == null)
			{
				aumentarNivel = true;
				this.mediana = medicamento;
				ndtemporal = null;
			}
			else
			{
				k = buscarNodo(medicamento, actual);
				if (encontrado)
				{
					aumentarNivel = false;
				}
				else
				{
					empujar(actual.getApuntadores(k), medicamento);
					if (aumentarNivel)
					{
						if (actual.isFull())
						{
							aumentarNivel = true;
							dividirNodo(this.mediana, actual, k);
						}
						else
						{
							aumentarNivel = false;
							meterMedicamento(this.mediana, actual, k);
						}
					}
				}
			}
		}

		public void meterMedicamento(Medicamento medicamento, Nodob actual, int indice)
		{
			for (int i = actual.getUsados(); i != indice; i--)
			{
				actual.setMedicamento(i, actual.getMedicamento(i - 1));
				actual.setApuntadores(i + 1, actual.getApuntadores(i));
			}

			actual.setMedicamento(indice, medicamento);
			actual.setApuntadores(indice + 1, ndtemporal);
			actual.incUsados();
		}

		public void dividirNodo(Medicamento medicamento, Nodob actual, int indice)
		{
			int posicionmediana = (indice <= (ORDEN / 2)) ? ORDEN / 2 : (ORDEN / 2) + 1;
			Nodob nuevaPagina = new Nodob(ORDEN);

			for (int pos = posicionmediana + 1; pos < ORDEN; pos++)
			{
				nuevaPagina.setMedicamento((pos - posicionmediana) - 1, actual.getMedicamento(pos - 1));
				nuevaPagina.setApuntadores(pos - posicionmediana, actual.getApuntadores(pos));
			}

			nuevaPagina.setUsados(ORDEN - 1 - posicionmediana);
			actual.setUsados(posicionmediana);

			if (indice <= (ORDEN / 2))
				meterMedicamento(medicamento, actual, indice);
			else
				meterMedicamento(medicamento, nuevaPagina, indice - posicionmediana);

			mediana = actual.getMedicamento(actual.getUsados() - 1);
			nuevaPagina.setApuntadores(0, actual.getApuntadores(actual.getUsados()));
			actual.setUsados(actual.getUsados() - 1);
			ndtemporal = nuevaPagina;
		}

		public int buscarNodo(Medicamento medicamento, Nodob actual)
		{
			int j = 0;
			if (medicamento.getId() < actual.getMedicamento(0).getId())
			{
				encontrado = false;
				j = 0;
			}
			else
			{
				j = actual.getUsados();
				while (medicamento.getId() < actual.getMedicamento(j - 1).getId() && j > 1)
					--j;
				encontrado = (medicamento.getId() == actual.getMedicamento(j - 1).getId());
			}
			return j;
		}

		public ListaMedicamentos busqueda(String nombre)
		{
			ListaMedicamentos temporal = new ListaMedicamentos();
			temporal = busquedaNombre(this.raiz, nombre, temporal);

			return temporal;
		}

		public ListaMedicamentos busquedaNombre(Nodob actual, String nombre, ListaMedicamentos temporal)
		{
			if (actual != null)
			{
				for (int i = 0; i < actual.getUsados(); i++)
				{
					if (actual.getMedicamento(i).getNombre() == nombre)
						temporal.insertar(actual.getMedicamento(i));
				}

				for (int i = 0; i <= actual.getUsados(); i++)
					temporal = busquedaNombre(actual.getApuntadores(i), nombre, temporal);
			}

			return temporal;
		}

		public void eliminar(Medicamento medicamento)
		{
			if (this.raiz != null)
			{
				eliminar(this.raiz, medicamento);
			}
		}

		public void eliminar(Nodob actual, Medicamento medicamento)
		{
			try
			{
				eliminarMedicamento(actual, medicamento);
			}
			catch (Exception e)
			{
				encontrado = false;
			}

			if (encontrado)
			{
				if (actual.getUsados() == 0)
					actual = actual.getApuntadores(0);
				this.raiz = actual;
			}
		}

		public void eliminarMedicamento(Nodob actual, Medicamento medicamento)
		{
			int posicion = 0;
			Medicamento temporal;

			if (actual == null)
			{
				encontrado = false;
			}
			else
			{
				posicion = buscarNodo(medicamento, actual);
				if (encontrado)
				{
					if (actual.getApuntadores(posicion - 1) == null)
						quitar(actual, posicion);
					else
					{
						sucesor(actual, posicion);
						eliminarMedicamento(actual.getApuntadores(posicion), actual.getMedicamento(posicion - 1));
					}
				}
				else
				{
					eliminarMedicamento(actual.getApuntadores(posicion), medicamento);
					if (actual.getApuntadores(posicion) != null && actual.getApuntadores(posicion).isHalfEmpty())
						restablecer(actual, posicion);
				}
			}
		}

		public void quitar(Nodob actual, int posicion)
		{
			for (int i = posicion + 1; i != actual.getUsados() + 1; i++)
			{
				actual.setMedicamento(i - 2, actual.getMedicamento(i - 1));
				actual.setApuntadores(i - 1, actual.getApuntadores(i));
			}
			actual.decUsados();
		}

		public void sucesor(Nodob actual, int indice)
		{
			Nodob temporal = actual.getApuntadores(indice);
			while (temporal.getApuntadores(0) != null)
				temporal = temporal.getApuntadores(0);
			actual.setMedicamento(indice - 1, temporal.getMedicamento(0));
		}

		public void restablecer(Nodob actual, int posicion)
		{
			if (posicion > 0)
			{
				if (actual.getApuntadores(posicion - 1).isHalfFull())
					correrDerecha(actual, posicion);
				else
					juntarMedicamento(actual, posicion);
			}
			else
			{
				if (actual.getApuntadores(ORDEN / 2 - 1).isHalfFull())
					correrIzquierda(actual, ORDEN / 2 - 1);
				else
					juntarMedicamento(actual, ORDEN / 2 - 1);
			}
		}

		public void correrDerecha(Nodob actual, int posicion)
		{
			Nodob temporal = actual.getApuntadores(posicion);
			for (int i = temporal.getUsados(); i > 0; i--)
			{
				temporal.setMedicamento(i, temporal.getMedicamento(i - 1));
				temporal.setApuntadores(i + 1, temporal.getApuntadores(i));
			}

			temporal.incUsados();
			temporal.setApuntadores(1, temporal.getApuntadores(0));
			temporal.setMedicamento(0, actual.getMedicamento(posicion - 1));
			actual.setMedicamento(posicion - 1, actual.getApuntadores(posicion - 1).getMedicamento(actual.getApuntadores(posicion - 1).getUsados() - 1));
			temporal.setApuntadores(0, actual.getApuntadores(posicion - 1).getApuntadores(actual.getApuntadores(posicion - 1).getUsados()));
			actual.getApuntadores(posicion - 1).decUsados();
		}

		public void correrIzquierda(Nodob actual, int posicion)
		{
			Nodob temporal = actual.getApuntadores(posicion - 1);
			temporal.incUsados();
			temporal.setMedicamento(temporal.getUsados() - 1, actual.getMedicamento(posicion - 1));
			temporal.setApuntadores(temporal.getUsados(), actual.getApuntadores(posicion).getApuntadores(0));
			actual.setMedicamento(posicion - 1, actual.getApuntadores(posicion).getMedicamento(0));
			actual.getApuntadores(posicion).setApuntadores(0, actual.getApuntadores(posicion).getApuntadores(1));
			actual.getApuntadores(posicion).decUsados();

			temporal = actual.getApuntadores(posicion);
			for (int i = 1; i != temporal.getUsados() + 1; i++)
			{
				temporal.setMedicamento(i - 1, temporal.getMedicamento(i));
				temporal.setApuntadores(i, temporal.getApuntadores(i + 1));
			}
		}

		public void juntarMedicamento(Nodob actual, int posicion)
		{
			Nodob derecha = actual.getApuntadores(posicion);
			Nodob izquierda = actual.getApuntadores(posicion - 1);
			izquierda.incUsados();

			izquierda.setMedicamento(izquierda.getUsados() - 1, actual.getMedicamento(posicion - 1));
			izquierda.setApuntadores(izquierda.getUsados(), derecha.getApuntadores(0));
			for (int i = 1; i != derecha.getUsados(); i++)
			{
				izquierda.incUsados();
				izquierda.setMedicamento(izquierda.getUsados() - 1, derecha.getMedicamento(i - 1));
				izquierda.setApuntadores(izquierda.getUsados(), derecha.getApuntadores(i));
			}
			quitar(actual, posicion);
		}

		public static int getORDEN()
		{
			return ORDEN;
		}

		public Nodob getRaiz()
		{
			return raiz;
		}

		public void setRaiz(Nodob raiz)
		{
			this.raiz = raiz;
		}

		bool isEmpty()
		{
			return (raiz == null);
		}

		public int getNivel()
		{
			return nivel;
		}

		public void setNivel(int nivel)
		{
			this.nivel = nivel;
		}

		public int getNodos()
		{
			return nodos;
		}

		public List<Nodob> recorrido = new List<Nodob>();
		public List<Nodob> recorrer()
		{
			recorrido.Clear();
			recorrer(recorrido, raiz);
			return recorrido;

		}

		private void recorrer(List<Nodob> tmp, Nodob hoja)
		{
			if (hoja != null)
			{
				tmp.Add(hoja);
				for (int i = 0; i < ORDEN; i++)
				{
					recorrer(tmp, hoja.getApuntadores(i));
				}

			}
		}
	}
}

