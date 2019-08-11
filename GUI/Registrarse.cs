using BO;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Registrarse : Form
    {
        /// <summary>
        /// contiene los datos del jugador
        /// </summary>
        public Jugador jugador { get; set; }
        JugadorBO jubo = new JugadorBO();
        public Registrarse()
        {
            InitializeComponent();
        }
        /// <summary>
        /// importa una imagen de la compu para agregarla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {    // Carga la imagen dentro del cuadro picture box. 
                PBMuestraFoto.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }
        /// <summary>
        /// btn que guarda los datos de jugado en y los manda al BO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Imagen ima = new Imagen()
                {
                    Foto = PBMuestraFoto.Image
                };

                jugador = new Jugador()
                {
                    Nombre = txtNombre.Text,
                    Contrasena = txtContraseña.Text,
                    correo = txtCorreo.Text,
                    telefono = txtTelefono.Text,

                };
                jugador.Imagen = ima;
                if (jubo.VerificaDatos(txtBoxConfiContraseña.Text, jugador))
                {
                    jubo.RegistrarJugador(jugador);
                    MessageBox.Show("¡El jugador fue creado correctamente!");
                }
                else
                {
                    MessageBox.Show("¡El jugador NO fue guardado!");

                }
                if (jugador.Id != 0)
                {
                    SetDatosEnBlanco();
                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.Message);
            }


        }
        /// <summary>
        /// pone en blanco todos los texbox
        /// </summary>
        private void SetDatosEnBlanco()
        {

            txtBoxConfiContraseña.Text = "";
            txtContraseña.Text = "";
            txtCorreo.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void Registrarse_Load(object sender, EventArgs e)
        {

        }
    }
}
