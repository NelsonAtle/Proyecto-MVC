using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProyectMVC.Models;
using ProyectMVC.Entidades;
using Microsoft.AspNetCore.Http;
using System.Collections;
//using System.Web.SessionState;

namespace ProyectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult RegistrarUsuario(string avatar, string contrasena, string nombre, string correo)
        {
            Usuarios us = new Usuarios(configuration);
            bool banderaRegistro = us.agregarUsuario(avatar,contrasena,nombre,correo);
            if (banderaRegistro)
            {
               ViewBag.registro = true;
            }
            else {
               ViewBag.registro = false;
            }

            return View("Index");
        }

        public IActionResult LoginUsuario(string avatar, string contrasena)
        {
            Usuarios us = new Usuarios(configuration);
            Ent_Avatar avatar_user = us.loginUsuario(avatar, contrasena);
            if (avatar_user.Estado)
            {


                HttpContext.Session.SetInt32("ID", int.Parse(avatar_user.Id.ToString()));
                HttpContext.Session.SetString("AVATAR", avatar_user.Usuario);
                HttpContext.Session.SetString("PASSWORD", avatar_user.Password);
                HttpContext.Session.SetString("NOMBRE", avatar_user.Nombre);
                HttpContext.Session.SetString("CORREO", avatar_user.Correo);
                int id = int.Parse(HttpContext.Session.GetInt32("ID").ToString());
                Clientes cl = new Clientes(configuration);
                IList<Ent_Cliente> lista = cl.cargarClientes(id);
                ViewData["listaClientes"] = lista;
                return View("Principal");
            }
            ViewBag.login = false;
            return View("Index");
        }

        public IActionResult Principal()
        {
            int id = int.Parse(HttpContext.Session.GetInt32("ID").ToString());
            Clientes cl = new Clientes(configuration);
            IList<Ent_Cliente> lista = cl.cargarClientes(id);
            ViewData["listaClientes"] = lista;
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Salir() {
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("AVATAR");
            HttpContext.Session.Remove("PASSWORD");
            HttpContext.Session.Remove("NOMBRE");
            HttpContext.Session.Remove("CORREO");
            return View("Index");
        }

        //Cuentas
        public IActionResult RegistrarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector, int usuario)
        {
            Clientes cl = new Clientes(configuration);
            int id = int.Parse(HttpContext.Session.GetInt32("ID").ToString());

            cl.agregarCliente(nombre, cedula_juri, sitio, direccion,numero,sector,id);
            IList<Ent_Cliente> lista = cl.cargarClientes(id);
            ViewData["listaClientes"] = lista;
            return View("Principal");
        }
        public IActionResult ActualizarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector, int usuario,int cliente)
        {
            Clientes cl = new Clientes(configuration);
            int id = int.Parse(HttpContext.Session.GetInt32("ID").ToString());

            cl.actualizarCliente(nombre, cedula_juri, sitio, direccion, numero, sector, id,cliente);
            IList<Ent_Cliente> lista = cl.cargarClientes(id);
            ViewData["listaClientes"] = lista;
            return View("Principal");
        }
        public IActionResult EliminarCliente(int cliente_del)
        {
            Clientes cl = new Clientes(configuration);
            cl.eliminarCliente(cliente_del);
            int id = int.Parse(HttpContext.Session.GetInt32("ID").ToString());
            IList<Ent_Cliente> lista = cl.cargarClientes(id);
            ViewData["listaClientes"] = lista;
            return View("Principal");
        }
    }
}
