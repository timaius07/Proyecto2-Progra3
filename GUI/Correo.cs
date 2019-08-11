using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

using BO;

namespace GUI
{
    public partial class Correo : Form
    {
        /// <summary>
        /// instancia para acceder al metodo de enviar
        /// </summary>
        /// 
        CorreoBO co = new CorreoBO();
        /// <summary>
        /// contiene el mensaje del top5
        /// </summary>
        public string Body { get; set; }
  
        public Correo()
        {
            InitializeComponent();
        }
     
    private void Correo_Load(object sender, EventArgs e)
        {
            txtBody.Text = Body;
            Body = "";
            for (int i = 0; i < txtBody.Lines.Count(); i++)
            {
                Body += txtBody.Lines[i];
            }
        }

        /// <summary>
        /// va al metodo para enviar el mensaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            co.EnviarCorreo( Body, txtasunto.Text, txtdestinatario.Text,txtRuta.Text );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.Equals("")==false)
                {
                    txtRuta.Text = openFileDialog1.FileName;
                }

            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }
    }
}
