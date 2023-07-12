using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication14.Models;
using WebApplication14.Controllers;

namespace WebApplication14.Repository
{
    

    public class VentaHandler


        {
        //Conexion a la base de datos 
        public const string connectionString = "Server=<LAPTOP-VU14H4RS\\SQLEXPRESS01> ; Database=<SistemaGestion>;Trusted_Connection=True;";

      //Traer ventas por ID de usuario
        public static List<Venta> TraerVentas_conIdUsuario(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                const string query = "SELECT v.Id, v.Comentarios " + // Query que me devuelve las ventas que contienen productos con IdUsuario = idUsuario.
                                        "FROM Venta AS v " +
                                        "INNER JOIN ProductoVendido AS pv " +
                                        "ON v.Id = pv.IdVenta " +
                                        "INNER JOIN Producto AS p " +
                                        "ON pv.IdProducto = p.Id " +
                                        "WHERE p.IdUsuario = @idUsuario ";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var parameterIdUsuario = new SqlParameter("idUsuario", SqlDbType.BigInt);
                    parameterIdUsuario.Value = idUsuario;
                    sqlCommand.Parameters.Add(parameterIdUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                ventas.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return ventas;
        }



        //Cargar ventas
        public static long CargarVenta(Venta venta)
        {
            bool resultado = false;
            long idVenta = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios) " + // Query que me permite agregar una Venta.
                                        "VALUES (@comentarios) " +
                                        "SELECT @@IDENTITY";

                var parameterComentarios = new SqlParameter("comentarios", SqlDbType.VarChar);
                parameterComentarios.Value = venta.Comentarios;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterComentarios);
                    idVenta = Convert.ToInt64(sqlCommand.ExecuteScalar());
                }
                sqlConnection.Close();
            }
            return idVenta;
        }


        //Método que trae todas los ProductoVendido de la BD que estén asociados a una venta.
        public static List<ProductoVendido> TraerVentas()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                const string query = "SELECT pv.Id, pv.Stock, pv.IdProducto, pv.IdVenta " + // Query que me devuelve las ventas que contienen productos con IdUsuario = idUsuario.
                                        "FROM Venta AS v " +
                                        "INNER JOIN ProductoVendido AS pv " +
                                        "ON v.Id = pv.IdVenta ";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["Idventa"]);

                                productosVendidos.Add(productoVendido);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productosVendidos;
        }
    }
}