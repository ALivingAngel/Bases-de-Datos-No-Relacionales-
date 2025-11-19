
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
    public class ClienteRepository
    {
        // Método para obtener todos los clientes
        public List<Cliente> GetAllClientes()
        {
            List<Cliente> listCliente = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllCliente", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listCliente.Add(new Cliente()
                        {
                            IdCliente = Convert.ToInt32(dataReader["IdCliente"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString()
                        });
                    }

                    return listCliente;
                }
                catch (Exception ex)
                {
                    // Log the error here (optional)
                    return null;
                }
            }
        }

        // Método para obtener un cliente por su ID
        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetClienteById", connection);
                    cmd.Parameters.AddWithValue("IdCliente", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        cliente = new Cliente()
                        {
                            IdCliente = Convert.ToInt32(dataReader["IdCliente"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    cliente = null;
                    // Log the error here (optional)
                }
            }

            return cliente;
        }

        // Método para insertar un nuevo cliente
        public bool InsertNewCliente(Cliente cliente)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewCliente", connection);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
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
        public bool DeleteClienteById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteClienteById", connection);
                    cmd.Parameters.AddWithValue("@IdCliente", id);
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

        // Método para actualizar un cliente
        public bool UpdateCliente(Cliente cliente)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateCliente", connection);
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
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
