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
    public class Clientes
    {
        private IConfiguration configuration;
        private DataTable schema = null;
        private SqlDataReader dataRead = null;
        public Clientes(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void agregarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector,int id)
        {
           
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();

           
            SqlCommand cmd = new SqlCommand("insert into clientes (nombre,ced_juridica,pagina_web,direccion,telefono,sector,id_usuario) values" +
                                            " ('" + nombre + "'," +
                                            "'" + cedula_juri + 
                                            "','" + sitio + 
                                            "','" + direccion + 
                                            "','" + numero + 
                                            "','" + sector +
                                            "','" + id + "')", connection);
           // dataRead.Close();
            cmd.ExecuteScalar();
            
            connection.Close();
            
        }
        public IList<Ent_Cliente> cargarClientes(int id)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from clientes where id_usuario ='" + id + "'", connection);
            dataRead = cmd.ExecuteReader();

           // schema = dataRead.GetSchemaTable();
            IList<Ent_Cliente> listaCliente = new List<Ent_Cliente>();
            while (dataRead.Read())
            {
                listaCliente.Add(new Ent_Cliente() { Id = int.Parse(dataRead["id"].ToString()),
                                                Nombre = dataRead["nombre"].ToString(),
                                                Cedula = dataRead["ced_juridica"].ToString(),
                                                Sitio = dataRead["pagina_web"].ToString(),
                                                Direccion = dataRead["direccion"].ToString(),
                                                Numero = int.Parse(dataRead["telefono"].ToString()),
                                                Sector = dataRead["sector"].ToString()});
            }
            dataRead.Close();
            connection.Close();
            return listaCliente;
        }
        public void actualizarCliente(string nombre, string cedula_juri, string sitio, string direccion, int numero, string sector, int id_user,int cliente_act)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("update clientes set nombre='"+nombre+"'," +
                                            "ced_juridica='"+cedula_juri+"'," +
                                            "pagina_web='"+sitio+"'," +
                                            "direccion='"+direccion+"'," +
                                            "telefono='"+numero+"'," +
                                            "sector='"+sector+"'," +
                                            "id_usuario='"+id_user+"' where id='"+cliente_act+"'", connection);
            // dataRead.Close();
            cmd.ExecuteScalar();

            connection.Close();

        }
        public void eliminarCliente(int cliente_del)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("DELETE FROM clientes where id='"+cliente_del+"'", connection);
            
            cmd.ExecuteScalar();

            connection.Close();

        }
    }
}
