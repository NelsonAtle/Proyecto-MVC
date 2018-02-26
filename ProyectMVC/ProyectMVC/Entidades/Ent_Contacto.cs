using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Entidades
{
    public class Ent_Contacto
    {
        private int id;
        private string nombre;
        private string apellido;
        private string correo;
        private int numero;
        private string puesto;
        private int id_cliente;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Correo { get => correo; set => correo = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public int Id_cliente { get => id_cliente; set => id_cliente = value; }
    }
}
