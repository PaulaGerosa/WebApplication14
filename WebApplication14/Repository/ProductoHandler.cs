using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication14.Models;

namespace WebApplication14.Repository
{
    public class ProductoHandler
    {
       
        public const string connectionString = "Server=<LAPTOP-VU14H4RS\\SQLEXPRESS01> ; Database=<SistemaGestion>;Trusted_Connection=True;";

        //Crear Producto
        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false; 
            long idProducto = 0;    

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " + // Query que me permite agregar un producto a la BD.
                                        "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario) " +
                                        "SELECT @@IDENTITY";

                
                var parameterDescripciones = new SqlParameter("descripciones", SqlDbType.VarChar);  
                parameterDescripciones.Value = producto.Descripciones;                                

                var parameterCosto = new SqlParameter("costo", SqlDbType.Money);
                parameterCosto.Value = producto.Costo;

                var parameterPrecioVenta = new SqlParameter("precioVenta", SqlDbType.Money);
                parameterPrecioVenta.Value = producto.PrecioVenta;

                var parameterStock = new SqlParameter("stock", SqlDbType.Int);
                parameterStock.Value = producto.Stock;

                var parameterIdUsuario = new SqlParameter("idUsuario", SqlDbType.BigInt);
                parameterIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open(); 

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection)) 
                {
                    
                    sqlCommand.Parameters.Add(parameterDescripciones); 
                    sqlCommand.Parameters.Add(parameterCosto);
                    sqlCommand.Parameters.Add(parameterPrecioVenta);
                    sqlCommand.Parameters.Add(parameterStock);
                    sqlCommand.Parameters.Add(parameterIdUsuario);
                    idProducto = Convert.ToInt64(sqlCommand.ExecuteScalar());  
                }
                sqlConnection.Close(); 
            }
            if (idProducto != 0) 
            {
                resultado = true; 
            }
            return resultado;

            

        }

        //Modificar Producto 

        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false; 
            int rowsAffected = 0;   

            
            if (producto.Id <= 0) 
            {
                return false;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto] " + 
                                        "SET Descripciones = @descripciones, " +
                                        "Costo = @costo, " +
                                        "PrecioVenta = @precioVenta, " +
                                        "stock = @Stock, " +
                                        "IdUsuario = @idUsuario " +
                                        "WHERE Id = @id";

                
                var parameterId = new SqlParameter("id", SqlDbType.BigInt); 
                parameterId.Value = producto.Id;                            
                var parameterDescripciones = new SqlParameter("descripciones", SqlDbType.VarChar);
                parameterDescripciones.Value = producto.Descripciones;

                var parameterCosto = new SqlParameter("costo", SqlDbType.Money);
                parameterCosto.Value = producto.Costo;

                var parameterPrecioVenta = new SqlParameter("precioVenta", SqlDbType.Money);
                parameterPrecioVenta.Value = producto.PrecioVenta;

                var parameterStock = new SqlParameter("stock", SqlDbType.Int);
                parameterStock.Value = producto.Stock;

                var parameterIdUsuario = new SqlParameter("idUsuario", SqlDbType.BigInt);
                parameterIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open(); 

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))  
                {
                    
                    sqlCommand.Parameters.Add(parameterId); 
                    sqlCommand.Parameters.Add(parameterDescripciones);
                    sqlCommand.Parameters.Add(parameterCosto);
                    sqlCommand.Parameters.Add(parameterPrecioVenta);
                    sqlCommand.Parameters.Add(parameterStock);
                    sqlCommand.Parameters.Add(parameterIdUsuario);
                    rowsAffected = sqlCommand.ExecuteNonQuery(); 
                }
                sqlConnection.Close(); 
            }
            if (rowsAffected == 1) 
            {
                resultado = true; 
            }
            return resultado;
        }

        //Eliminar Producto

        public static bool EliminarProducto(long id)
        {
            
            if (id <= 0) 
            {
                return false;
            }

            bool resultado = false; 
            int rowsAffected = 0;   

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] " + 
                                        "WHERE Id = @id";

                var parameterId = new SqlParameter("id", SqlDbType.BigInt); 
                parameterId.Value = id;                                     

                sqlConnection.Open(); 

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection)) 
                {
                    sqlCommand.Parameters.Add(parameterId); 
                    rowsAffected = sqlCommand.ExecuteNonQuery(); 
                }
                sqlConnection.Close(); 
            }
            if (rowsAffected == 1) 
            {
                resultado = true; 
            }
            return resultado;
        }
    }
}