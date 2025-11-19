using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.Repository
{
    public class ConversionRepository
    {
        public List<Conversion> ObtenerConversiones()
        {
            List<Conversion> listaConversiones = new List<Conversion>();

            using (SqlConnection connection = new SqlConnection(DBConnection.Connect()))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Conversiones.ObtenerConversiones", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        listaConversiones.Add(new Conversion()
                        {
                            IdConversion = Convert.ToInt32(reader["IdConversion"]),
                            IdUnidadDesde = Convert.ToInt32(reader["IdUnidadDesde"]),
                            IdUnidadHasta = Convert.ToInt32(reader["IdUnidadHasta"]),
                            Factor = Convert.ToDecimal(reader["Factor"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Log error si deseas
                    listaConversiones = null;
                }
            }

            return listaConversiones;
        }
    }
}
