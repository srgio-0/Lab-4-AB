using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BArbol;
using Lab_4_AB.Models;

namespace Lab_4_AB.Controllers
{
	public class ServiciosController : Controller
	{
		static List<Pedidos> Pedidos = new List<Pedidos>();
		Servicios Servicios = new Servicios();
		static public int grados;
		// GET: Servicios
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Index(int grado)
		{
			if (grado > 1)
			{
				Servicios.Incializar(grado);
				ViewBag.Message = "";
				return RedirectToAction("Ok", "Servicios");
			}
			else
			{
				ViewBag.Message = "Ingrese de nuevo el grado";
				return View();
			}
		}

		[HttpGet]
		public ActionResult Comprar()
		{
			List<Farmaco> listaMed = Servicios.getLista();
			return View(listaMed);
		}
		//La busqueda solo es EXISTOSA SI EL NOMBRE INGRESADO ES IGUAL AL DEL ARBOL
		[HttpPost]
		public ActionResult Comprar(String cadena)
		{
			List<Farmaco> listaMed = new List<Farmaco>();
			try
			{
				listaMed = Servicios.Buscar(cadena);
				return View(listaMed);
			}
			catch
			{
				//Si el medicamento no existe se viene aca
				return RedirectToAction("NoFarmaco", "Servicios");
			}
		}

		public ActionResult NoFarmaco()
		{
			return View();
		}

		public ActionResult Ok()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult AñadirProducto(int Id, string Nombre, string Descrip, string Casa, double Precio, int Cant)
		{
			Farmaco Farmaco = new Farmaco();
			Farmaco.Id = Id;
			Farmaco.Nombre = Nombre;
			Farmaco.Descripcion = Descrip;
			Farmaco.Casa = Casa;
			Farmaco.Precio = Precio;
			Farmaco.Cant = Cant;
			Servicios.AñadirCarrito(Farmaco);
			//Manda a quitar 1 existencia del producto
			Servicios.QuitarExistencia(Nombre);
			ViewBag.Message = "Producto Añadido Correctamente";
			return View();
		}

		public ActionResult Pagar(string Nombre, string Direccion, int NIT = 0, double Total = 0)
		{
			if (Nombre != null && Direccion != null && NIT > 0 && Total > 0)
			{
				Pedidos tmp = new Pedidos();
				tmp.Nombre = Nombre;
				tmp.Direccion = Direccion;
				tmp.NIT = NIT;
				tmp.Total = Total;
				Pedidos.Add(tmp);
				Servicios.BorrarCarrito();
				return RedirectToAction("Ok", "Servicios");
			}
			var aux = Servicios.getCarrito();
			return View(aux);
		}

		public ActionResult VerPedidos()
		{
			return View(Pedidos);
		}

		public ActionResult ReAbastecer()
		{
			var auxliar = Servicios.getReAbastecer();
			return View(auxliar);
		}

		[AllowAnonymous]
		public ActionResult ReAbastecer2(int Id, string Nombre, string Descrip, string Casa, double Precio, int Cant)
		{
			Servicios.ReAbastecerProductos();
			return View();
		}
		
		//EL SIGUIENTE METODO
		//1)Genera el nuevo Archivo con los datos que fueron eliminados o modificados para la siguiente ejecucion
		//2)Genera el JSON con el recorrido del arbol
		public ActionResult Recorrido()
		{
			//Generacion del nuevo Archivo
			List<Farmaco> ListaNueva = Servicios.getLista();
			System.IO.File.Delete(@"C:\Users\Sergio Daniel\Documents\Programas Visual Studio 2017\Lab 4 AB\Lab 4 AB\Files\MOCK_DATA.xls");
			using (StreamWriter archivo = new StreamWriter(@"C:\Users\Sergio Daniel\Documents\Programas Visual Studio 2017\Lab 4 AB\Lab 4 AB\Files\MOCK_DATA.xls", true))
			{
				archivo.Flush();
				foreach (var item in ListaNueva)
				{
					archivo.WriteLine(item.Id.ToString() + "," + item.Nombre + "," + item.Descripcion + "," + item.Casa + ",$" + item.Precio + "," + item.Cant);
				}
			}
			//Mostrar recorrido
			var temporal = Servicios.RecorrerJ();
			List<Farmaco> tmp = new List<Farmaco>();
			foreach (var item in temporal)
			{

				if (item.medicamento[1] != null)
				{
					Farmaco x = new Farmaco { Id = item.medicamento[1].id, Nombre = item.medicamento[1].Nombre };
					tmp.Add(x);
				}

			}

			return View(tmp);
		}
	}
}