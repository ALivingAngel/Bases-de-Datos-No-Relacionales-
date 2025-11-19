using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class BodegaDwRepository
    {
        // Obtener todas las bodegas del Data Warehouse
        public List<DimBodega> GetAllBodegasDw()
        {
            List<DimBodega> lista = new List<DimBodega>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Bodega", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DimBodega()
                        {
                            IdBodega = Convert.ToInt32(reader["IdBodega"]),
                            NombreBodega = reader["NombreBodega"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Tipo = reader["Tipo"].ToString(),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener las bodegas del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
