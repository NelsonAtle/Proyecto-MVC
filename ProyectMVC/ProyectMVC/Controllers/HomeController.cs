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
        private Clientes cl;
        private Contactos con;
        private Usuarios us;
        private Reuniones rn;
        private Tikects tk;
        private IList<Ent_Cliente> listaCli;
        private IList<Ent_Contacto> listaCon;
        private IList<Ent_Usuario> listUsers;
        private IList<Ent_Reunion> listReuniones;
        private IList<Ent_Tikects> listTiketes;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
            cl = new Clientes(configuration);
            con = new Contactos(configuration);
            us = new Usuarios(configuration);
            rn = new Reuniones(configuration);
            tk = new Tikects(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public void cargar()
        {
            listaCli = cl.cargarClientes();
            listaCon = con.cargarContactos();
            listUsers = us.cargarUsuarios();
            listReuniones = rn.cargarReuniones();
            listTiketes = tk.cargarTiketes();
        }

        public IActionResult LoginUsuario(string avatar, string contrasena)
        {
            Usuarios us = new Usuarios(configuration);
            Ent_Usuario usuario = us.loginUsuario(avatar, contrasena);
            if (usuario.Estado>0)
            {


                HttpContext.Session.SetInt32("ID", int.Parse(usuario.Id.ToString()));
                HttpContext.Session.SetString("AVATAR", usuario.Avatar);
                HttpContext.Session.SetString("PASSWORD", usuario.Password);
                HttpContext.Session.SetString("NOMBRE", usuario.Nombre);
                HttpContext.Session.SetString("CORREO", usuario.Correo);
                HttpContext.Session.SetString("TIPO", usuario.Tipo);
                cargar();
                ViewData["listaClientes"] = listaCli;
                ViewData["listaContactos"] = listaCon;
                ViewData["listaUsuarios"] = listUsers;
                ViewData["listaReuniones"] = listReuniones;
                ViewData["listaTiketes"] = listTiketes;
                return View("Principal");
            }
            ViewBag.login = false;
            return View("Index");
        }

        public IActionResult Principal()
        {

            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
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

        //Cuentas Usuarios
        public IActionResult RegistrarUsuario(string avatar, string contrasena, string nombre, string correo)
        {
            Usuarios us = new Usuarios(configuration);
            bool banderaRegistro = us.agregarUsuario(avatar, contrasena, nombre, correo);
            if (banderaRegistro)
            {
                ViewBag.registro = true;
            }
            else
            {
                ViewBag.registro = false;
            }
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;

            return View("Principal");
        }
        public IActionResult ActualizarUsuario(string avatar, string contrasena, string nombre, string correo, int id_user)
        {

            us.actualizarUsuario(avatar, contrasena, nombre, correo, id_user);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult EliminarUsuario(int id_user)
        {

            us.eliminarUsuario(id_user);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }

        //Cuentas Clientes
        public IActionResult RegistrarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector, int usuario)
        {
           
            cl.agregarCliente(nombre, cedula_juri, sitio, direccion, numero, sector);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult ActualizarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector, int usuario,int cliente)
        {
           
            cl.actualizarCliente(nombre, cedula_juri, sitio, direccion, numero, sector,cliente);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult EliminarCliente(int cliente_del)
        {
            
            cl.eliminarCliente(cliente_del);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }

        //Cuenta Contactos
        public IActionResult RegistrarContacto(string nombre, string apellidos, string correo, int numero, string puesto, int id_cliente)
        {
            
            con.agregarContacto(nombre, apellidos, correo, numero, puesto, id_cliente);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult ActualizarContacto(string nombre, string apellidos, string correo, int numero, string puesto, int id_cliente, int id_contacto)
        {
           
            con.actualizarContacto(nombre, apellidos, correo, numero, puesto, id_cliente,id_contacto);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult EliminarContacto(int id_contacto)
        {
           
            con.eliminarContacto(id_contacto);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }

        //Cuenta Contactos
        public IActionResult RegistrarReunion(string titulo, DateTime dia, DateTime hora, int[] usuarios, bool linea, int cliente)
        {

            rn.agregarReunion(titulo,dia,hora,usuarios,linea,cliente);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult ActualizarReunion(string titulo, DateTime dia, DateTime hora, int[] usuarios, bool linea, int cliente,int id)
        {

            rn.actualizarReunion(titulo,dia,hora,usuarios,linea,cliente,id);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult EliminarReunion(int id)
        {

            rn.eliminarReunion(id);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }

        //Cuenta Tiketes
        public IActionResult RegistrarTikete(string titulo, string detalle, int id_cliente, string estado)
        {
            tk.agregarTikete(titulo,detalle,Convert.ToInt32(HttpContext.Session.GetInt32("ID")),id_cliente,estado);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult ActualizarTikete(string titulo, string detalle, int id_cliente, string estado,int id)
        {

            tk.actualizarTikete(titulo,detalle, Convert.ToInt32(HttpContext.Session.GetInt32("ID")),id_cliente,estado,id);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }
        public IActionResult EliminarTikete(int id)
        {

            tk.eliminarTikete( id);
            cargar();
            ViewData["listaClientes"] = listaCli;
            ViewData["listaContactos"] = listaCon;
            ViewData["listaUsuarios"] = listUsers;
            ViewData["listaReuniones"] = listReuniones;
            ViewData["listaTiketes"] = listTiketes;
            return View("Principal");
        }


    }
}
