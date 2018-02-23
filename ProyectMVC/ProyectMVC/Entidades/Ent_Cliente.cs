using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Cliente
    {
        private int id;
        private string nombre;
        private string cedula;
        private string sitio;
        private string direccion;
        private int numero;
        private string sector;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Sitio { get => sitio; set => sitio = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Sector { get => sector; set => sector = value; }
    }
}
