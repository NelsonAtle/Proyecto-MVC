using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Tikects
    {
        private int id;
        private string titulo;
        private string detalle;
        private int reportante;
        private int cliente;
        private string estado;

        public string Titulo { get => titulo; set => titulo = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public int Reportante { get => reportante; set => reportante = value; }
        public int Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }
        public int Id { get => id; set => id = value; }
    }
}
