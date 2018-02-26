using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Usuario
    {
        private int id;
        private string avatar;
        private string password;
        private string nombre;
        private string correo;
        private string tipo;
        private int estado;

        public int Id { get => id; set => id = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Estado { get => estado; set => estado = value; }
    }
}
