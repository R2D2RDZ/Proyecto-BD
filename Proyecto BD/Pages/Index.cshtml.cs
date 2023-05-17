using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Proyecto_BD.Models;
using Microsoft.Data.SqlClient;

namespace Proyecto_BD.Pages
{
    public struct Venta
    {
        public int id;
        public string orden;
        public int cantidad;
        public int id_pieza;
        public string nombre;
        public string RFC;
        public string regimen;
    }
    public class IndexModel : PageModel
    {
        public int numCols = 3;
        public List<Venta> ventas= new List<Venta>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SqlDataReader reader = EjecutarCommand("ObtenerVenta");
            int i = 0;
            while (reader.Read())
            {
                //Console.WriteLine(reader.GetString(0));
                Venta venta = new Venta();
                venta.id = reader.GetInt32(0);
                venta.orden = reader.GetString(1);
                venta.cantidad = reader.GetInt32(2);
                venta.id_pieza = reader.GetInt32(3);
                venta.nombre = reader.GetString(4);
                venta.RFC = reader.GetString(5);
                venta.regimen = reader.GetString(6);
                ventas.Add(venta);
                i++;
            }
            connection.Close();
            
        }
        SqlConnection connection;
        SqlDataReader EjecutarCommand(string SQLstring)
        {
            string connectionString = "Data Source=R2D2RDZ\\MSSQLSERVER01;Initial Catalog=ProyectoBD;User ID=Proyecto;Password=Proyecto1;TrustServerCertificate=True;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(SQLstring, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        SqlDataReader EjecutarCommand(string SQLstring, string[] values1, int[] values2)
        {
            string connectionString = "Data Source=R2D2RDZ\\MSSQLSERVER01;Initial Catalog=ProyectoBD;User ID=Proyecto;Password=Proyecto1;TrustServerCertificate=True;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(SQLstring, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }


        void AddCliente(string[] values1, int[] values2)
        {

            string sqlString = $"INSERT INTO ventas (orden_compra, cantidad, id_pieza, nombre_comprador, RFC, regimen_fiscal) VALUES ('{values1[0]}', {values2[0]}, {values2[1]}, '{values1[1]}', '{values1[2]}', '{values1[3]}');";
            Console.WriteLine(sqlString);
            try
            {
                EjecutarCommand(sqlString, values1, values2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            OnGet();
        }
        public async Task OnPostAdd(string order, int quantity, int id_part, string name, string rfc, string regimen)
        {
            string[] values1 = new string[] {order, name, rfc, regimen };
            int[] values2 = new int[] { quantity, id_part };
            AddCliente(values1, values2);
        }

        void DeleteCliente(int id)
        {
            string sqlString = $"DELETE FROM ventas WHERE id_venta={id}";
            Console.WriteLine(sqlString);
            try
            {
                EjecutarCommand(sqlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            OnGet();
        }

        public async Task OnPostDelete(int id)
        {
            DeleteCliente(id);
        }

        void UpdateCliente(string[] values1, int[] values2)
        {
            string sqlString = $"UPDATE ventas SET orden_compra = '{values1[0]}', cantidad = {values2[0]}, id_pieza = {values2[1]}, nombre_comprador = '{values1[1]}', rfc = '{values1[2]}', regimen_fiscal = '{values1[3]}' WHERE id_venta = {values2[2]};";
            Console.WriteLine(sqlString);
            try
            {
                EjecutarCommand(sqlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            OnGet();
        }
        public async Task OnPostUpdate(int id, string order, int quantity, int id_part, string name, string rfc, string regimen)
        {
            string[] values1 = new string[] { order, name, rfc, regimen };
            int[] values2 = new int[] { quantity, id_part, id };
            UpdateCliente(values1, values2);
        }
    }
}