using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Reu_Usuarios
    {
        private int id;
        private int id_reunion;
        private int id_usuario;

        public int Id_reunion { get => id_reunion; set => id_reunion = value; }
        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public int Id { get => id; set => id = value; }
    }
}
