using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using BO;
namespace GUI
{
    public partial class Login : Form
    {
        public Jugador Jugador { get; set; }
        public Jugador Jugador1 { get; set; }
        bool Verifica = false;

        public JugadorBO JuBO;

        public Login()
        {
            InitializeComponent();      
        }
        /// <summary>
        /// btn que nos sirve para autentificar los dos jugadores 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Digite los datos Requeridos");
                }
                if (Verifica == false)
                {
                    Jugador = new Jugador()
                    {
                        Nombre = textBox1.Text,
                        Contrasena = textBox2.Text
                    };

                    Jugador = JuBO.Autentificar(Jugador);
                    if (Jugador.Id > 0)
                    {
                        MessageBox.Show("Jugador 1 Cargado, por favor Ingrese el segundo Jugador");
                        label1.Text = "Jugador 1: " + Jugador.Nombre;
                        Verifica = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox1.Select();
                    }
                }
                else
                {
                    Jugador1 = new Jugador()
                    {
                        Nombre = textBox1.Text,
                        Contrasena = textBox2.Text
                    };
                    Jugador1 = JuBO.Autentificar(Jugador1);
                    if (Jugador.Id > 0 && Jugador1.Id > 0)
                    {
                        if (Jugador.Id != Jugador1.Id)
                        {
                            frmJuego frmJu = new frmJuego()
                            {
                                jugador = Jugador,
                                jugador1 = Jugador1
                            };
                            this.Hide();
                            frmJu.Show(this);
                        }
                        else
                        {
                            MessageBox.Show("Tienes que ingresar con otro Jugador diferente al primero");
                        }


                    }
                }
            }
            catch (Exception q)
            {
                MessageBox.Show(q.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Registrarse reg = new Registrarse();
            this.Hide();
            reg.Show(this);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            JuBO = new JugadorBO();
        }
    }
}