using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class ClientDwRepository
    {
        // Obtener todos los clientes desde el Data Warehouse
        public List<DIM_ClientDw> GetAllClientDw()
        {
            List<DIM_ClientDw> lista = new List<DIM_ClientDw>();

            // Conexión al Data Warehouse
            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_CLIENT", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DIM_ClientDw()
                        {
                            CLientPK = Convert.ToInt32(reader["CLientPK"]),
                            ClientName = reader["ClientName"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLExecution = reader["ETLExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener los clientes del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
