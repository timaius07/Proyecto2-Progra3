using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BO
{

    public class JugadorBO
    {/// <summary>
    /// variable que se compara co el correo para verficar si esta bien.
    /// </summary>
        string Corr = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        JugadorDAO JuDAO;

        public JugadorBO()
        {
            JuDAO = new JugadorDAO();
        }

       
        /// <summary>
        /// autentifica si existe el usuarios digitado en en el frm
        /// </summary>
        /// <param name="jugador">trae la contra y el nombre para autentificar</param>
        /// <returns></returns>
        public Jugador Autentificar(Jugador jugador)
        {
            return JuDAO.AutentificaJugador(jugador);
        }
        /// <summary>
        /// registra el usuario que quiere crear el usuario
        /// </summary>
        /// <param name="jugador">lleva los datos de un nuevo jugador</param>
        public void RegistrarJugador(Jugador jugador)
        {
            JuDAO.RegistrarJugador(jugador);
        }
        /// <summary>
        /// carga todos los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>devuelve un lista con los jugadores
        public List<Jugador> CargarUsuarios()
        {
            return JuDAO.cargarJugadores();
        }
        /// <summary>
        /// verifica si los datos va de la mejor manera
        /// </summary>
        /// <param name="txtBoxConfiContraseña">verifica que la contra son iguale</param>
        /// <param name="jugador">trae los datos nuevos del jugador</param>
        /// <returns></returns>
        public bool VerificaDatos(string txtBoxConfiContraseña, Jugador jugador)
        {      bool verifica = true;
            try
            {
                
                if ((jugador.Nombre == " "))
                {
                    verifica = false;
                    MessageBox.Show("Complete los datos usuarios o Nombre ");
                }
                else if (!(jugador.Contrasena == " " || txtBoxConfiContraseña == " " || jugador.Contrasena == txtBoxConfiContraseña))
                {
                    MessageBox.Show("Confirme la contraseña ",
                        "Contraseña", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    verifica = false;
                }
                else if (jugador.Imagen == null)
                {
                    MessageBox.Show("Aceptar",
        "Tines que Agregar una imagen", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                    verifica = false;
                }
                else if (!Regex.IsMatch(jugador.correo, Corr))
                {
                    MessageBox.Show("Confirme el correo",
        "Correo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                    verifica = false;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

                return verifica;
        }
        /// <summary>
        /// trae un jugador con el id 
        /// </summary>
        /// <param name="id">id del jugador</param>
        /// <returns></returns>devuelve un jugador
        public Jugador CargarJugador(int id)
        {
            return JuDAO.cargarJugadore(id);
        }
    }
}
