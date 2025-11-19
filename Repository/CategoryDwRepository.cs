using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebApiDatas.DTO.DTOAnalytics;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class CategoryDwRepository
    {
        // Obtener todas las categorías desde el Data Warehouse
        public List<CategoryDw> GetAllCategoryDw()
        {
            List<CategoryDw> lista = new List<CategoryDw>();

            // Conexión al Data Warehouse
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Category", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new CategoryDw()
                        {
                            CategoryPK = Convert.ToInt32(reader["CategoryPK"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener las categorías del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
