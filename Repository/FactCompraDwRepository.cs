using DistribuidoraWalter.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebModel.DataWarehouse;

namespace WebApiData.Repository
{
    public class FactCompraDwRepository
    {
        // Obtener todas las compras desde el Data Warehouse
        public List<FactCompra> GetAllFactCompraDw()
        {
            List<FactCompra> lista = new List<FactCompra>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAll_Compras", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new FactCompra()
                        {
                            IdCompra = Convert.ToInt32(reader["IdCompra"]),
                            IdTiempo = reader["IdTiempo"] == DBNull.Value ? null : Convert.ToInt32(reader["IdTiempo"]),
                            IdProveedor = reader["IdProveedor"] == DBNull.Value ? null : Convert.ToInt32(reader["IdProveedor"]),
                            IdUsuario = reader["IdUsuario"].ToString(),
                            IdProducto = reader["IdProducto"] == DBNull.Value ? null : Convert.ToInt32(reader["IdProducto"]),
                            IdCategoria = reader["IdCategoria"] == DBNull.Value ? null : Convert.ToInt32(reader["IdCategoria"]),
                            IdMarca = reader["IdMarca"] == DBNull.Value ? null : Convert.ToInt32(reader["IdMarca"]),
                            IdUnidadMedida = reader["IdUnidadMedida"] == DBNull.Value ? null : Convert.ToInt32(reader["IdUnidadMedida"]),
                            IdBodega = reader["IdBodega"] == DBNull.Value ? null : Convert.ToInt32(reader["IdBodega"]),
                            Cantidad = reader["Cantidad"] == DBNull.Value ? null : Convert.ToDecimal(reader["Cantidad"]),
                            PrecioUnitario = reader["PrecioUnitario"] == DBNull.Value ? null : Convert.ToDecimal(reader["PrecioUnitario"]),
                            TotalCompra = reader["TotalCompra"] == DBNull.Value ? null : Convert.ToDecimal(reader["TotalCompra"]),
                            ETLLoad = reader["ETLLoad"] == DBNull.Value ? null : Convert.ToDateTime(reader["ETLLoad"]),
                            ETLIdExecution = reader["ETLIdExecution"] == DBNull.Value ? null : Convert.ToInt32(reader["ETLIdExecution"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener las compras del DW: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }

        // ✅ Nuevo método: obtener el gasto total de compras agrupado por año y mes
        public List<GastoTotalCompraDw> GetGastoTotalComprasDw()
        {
            List<GastoTotalCompraDw> lista = new List<GastoTotalCompraDw>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect("ConnectionStringDW")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GastoTotal_Compras", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new GastoTotalCompraDw()
                        {
                            Anio = Convert.ToInt32(reader["Anio"]),
                            Mes = Convert.ToInt32(reader["Mes"]),
                            GastoCompras = Convert.ToDecimal(reader["GastoCompras"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el gasto total de compras: " + ex.Message);
                    lista = null;
                }
            }

            return lista;
        }
    }
}
