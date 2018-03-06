using Microsoft.Extensions.Configuration;
using ProyectMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Models
{
    public class Tikects
    {
        private IConfiguration configuration;

        private SqlDataReader dataRead = null;
        public Tikects(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void agregarTikete(string titulo, string detalle, int reportante, int cliente, string estado)
        {
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("INSERT INTO tikects(titulo,detalle,id_reportante,id_cliente,estado) values('" + titulo + "','" + detalle + "','" + reportante + "'," + cliente + ",'" + estado + "'); ", connection);

            cmd.ExecuteScalar();

           
            connection.Close();

        }
        public IList<Ent_Tikects> cargarTiketes()
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from tikects", connection);
            dataRead = cmd.ExecuteReader();

            // schema = dataRead.GetSchemaTable();
            IList<Ent_Tikects> listaTiketes = new List<Ent_Tikects>();
            while (dataRead.Read())
            {
                listaTiketes.Add(new Ent_Tikects()
                {
                    Id = int.Parse(dataRead["id"].ToString()),
                    Titulo = dataRead["titulo"].ToString(),
                    Detalle = dataRead["detalle"].ToString(),
                    Reportante = Convert.ToInt32(dataRead["id_reportante"].ToString()),
                    Cliente = Convert.ToInt32(dataRead["id_cliente"].ToString()),
                    Estado = dataRead["estado"].ToString()

                });
            }
            dataRead.Close();
            connection.Close();
            return listaTiketes;
        }
        public void actualizarTikete(string titulo, string detalle, int reportante, int cliente, string estado,int id)
        {
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("update tikects set titulo='" + titulo + "'," +
                                            "detalle='" + detalle + "'," +
                                            "id_reportante='" + reportante + "'," +
                                            "id_cliente='" + cliente + "'," +
                                            "estado='" + estado + "' where id="+id, connection);
            
            cmd.ExecuteScalar();



            connection.Close();

        }
        public void eliminarTikete(int id)
        {
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("delete from tikects where id='" + id + "'", connection);

            cmd.ExecuteScalar();



            connection.Close();

        }
    }
}
