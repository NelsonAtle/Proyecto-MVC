using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Models
{
    public class Ent_Avatar
    {
        private int id;
        private string usuario;
        private string password;
        private string nombre;
        private string correo;
        private bool estado;

        public int Id { get => id; set => id = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public bool Estado { get => estado; set => estado = value; }
    }
}
