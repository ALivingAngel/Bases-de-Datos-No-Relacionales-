
using DistribuidoraWalter.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribuidoraWalter.Data.Repository
{
    public class CategoriaRepository
    {
        public List<Categoria> GetAllCategoria()
        {
            List<Categoria> listCategoria = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllCategoria", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listCategoria.Add(new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dataReader["IdCategoria"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                        });
                    }

                    return listCategoria;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public Categoria GetCategoriaById(int id)
        {
            Categoria Categoria = null;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetCategoriaById", connection);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        Categoria = new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dataReader["IdCategoria"].ToString()),
                            Nombre = dataReader["nombre"].ToString(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    Categoria = null;
                    // Log the error here (optional)
                }
            }

            return Categoria;
        }

        public bool InsertNewCategoria(Categoria Categoria)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewCategoria", connection);
                    cmd.Parameters.AddWithValue("@Nombre", Categoria.Nombre);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch (Exception ex)
                {
                    result = false;
                    // Log the error here (optional)
                }
            }

            return result;
        }

        public bool DeleteCategoriaById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteCategoriaById", connection);
                    cmd.Parameters.AddWithValue("@IdCategoria", id);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch (Exception ex)
                {
                    result = false;
                    // Log the error here (optional)
                }
            }

            return result;
        }


        public bool UpdateCategoria(Categoria Categoria)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateCategoria", connection);
                    cmd.Parameters.AddWithValue("@IdCategoria", Categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Nombre", Categoria.Nombre);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch (Exception ex)
                {
                    result = false;
                    // Log the error here (optional)
                }
            }

            return result;
        }
    }
}
