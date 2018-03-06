using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Reunion
    {
        private int id;
        private string titulo;
        private string dia;
        private string tiempo;
        private IList<Ent_Reu_Usuarios> usuarios;
        private int linea;
        private int cliente;

        public string Titulo { get => titulo; set => titulo = value; }
        public string Dia { get => dia; set => dia = value; }
        public string Tiempo { get => tiempo; set => tiempo = value; }
        public IList<Ent_Reu_Usuarios> Usuarios { get => usuarios; set => usuarios = value; }
        public int Linea { get => linea; set => linea = value; }
        public int Cliente { get => cliente; set => cliente = value; }
        public int Id { get => id; set => id = value; }
    }
}
