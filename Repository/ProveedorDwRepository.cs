using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class ProveedorDwRepository
    {
        // Obtener todos los proveedores desde el Data Warehouse
        public List<DimProveedor> GetAllProveedorDw()
        {
            List<DimProveedor> lista = new List<DimProveedor>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Proveedor", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DimProveedor()
                        {
                            IdProveedor = Convert.ToInt32(reader["IdProveedor"]),
                            NombreProveedor = reader["NombreProveedor"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener los proveedores del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }

        // Obtener proveedores por mayor gasto
        public List<ProveedorMayorGasto> GetProveedorMayorGastoDw()
        {
            List<ProveedorMayorGasto> lista = new List<ProveedorMayorGasto>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_Proveedor_MayorGasto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new ProveedorMayorGasto()
                        {
                            NombreProveedor = reader["NombreProveedor"].ToString(),
                            GastoTotal = Convert.ToDecimal(reader["GastoTotal"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener proveedores por mayor gasto: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }

}
