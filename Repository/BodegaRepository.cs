using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace WebApiData.Repository
{
    public class BodegaRepository
    {
        // Obtener todas las bodegas
        public List<Bodega> GetAllBodega()
        {
            List<Bodega> ListBodega = new List<Bodega>();
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllBodega", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListBodega.Add(new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["IdBodega"]),
                            Nombre = reader["Nombre"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString()
                        });
                    }
                }
                catch
                {
                    ListBodega = null;
                }
            }
            return ListBodega;
        }

        // Obtener una bodega por ID
        public Bodega GetBodegaById(int id)
        {
            Bodega bodega = new Bodega();
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetBodegaById", connection);
                    cmd.Parameters.AddWithValue("@IdBodega", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bodega.IdBodega = Convert.ToInt32(reader["IdBodega"]);
                        bodega.Nombre = reader["Nombre"].ToString();
                        bodega.Ubicacion = reader["Ubicacion"].ToString();
                    }
                }
                catch
                {
                    bodega = null;
                }
            }
            return bodega;
        }

        // Insertar nueva bodega
        public bool InsertBodega(Bodega bodega)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertBodega", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", bodega.Nombre);
                    cmd.Parameters.AddWithValue("@Ubicacion", bodega.Ubicacion);
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

        // Actualizar bodega
        public bool UpdateBodega(Bodega bodega)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateBodega", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdBodega", bodega.IdBodega);
                    cmd.Parameters.AddWithValue("@Nombre", bodega.Nombre);
                    cmd.Parameters.AddWithValue("@Ubicacion", bodega.Ubicacion);
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

        // Eliminar bodega
        public bool DeleteBodegaById(int id)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteBodegaById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdBodega", id);
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
