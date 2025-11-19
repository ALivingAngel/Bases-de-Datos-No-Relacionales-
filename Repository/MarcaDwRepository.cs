using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class MarcaDwRepository
    {
        // Obtener todas las marcas desde el Data Warehouse
        public List<DimMarca> GetAllMarcaDw()
        {
            List<DimMarca> lista = new List<DimMarca>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Marca", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DimMarca()
                        {
                            IdMarca = Convert.ToInt32(reader["IdMarca"]),
                            NombreMarca = reader["NombreMarca"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener las marcas del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
