using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Npgsql;
using System.Windows.Forms;

namespace DAO
{
    public class JugadorDAO
    {/// <summary>
    /// instancia la para acceder a metodos de la clase imagendao
    /// </summary>
        ImagenDAO ima;
        /// <summary>
        /// variable para saber si realizo de la mejor menera
        /// </summary>
        private bool verifica = false;

        public JugadorDAO()
        {
            ima = new ImagenDAO();
        }
        /// <summary>
        /// verifica si el juga ya a sido registrado
        /// </summary>
        /// <param name="jugador">contiene el nombre y la contraseña</param>
        /// <returns></returns>
        public Jugador AutentificaJugador(Jugador jugador)
        {

            Jugador J = new Jugador();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))

                {
                    con.Open();
                    string sql = @"select 
                              id,contrasena,id_imagen,nombre,telefono,correo
                               from 
                               jugador
                               where 
                               nombre = :nom and contrasena= :con  ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("nom", jugador.Nombre);
                    cmd.Parameters.AddWithValue("con", jugador.Contrasena);

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        J.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);

                        J.Contrasena = reader.GetString(1);
                        ImagenDAO idao = new ImagenDAO();
                        J.Imagen = reader.IsDBNull(2) ? new Imagen() : idao.CargarFoto(reader.GetInt32(2));
                        J.Nombre = reader.GetString(3);
                        J.telefono = reader.GetString(4);
                        J.correo = reader.GetString(5);

                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }



            return J;
        }
        /// <summary>
        /// carga en una lista todos los jugadores
        /// </summary>
        /// <returns>devuelve una lista de todos los jugadores</returns>
        public List<Jugador> cargarJugadores()
        {
            Jugador jugador = new Jugador();
            List<Jugador> listJu = new List<Jugador>();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    con.Open();
                    string sql = @"SELECT id, id_imagen, contrasena, nombre, telefono, correo 
                     FROM public.jugador;";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        jugador = new Jugador();
                        jugador.Id = reader.GetInt32(0);
                        jugador.Imagen = reader.IsDBNull(1) ? new Imagen() :   ima.CargarFoto(reader.GetInt32(1));
                        jugador.Contrasena = reader.GetString(2);
                        jugador.Nombre = reader.GetString(3);
                        jugador.telefono = reader.GetString(4);
                        jugador.correo = reader.GetString(5);

                        listJu.Add(jugador);  
                    }
                   

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return listJu;
        }
        /// <summary>
        /// carga un jugador en especifico
        /// </summary>
        /// <param name="id">id del jugador</param>
        /// <returns></returns>
        public Jugador cargarJugadore(int id)
        {
            Jugador J = new Jugador();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))

                {
                    con.Open();
                    string sql = @"select nombre , telefono , correo from  jugador  where  id = :id_ju ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("id_ju", id);

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        J.Nombre = reader.GetString(0);
                        J.telefono = reader.GetString(1);
                        J.correo = reader.GetString(2);

                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return J;

        }
        /// <summary>
        /// inserta los dotos de un nuevo jugador 
        /// </summary>
        /// <param name="jugador">contiene los datos de un jugador</param>
        /// <returns>true o false </returns>
        public bool RegistrarJugador(Jugador jugador)
        {
            NpgsqlTransaction tran = null;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    con.Open();
                    tran = con.BeginTransaction();

                    jugador.Imagen.Id = ima.InsertarFoto(jugador.Imagen, con);
                    string sql = @"insert into jugador  
                              ( contrasena, id_imagen, nombre, telefono, correo)
                              values( :con, :ima, :nom, :tele, :cor)RETURNING id; ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("con", jugador.Contrasena);
                    cmd.Parameters.AddWithValue("ima", jugador.Imagen.Id);
                    cmd.Parameters.AddWithValue("nom", jugador.Nombre);
                    cmd.Parameters.AddWithValue("tele", jugador.telefono);
                    cmd.Parameters.AddWithValue("cor", jugador.correo);
                    jugador.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    if (jugador.Id != 0)
                    {
                        verifica = true;
                    }
                    tran.Commit();
                    con.Close();
                }




            }
            catch (Exception E)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                MessageBox.Show(E.Message);
            }
            return verifica;
        }
    }
}
