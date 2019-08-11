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

        CorreoBO co = new CorreoBO();
        public string Body { get; set; }
  
        public Correo()
        {
            InitializeComponent();
        }
     
    private void Correo_Load(object sender, EventArgs e)
        {
            txtBody.Text=(Body); 
            txtBody.Enabled = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            co.EnviarCorreo(txtemisor.Text, txtcontrasena.Text,Body,txtasunto.Text, txtdestinatario.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }
    }
}
