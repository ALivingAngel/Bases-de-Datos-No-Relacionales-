using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;
using static WebModel.DataWarehouse.CostoPromedioProducto;

namespace WebApiData.Repository
{
    public class ProductDwRepository
    {
        // Obtener todos los productos desde el Data Warehouse
        public List<DIM_Product> GetAllProductDw()
        {
            List<DIM_Product> lista = new List<DIM_Product>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DIM_Product()
                        {
                            ProductPK = Convert.ToInt32(reader["ProductPK"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductDescription = reader["ProductDescription"].ToString(),
                            CategoryCod = reader["CategoryCod"] == DBNull.Value ? null : Convert.ToInt32(reader["CategoryCod"]),
                            BrandCod = reader["BrandCod"] == DBNull.Value ? null : Convert.ToInt32(reader["BrandCod"]),
                            MeasureCod = reader["MeasureCod"] == DBNull.Value ? null : Convert.ToInt32(reader["MeasureCod"]),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener los productos del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }

        // Obtener el costo promedio de todos los productos
        public List<CostoPromedioProducto> GetCostoPromedioProductosDw()
        {
            List<CostoPromedioProducto> lista = new List<CostoPromedioProducto>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_Costo_PromedioProducto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new CostoPromedioProducto()
                        {
                            ProductName = reader["ProductName"].ToString(),
                            TotalUnidades = Convert.ToInt32(reader["TotalUnidades"]),
                            GastoTotal = Convert.ToDecimal(reader["GastoTotal"]),
                            CostoPromedioUnitario = Convert.ToDecimal(reader["CostoPromedioUnitario"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el costo promedio de productos: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }

        // Obtener unidades vendidas por producto
        public List<UnidadesVendidasProducto> GetUnidadesVendidasXProductoDw()
        {
            List<UnidadesVendidasProducto> lista = new List<UnidadesVendidasProducto>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_Unidades_VendidasXProducto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new UnidadesVendidasProducto()
                        {
                            ProductName = reader["ProductName"].ToString(),
                            UnidadesVendidas = Convert.ToInt32(reader["UnidadesVendidas"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener unidades vendidas por producto: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
