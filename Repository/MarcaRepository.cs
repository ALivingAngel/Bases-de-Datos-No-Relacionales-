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
    public class MarcaRepository
    {
        public List<Marca> GetAllMarcas()
        {
            List<Marca> listMarca = new List<Marca>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllMarcas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listMarca.Add(new Marca()
                        {
                            IdMarca = Convert.ToInt32(dataReader["IdMarca"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                        });
                    }

                    return listMarca;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public Marca GetMarcaById(int id)
        {
            Marca Marca = null;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetMarcaById", connection);
                    cmd.Parameters.AddWithValue("IdMarca", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        Marca = new Marca()
                        {
                            IdMarca = Convert.ToInt32(dataReader["IdMarca"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    Marca = null;
                    // Log the error here (optional)
                }
            }

            return Marca;
        }

        public bool InsertNewMarca(Marca Marca)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewMarca", connection);
                    cmd.Parameters.AddWithValue("@Nombre", Marca.Nombre);
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

        // Método para eliminar un cliente por su ID
        public bool DeleteMarcaById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteMarcaById", connection);
                    cmd.Parameters.AddWithValue("@IdMarca", id);
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


        public bool UpdateMarca(Marca Marca)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateMarca", connection);
                    cmd.Parameters.AddWithValue("@IdMarca", Marca.IdMarca);
                    cmd.Parameters.AddWithValue("@Nombre", Marca.Nombre);
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
