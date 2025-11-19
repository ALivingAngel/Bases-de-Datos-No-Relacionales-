using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace WebApiData.Repository
{
    public class ProductRepository
    {
        // Obtener todos los productos
        public List<Product> GetAllProduct()
        {
            List<Product> ListProduct = new List<Product>();
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListProduct.Add(new Product()
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                            IdMarca = Convert.ToInt32(reader["IdMarca"]),
                            IdUnidadMedida = Convert.ToInt32(reader["IdUnidadMedida"])
                        });
                    }
                }
                catch
                {
                    ListProduct = null;
                }
            }
            return ListProduct;
        }

        // Obtener producto por ID
        public Product GetProductById(int idProducto)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetProductById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new Product()
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                            IdMarca = Convert.ToInt32(reader["IdMarca"]),
                            IdUnidadMedida = Convert.ToInt32(reader["IdUnidadMedida"])
                        };
                    }
                }
                catch
                {
                    product = null;
                }
            }
            return product;
        }

        // Insertar nuevo producto
        public bool InsertNewProduct(Product product)
        {
            bool result;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", product.IdCategoria);
                    cmd.Parameters.AddWithValue("@IdMarca", product.IdMarca);
                    cmd.Parameters.AddWithValue("@Nombre", product.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", product.Descripcion);
                    cmd.Parameters.AddWithValue("@IdUnidadMedida", product.IdUnidadMedida);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        // Actualizar producto
        public bool UpdateProductById(Product product)
        {
            bool result;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", product.IdProducto);
                    cmd.Parameters.AddWithValue("@IdCategoria", product.IdCategoria);
                    cmd.Parameters.AddWithValue("@IdMarca", product.IdMarca);
                    cmd.Parameters.AddWithValue("@Nombre", product.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", product.Descripcion);
                    cmd.Parameters.AddWithValue("@IdUnidadMedida", product.IdUnidadMedida);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        // Eliminar producto por ID
        public bool DeleteProductById(int idProducto)
        {
            bool result;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteProductById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
