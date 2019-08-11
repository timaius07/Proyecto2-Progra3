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
    public partial class VictoriaJugadores : Form
    {/// <summary>
    /// // contiene el promedio de los los tiempos del jugador
    /// </summary>
        int resulSegundos = 0; 
        /// <summary>
        /// // variable para acceder a la logica
        /// </summary>
        public JugadorBO juBO; 
        /// <summary>
        /// //variable para almacenar informacion de la cada victoeria
        /// </summary>
        public Victoria vic;
        /// <summary>
        /// //lista de todos las victorias
        /// </summary>
        List<Victoria> ListVic;
        /// <summary>
        /// // almacena el top5 de las victorias
        /// </summary>
        string Top5Compartir = "";

        VictoriaBO VicBO;

        public VictoriaJugadores()
        {
            ListVic = new List<Victoria>();
            VicBO = new VictoriaBO();
            vic = new Victoria();
            juBO = new JugadorBO();
            InitializeComponent();
        }

        private void VictoriaJugadores_Load(object sender, EventArgs e)
        {
            DataGrJugadores.DataSource = juBO.CargarUsuarios();
            // oculta las columnas que no son inecesarias
            this.DataGrJugadores.Columns[0].Visible = false;
            this.DataGrJugadores.Columns[2].Visible = false;
            this.DataGrJugadores.Columns[3].Visible = false;
            this.DTGRTop5.Rows.Add();
            this.DTGRTop5.Rows.Add();
            this.DTGRTop5.Rows.Add();
            this.DTGRTop5.Rows.Add();
            Top5();

        }
/// <summary>
/// metodo encargado de extraer los 5 mejores partidas que pudieron resolver en el menor tiempo posible.
/// </summary>
        public void Top5()
        {
            Victoria[] vecV = new Victoria[5];
            int segundos = 0;
            string tiempoT = "";
            // llsta con todas lass victorias de los jugadores
            ListVic = VicBO.CargarTop5Victorias();
            // vector que almacena los 5 mejores jugadores
            vecV = VicBO.Top5(ListVic);
            try
            {
                for (int i = 0; i < vecV.Length; i++)
                {
                    if (i == 0)
                    {
                        Jugador ju = new Jugador();
                        ju.Id = vecV[i].id_jugador;
                        segundos = vecV[i].SegundoTotales;
                        tiempoT = VicBO.ConvertirTiempo(segundos);
                        ju = juBO.CargarJugador(ju.Id);
                        Top5Compartir = "Nombre" + " y " + "Tiempo:  " + "\n" + ju.Nombre + ",    " + tiempoT+" -  ";
                        DTGRTop5.Rows[0].Cells[0].Value = "1: "+ju.Nombre;
                        DTGRTop5.Rows[0].Cells[1].Value = tiempoT;
                    }
                    if (i == 1)
                    {
                        Jugador ju = new Jugador();
                        ju.Id = vecV[i].id_jugador;
                        segundos = vecV[i].SegundoTotales;
                        tiempoT = VicBO.ConvertirTiempo(segundos);
                        ju = juBO.CargarJugador(ju.Id);
                        Top5Compartir +=  "\n" + ju.Nombre + ",     " + tiempoT + " -  ";
                        DTGRTop5.Rows[1].Cells[0].Value ="2: "+ ju.Nombre;
                        DTGRTop5.Rows[1].Cells[1].Value = tiempoT;
                    }
                    if (i == 2)
                    {
                        Jugador ju = new Jugador();
                        ju.Id = vecV[i].id_jugador;
                        segundos = vecV[i].SegundoTotales;
                        tiempoT = VicBO.ConvertirTiempo(segundos);
                        ju = juBO.CargarJugador(ju.Id);
                        Top5Compartir += "\n" + ju.Nombre + ",     " + tiempoT + " -  ";
                        DTGRTop5.Rows[2].Cells[0].Value = "3: "+ju.Nombre;
                        DTGRTop5.Rows[2].Cells[1].Value = tiempoT;
                    }
                    if (i == 3)
                    {
                        Jugador ju = new Jugador();
                        ju.Id = vecV[i].id_jugador;
                        segundos = vecV[i].SegundoTotales;
                        tiempoT = VicBO.ConvertirTiempo(segundos);
                        ju = juBO.CargarJugador(ju.Id);
                        Top5Compartir += "\n" + ju.Nombre + ",     " + tiempoT + " -  ";
                        DTGRTop5.Rows[3].Cells[0].Value = "4: "+ju.Nombre;
                        DTGRTop5.Rows[3].Cells[1].Value = tiempoT;
                    }
                    if (i == 4)
                    {
                        Jugador ju = new Jugador();
                        ju.Id = vecV[i].id_jugador;
                        segundos = vecV[i].SegundoTotales;
                        tiempoT = VicBO.ConvertirTiempo(segundos);
                        ju = juBO.CargarJugador(ju.Id);
                        Top5Compartir += "\n" + ju.Nombre + ",     " + tiempoT + " -  ";
                        DTGRTop5.Rows[4].Cells[0].Value ="5: "+ ju.Nombre;
                        DTGRTop5.Rows[4].Cells[1].Value = tiempoT;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// boton que trae los datos de la victoria del cada participante y los muestra en un datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Victoria victoria = new Victoria();
            int cont = 0;
            int suma = 0;
            int id = 0;
            try
            {
                List<Victoria> lisVic = new List<Victoria>();
                id = Convert.ToInt32(DataGrJugadores.CurrentRow.Cells["Id"].Value);
                if (id != 0)
                {

                    lisVic = VicBO.CargarVictoria_Jugador(id);
                    // for que trae el tiempo de la victoria 
                    for (int i = 0; i < lisVic.Count; i++)
                    {

                        vic = lisVic[i];
                        suma += vic.SegundoTotales;
                        cont += 1;
                    }
                    resulSegundos = suma / cont;

                    string num = VicBO.ConvertirTiempo(resulSegundos);

                    DTGDatosJug.Rows[0].Cells[0].Value = cont;

                    DTGDatosJug.Rows[0].Cells[1].Value = num;
                }
                else
                {
                    MessageBox.Show("Debes seleccionar un Jugador");
                }
            }
            catch (Exception q)
            {
                MessageBox.Show(q.Message);
                DTGDatosJug.Rows[0].Cells[0].Value = 0;

                DTGDatosJug.Rows[0].Cells[1].Value = 0;
            }


        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DTGRTop5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }
        /// <summary>
        /// abre un frm para compartir el to5 por correo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCorreo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Correo cor = new Correo()
            {
                Body = Top5Compartir
            };
            cor.Show(this);
        }
    }
}
