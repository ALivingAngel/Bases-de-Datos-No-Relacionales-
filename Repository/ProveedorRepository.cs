using DistribuidoraWalter.Model;
using System.Data;
using System.Data.SqlClient;

namespace DistribuidoraWalter.Data.Repository
{
    public class ProveedorRepository
    {
        // Método para obtener todos los clientes
        public List<Proveedor> GetAllProveedor()
        {
            List<Proveedor> listProveedor = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllProveedor", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listProveedor.Add(new Proveedor()
                        {
                            IdProveedor = Convert.ToInt32(dataReader["IdProveedor"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Contacto = dataReader["Contacto"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString()
                        });
                    }

                    return listProveedor;
                }
                catch (Exception ex)
                {
                    // Log the error here (optional)
                    return null;
                }
            }
        }

        // Método para obtener un cliente por su ID
        public Proveedor GetProveedorById(int id)
        {
            Proveedor Proveedor = null;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetProveedorById", connection);
                    cmd.Parameters.AddWithValue("IdProveedor", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        Proveedor = new Proveedor()
                        {
                            IdProveedor = Convert.ToInt32(dataReader["IdProveedor"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Contacto = dataReader["Contacto"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    Proveedor = null;
                    // Log the error here (optional)
                }
            }

            return Proveedor;
        }


        public bool InsertNewProveedor(Proveedor Proveedor)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewProveedor", connection);
                    cmd.Parameters.AddWithValue("@Nombre", Proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@Contacto", Proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@Correo", Proveedor.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", Proveedor.Telefono);
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


        public bool DeleteProveedorById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteProveedorById", connection);
                    cmd.Parameters.AddWithValue("@IdProveedor", id);
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

        public bool UpdateProveedor(Proveedor Proveedor)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateProveedor", connection);
                    cmd.Parameters.AddWithValue("@IdProveedor", Proveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@Nombre", Proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@Contacto", Proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@Correo", Proveedor.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", Proveedor.Telefono);
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
