using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ProyectMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectMVC.Models
{
    public class Contactos
    {
        private IConfiguration configuration;
        //private DataTable schema = null;
        private SqlDataReader dataRead = null;
        public Contactos(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void agregarContacto(string nombre, string apellido, string correo, int numero, string puesto,int id_cliente)
        {
           
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();

           
            SqlCommand cmd = new SqlCommand("insert into contactos (nombre,apellidos,correo,numero,puesto,id_cliente) values" +
                                            " ('" + nombre + "'," +
                                            "'" + apellido + 
                                            "','" + correo + 
                                            "','" + numero + 
                                            "','" + puesto +
                                            "','" + id_cliente + "')", connection);
           // dataRead.Close();
            cmd.ExecuteScalar();
            
            connection.Close();
            
        }
        public IList<Ent_Contacto> cargarContactos()
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from contactos", connection);
            dataRead = cmd.ExecuteReader();

           // schema = dataRead.GetSchemaTable();
            IList<Ent_Contacto> listaContacto = new List<Ent_Contacto>();
            while (dataRead.Read())
            {
                listaContacto.Add(new Ent_Contacto() { Id = int.Parse(dataRead["id"].ToString()),
                                                Nombre = dataRead["nombre"].ToString(),
                                                Apellido = dataRead["apellidos"].ToString(),
                                                Correo = dataRead["correo"].ToString(),
                                                Numero = int.Parse(dataRead["numero"].ToString()),
                                                Puesto = dataRead["puesto"].ToString(),
                                                Id_cliente = int.Parse(dataRead["id_cliente"].ToString()),
                                            });
            }
            dataRead.Close();
            connection.Close();
            return listaContacto;
        }
        public void actualizarContacto(string nombre, string apellido, string correo, int numero, string puesto, int id_cliente,int id_contacto)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("update contactos set nombre='"+nombre+"'," +
                                            "apellidos='"+apellido+"'," +
                                            "correo='"+correo+"'," +
                                            "numero='"+numero+"'," +
                                            "puesto='"+puesto+"'," +
                                            "id_cliente='"+id_cliente+"' where id='"+id_contacto+"'", connection);
            // dataRead.Close();
            cmd.ExecuteScalar();

            connection.Close();

        }
        public void eliminarContacto(int contacto_del)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("DELETE FROM contactos where id='"+contacto_del+"'", connection);
            
            cmd.ExecuteScalar();

            connection.Close();

        }
    }
}
