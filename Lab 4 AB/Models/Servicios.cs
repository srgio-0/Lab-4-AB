using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BArbol;
using System.IO;

namespace Lab_4_AB.Models
{
	public class Servicios
	{
		static Arbol_B AB;
		static List<Farmaco> Lista = new List<Farmaco>();
		static List<Farmaco> Carrito = new List<Farmaco>();
		static List<Farmaco> ReAbastecer = new List<Farmaco>();
		static List<int> ReAbastecerID = new List<int>();

		public void Incializar(int grado)
		{
			AB = new Arbol_B(grado);
			string[] lineas = File.ReadAllLines(@"C:\Users\Sergio Daniel\Documents\Programas Visual Studio 2017\Lab 4 AB\Lab 4 AB\Files\MOCK_DATA.xls");
			int cant = lineas.Length + 1;
			int contador = 0;
			for (int row = 0; row < (lineas.Length); row++)
			{
				Farmaco Farmaco = new Farmaco();
				string[] partes = lineas[row].Split(',');
				Farmaco.Id = int.Parse(partes[0]);
				Farmaco.Nombre = partes[1];
				Farmaco.Descripcion = partes[2];
				Farmaco.Casa = partes[3];
				string[] tmp1 = partes[4].Split('$');
				Farmaco.Precio = double.Parse(tmp1[1]);
				Farmaco.Cant = int.Parse(partes[5]);
				Lista.Add(Farmaco);
				AB.insertar(contador, Farmaco.Nombre);
				contador++;
				Farmaco = null;
			}
		}

		public List<Farmaco> getLista()
		{
			return Lista;
		}

		public List<Farmaco> Buscar(String nombre)
		{
			//Devuelve el nodo donde esta el nombre
			var x = AB.busqueda(nombre);
			//Se busca el nombre en la lista que tiene el nodo 
			var x2 = x.buscar(nombre);
			List<Farmaco> aux = new List<Farmaco>();
			//Retorna el Farmaco guardado en la lista con todos lo farmacos en base al id que tiene de posicion 
			aux.Add(Lista[x2.medicamento.id]);
			return aux;
		}

		public void AñadirCarrito(Farmaco Farmaco)
		{
			Carrito.Add(Farmaco);
		}

		public List<Farmaco> getCarrito()
		{
			return Carrito;
		}

		public void BorrarCarrito()
		{
			Carrito = null;
		}

		public void QuitarExistencia(string nombre)
		{
			var x = AB.busqueda(nombre);
			var x2 = x.buscar(nombre);
			var x3 = Lista[x2.medicamento.id].Cant;
			Lista[x2.medicamento.id].Cant = Lista[x2.medicamento.id].Cant - 1;
			if (Lista[x2.medicamento.id].Cant <= 0)
			{
				ReAbastecer.Add(Lista[x2.medicamento.id]);
				ReAbastecerID.Add(x2.medicamento.id);
				AB.eliminar(x2.medicamento);
			}
		}

		public List<Farmaco> getReAbastecer()
		{
			return ReAbastecer;
		}

		public void ReAbastecerProductos()
		{
			int contador = 0;
			foreach (var item in ReAbastecer)
			{
				Random rand = new Random();
				Lista[ReAbastecerID[contador]].Cant = rand.Next(1, 15);
				AB.insertar(ReAbastecerID[contador], ReAbastecer[contador].Nombre);
				contador++;
			}
			ReAbastecer = null;
			ReAbastecerID = null;
		}

		public List<Nodob> RecorrerJ()
		{
			var auxiliar = AB.recorrer();
			return auxiliar;
		}

		public void BorrarArbol()
		{ }
	}
}