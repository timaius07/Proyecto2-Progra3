using Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class VictoriaDAO
    {
        /// <summary>
        /// guarda una victoria en la base de datos
        /// </summary>
        /// <param name="resul">tiempo en segundos</param>
        /// <param name="id_jugador">id del jugador</param>
        public void GuardarVictoria(int resul, int id_jugador)
        {

            // bool Verifica = false;
            Victoria Vict = new Victoria();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    con.Open();
                    string sql = @"INSERT INTO public.jugador_victoria(id_jugador, tiempo)  VALUES ( :id_jugador, :tiempo ) RETURNING id; ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue(":id_jugador", id_jugador);
                    cmd.Parameters.AddWithValue(":tiempo", resul);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// lista de todas las victorias 
        /// </summary>
        /// <returns>lista de todas las victorias</returns>
        public List<Victoria> CargarTop5Victoria()
        {
            List<Victoria> livi = new List<Victoria>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    con.Open();
                    string sql = @"SELECT  id_jugador,tiempo FROM public.jugador_victoria;";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Victoria vi = new Victoria()
                        {
                            id_jugador = reader.GetInt32(0),
                            SegundoTotales = reader.GetInt32(1)
                        };

                        livi.Add(vi);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return livi;

        }



        public List<Victoria> CargarVictoria_Jugador(int id)
        {
            List<Victoria> livi = new List<Victoria>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    con.Open();
                    string sql = @"SELECT  id_jugador,tiempo FROM public.jugador_victoria;";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Victoria vi = new Victoria()
                        {
                            id_jugador = reader.GetInt32(0),
                            SegundoTotales = reader.GetInt32(1)

                        };

                        if (vi.id_jugador == id)
                        {
                            livi.Add(vi);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            return livi;
        }


    }
}
