using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BO;
using GUI.Properties;
using System.Threading;
using Entidades;

namespace GUI
{
    public partial class frmJuego : Form

    {
        DateTime Resul;   //variable con el tiempo actualizado del jugador2
        DateTime Resul1; // variable con el tiempo actualizado del jugador1
        int h; // variable para el control del jug 1
        int m;// variable para el control del tiempo jug 1
        int s;// variable para el control del tiempo jug 1
        int h1; // variable para el control del tiempo jug 2
        int m1;// variable para el control del tiempo jug 2
        int s1;// variable para el control del tiempo jug 2k
        int fila=0; //variable que contiene la fila en el tablero
        int colum=0; // variable que contiene la columna del tablero
        DateTime tie = DateTime.Now;
        DadoBO dbo;
        CuadroBO cbo;
        int numdado = 0;    
        /// <summary>
        /// variable que recibe un jugador el cual es el que se logea
        /// </summary>
        public Jugador jugador { get; set; }
        /// <summary>
        ///variable que recibe un jugador el cual es el que se logea
        /// </summary>
        public Jugador jugador1 { get; set; }
        List<Image> listaPictB = new List<Image>();
        List<int> listaNumPos = new List<int>();
        List<int> listaNumPos2 = new List<int>();
        List<bool> listMarcados = new List<bool>();
        int numero = 0;
        Random rnd = new Random();
        bool iniciarjuego = false;
        //impide que se pueda cambiar la ficha en el dragdrop
        bool cambiojug = true;
        //valida error al colocar la ficha correcta
        bool validarerror = false;
        int[] numeros = new int[] { 2, 4, 6, 8, 10, 12 };
        public frmJuego()

        {
            InitializeComponent();
            dbo = new DadoBO();
            cbo = new CuadroBO();
        }
        /// <summary>
        /// Carga el formulario y habilta las opciones de drag and drop
        /// para los picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmJuego_Load(object sender, EventArgs e)
        {
            timHora.Start();
            this.Inicializar_DragDrop();
            listaPictB = new List<Image>();
            pictJug1.AllowDrop = true;
            pictJug2.AllowDrop = true;
            pictTurno.AllowDrop = true;
            cbo.Llenar_Cuadro();
            timHora.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            validarerror = false;
            Activar_Allow_Drop();
           // this.InicializarTimerJug1();
            this.Random_Jug();

            if (pictTurno.Image == pictJug1.Image)
            {
                timJug1.Start();
            }
            else
            {
                timJug2.Start();
            }


            for (int i = 0; i < 20; i++)
            {
                numdado = dbo.LanzarDado();
                pictBoxDado.Image = (Image)Resources.ResourceManager.GetObject("dado_" + numdado);              
                Refresh();
                Thread.Sleep(i * 15);
            }
            numero = numdado * 2;
            pictTurno.Enabled = true;
            iniciarjuego = false;
            

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            iniciarjuego = true;
          //  this.Random_Jug();
            this.Insertar_ImageList();
            this.Lista_Random();
            btn1.Enabled = false;
            btnDado.Enabled = true;
        }

        /// <summary>
        /// Metodo que realiza un random en el BO dado para 
        /// saber cual jugador inicia la partida
        /// </summary>
        private void Random_Jug()
        {
           
            if (iniciarjuego == true)
            {
                int jug = dbo.Revolver_Jugador();
                if (jug == 0 || jug == 3 || jug == 5 || jug == 8)
                      {
                       pictTurno.Image = pictJug1.Image;
                       timJug1.Start();
                       }
                       else
                       {
                        pictTurno.Image = pictJug2.Image;
                        timJug2.Start();
                }
            }
            else
            {
                if (pictTurno.Image == pictJug1.Image)
                {
                    pictTurno.Image = pictJug2.Image;
                    timJug2.Start();
                    timJug1.Stop();
                }
                else
                {
                    pictTurno.Image = pictJug1.Image;
                    timJug1.Start();
                    timJug2.Stop();
                }
            }

        }

        /// <summary>
        /// Metodo encargado de crear un Random con los numero 
        /// y colocar las fichas aleatoriamente
        /// </summary>
        private void Insertar_ImageList()
        {
            Image nuevnum;
            /*metodo random en el BO Dado que genera un numero
            *y se valida para entrar en uno de los vectores con numeros
            *acomodados de manera desordenada para crear un random de las 
            *imagenes y luego colocarlas en los picturebox
            */
            int aleatorio = dbo.Revolver_Fichas();
            if (aleatorio == 0)
            {
                numeros = new int[] { 2, 4, 6, 8, 10, 12};
            }else if (aleatorio == 1)
            {
                numeros = new int[] { 12, 10, 8, 6, 4, 2};
            }else if (aleatorio == 2)
            {
               numeros = new int[] { 6, 4, 8, 12, 2, 10};
            }else if (aleatorio == 3)
            {
               numeros = new int[] { 10, 8, 12, 2, 4, 6};
            }else if (aleatorio == 4)
            {
                numeros = new int[] { 4, 12, 8, 10, 6, 2};
            }else if (aleatorio == 5)
            {
                numeros = new int[] { 8, 2, 6, 12, 10, 4};
            }else if (aleatorio == 6)
            {
                numeros = new int[] { 10, 6, 8, 4, 12, 2 };
            }

            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    nuevnum = (Image)Resources.ResourceManager.GetObject("num" + numeros[j]); ;
                    listaPictB.Add(nuevnum);     
                    listaNumPos.Add(numeros[j]);
                    listMarcados.Add(true);
                   // Tablero[]
                }
                

            }



        }
        /// <summary>
        /// Metodo encargado de validar si los pictureBox
        /// se puede usar como destino de una operación de arrastrar y colocar
        /// </summary>
        public void Activar_Allow_Drop()
        {
            pic1.AllowDrop = true;
            pic2.AllowDrop = true;
            pic3.AllowDrop = true;
            pic4.AllowDrop = true;
            pic5.AllowDrop = true;
            pic6.AllowDrop = true;
            pic7.AllowDrop = true;
            pic8.AllowDrop = true;
            pic9.AllowDrop = true;
            pic10.AllowDrop = true;
            pic11.AllowDrop = true;
            pic12.AllowDrop = true;
            pic13.AllowDrop = true;
            pic14.AllowDrop = true;
            pic15.AllowDrop = true;
            pic16.AllowDrop = true;
            pic17.AllowDrop = true;
            pic18.AllowDrop = true;
            pic19.AllowDrop = true;
            pic20.AllowDrop = true;
            pic21.AllowDrop = true;
            pic22.AllowDrop = true;
            pic23.AllowDrop = true;
            pic24.AllowDrop = true;
            pic25.AllowDrop = true;
            pic26.AllowDrop = true;
            pic27.AllowDrop = true;
            pic28.AllowDrop = true;
            pic29.AllowDrop = true;
            pic30.AllowDrop = true;
        }
        /// <summary>
        /// Metodo que contiene una lista con imagenes listaPictB y
        /// otra con los numeros insertados en dichas posiciones listaNumPos
        /// se asigna las posiciones manualmente en ambas listas
        /// </summary>
        private void Lista_Random()
        {
            //primera fila
            pic1.Image = listaPictB[0]; //2
            listaNumPos2.Add(listaNumPos[0]);
            pic2.Image = listaPictB[25];//12
            listaNumPos2.Add(listaNumPos[25]);
            pic3.Image = listaPictB[15];//8
            listaNumPos2.Add(listaNumPos[15]);
            pic4.Image = listaPictB[20];//10
            listaNumPos2.Add(listaNumPos[20]);
            pic5.Image = listaPictB[5];//4
            listaNumPos2.Add(listaNumPos[5]);
            //segunda fila
            pic6.Image = listaPictB[10];//6
            listaNumPos2.Add(listaNumPos[10]);
            pic7.Image = listaPictB[6];//4
            listaNumPos2.Add(listaNumPos[6]);
            pic8.Image = listaPictB[26];//12
            listaNumPos2.Add(listaNumPos[26]);
            pic9.Image = listaPictB[11];//6
            listaNumPos2.Add(listaNumPos[11]);
            pic10.Image = listaPictB[1];//2
            listaNumPos2.Add(listaNumPos[1]);
            //tercera fila
            pic11.Image = listaPictB[7];//4
            listaNumPos2.Add(listaNumPos[7]);
            pic12.Image = listaPictB[16];//8
            listaNumPos2.Add(listaNumPos[16]);
            pic13.Image = listaPictB[12];//6
            listaNumPos2.Add(listaNumPos[12]);
            pic14.Image = listaPictB[27];//12
            listaNumPos2.Add(listaNumPos[27]);
            pic15.Image = listaPictB[21];//10
            listaNumPos2.Add(listaNumPos[21]);
            //cuarta fila
            pic16.Image = listaPictB[17];//8
            listaNumPos2.Add(listaNumPos[17]);
            pic17.Image = listaPictB[22];//10
            listaNumPos2.Add(listaNumPos[22]);
            pic18.Image = listaPictB[2];//2
            listaNumPos2.Add(listaNumPos[2]);
            pic19.Image = listaPictB[18];//8
            listaNumPos2.Add(listaNumPos[18]);
            pic20.Image = listaPictB[8];//4
            listaNumPos2.Add(listaNumPos[8]);
            //quinta fila
            pic21.Image = listaPictB[23];//10
            listaNumPos2.Add(listaNumPos[23]);
            pic22.Image = listaPictB[13];//6
            listaNumPos2.Add(listaNumPos[13]);
            pic23.Image = listaPictB[28];//12
            listaNumPos2.Add(listaNumPos[28]);
            pic24.Image = listaPictB[9];//4
            listaNumPos2.Add(listaNumPos[9]);
            pic25.Image = listaPictB[24];//10
            listaNumPos2.Add(listaNumPos[24]);
            //sexta fila
            pic26.Image = listaPictB[29];//12
            listaNumPos2.Add(listaNumPos[29]);
            pic27.Image = listaPictB[3];//2
            listaNumPos2.Add(listaNumPos[3]);
            pic28.Image = listaPictB[14];//6
            listaNumPos2.Add(listaNumPos[14]);
            pic29.Image = listaPictB[4];// 2
            listaNumPos2.Add(listaNumPos[4]);
            pic30.Image = listaPictB[19];//
            listaNumPos2.Add(listaNumPos[19]); 
        }
        /// <summary>
        /// Metodo encargado de inicializar las propiedades de DragEnter
        /// DragDrop en cada uno de los PictureBox
        /// </summary>
        private void Inicializar_DragDrop()
        {
            pictTurno.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pictTurno.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            pictTurno.MouseDown += new MouseEventHandler(pictTurno_MouseDown);
            //**********************************************************
            //**********************************************************
            pic1.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic1.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic2.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic2.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic3.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic3.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic4.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic4.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic5.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic5.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic6.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic6.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic7.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic7.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic8.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic8.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic9.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic9.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic10.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic10.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic11.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic11.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic12.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic12.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic13.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic13.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic14.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic14.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic15.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic15.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic16.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic16.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic17.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic17.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic18.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic18.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic19.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic19.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic20.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic20.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic21.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic21.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic22.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic22.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic23.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic23.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic24.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic24.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic25.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic25.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic26.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic26.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic27.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic27.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic28.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic28.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic29.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic29.DragDrop += new DragEventHandler(pictTurno_DragDrop);
            //**********************************************************
            pic30.DragEnter += new DragEventHandler(pictTurno_DragEnter);
            pic30.DragDrop += new DragEventHandler(pictTurno_DragDrop);
        }


        private void btn2_Click(object sender, EventArgs e)
        {

        }

        private void pic30_DragDrop(object sender, DragEventArgs e)
        {

            if (numero == listaNumPos2[29] && listMarcados[29] == true)
            {
                pic30.AllowDrop = true;
                listMarcados[29] = false;
                validarerror = true;
                pictTurno.Enabled = false;

            }
            else if (validarerror==false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);           
            }
           
        }

        private void pic1_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 0;
            if (numero == listaNumPos2[0] && listMarcados[0] == true)
            {
                pic1.AllowDrop = true;
                listMarcados[0] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
               cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void pic2_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 1;
            if (numero == listaNumPos2[1] && listMarcados[1] == true)
            {
                pic2.AllowDrop = true;
                listMarcados[1] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
               cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void pic3_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 2;
            if (numero == listaNumPos2[2] && listMarcados[2] == true)
            {
                pic3.AllowDrop = true;
                listMarcados[2] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);

            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      
        }

        private void pic4_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 3;
            if (numero == listaNumPos2[3] && listMarcados[3] == true)
            {
                pic4.AllowDrop = true;
                listMarcados[3] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pic5_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 4;
            if (numero == listaNumPos2[4] && listMarcados[4] == true)
            {
                pic5.AllowDrop = true;
                listMarcados[4] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pic6_DragDrop(object sender, DragEventArgs e)
        {
            fila = 1;
            colum = 0;
            if (numero == listaNumPos2[5] && listMarcados[5] == true)
            {
                pic6.AllowDrop = true;
                listMarcados[5] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic7_DragDrop(object sender, DragEventArgs e)
        {
            fila = 1;
            colum = 1;
            if (numero == listaNumPos2[6] && listMarcados[6] == true)
            {
                pic7.AllowDrop = true;
                listMarcados[6] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic8_DragDrop(object sender, DragEventArgs e)
        {
            fila = 1;
            colum = 2;
            if (numero == listaNumPos2[7] && listMarcados[7] == true)
            {
                pic8.AllowDrop = true;
                listMarcados[7] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic9_DragDrop(object sender, DragEventArgs e)
        {
            fila = 1;
            colum = 3;
            if (numero == listaNumPos2[8] && listMarcados[8] == true)
            {
                pic9.AllowDrop = true;
                listMarcados[8] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic10_DragDrop(object sender, DragEventArgs e)
        {
            fila = 1;
            colum=4;
            if (numero == listaNumPos2[9] && listMarcados[9] == true)
            {
                pic9.AllowDrop = true;
                listMarcados[9] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic11_DragDrop(object sender, DragEventArgs e)
        {
            fila = 2;
            colum = 0;
            if (numero == listaNumPos2[10] && listMarcados[10] == true)
            {
                pic11.AllowDrop = true;
                listMarcados[10] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic12_DragDrop(object sender, DragEventArgs e)
        {
            fila = 2;
            colum = 1;
            if (numero == listaNumPos2[11] && listMarcados[11] == true)
            {
                pic12.AllowDrop = true;
                listMarcados[11] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic13_DragDrop(object sender, DragEventArgs e)
        {
            fila = 2;
            colum = 2;
            if (numero == listaNumPos2[12] && listMarcados[12] == true)
            {
                pic13.AllowDrop = true;
                listMarcados[12] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic14_DragDrop(object sender, DragEventArgs e)
        {
            fila = 2;
            colum=3;
            if (numero == listaNumPos2[13] && listMarcados[13] == true)
            {
                pic14.AllowDrop = true;
                listMarcados[13] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic15_DragDrop(object sender, DragEventArgs e)
        {
            fila = 2;
            colum = 4;
            if (numero == listaNumPos2[14] && listMarcados[14] == true)
            {
                pic15.AllowDrop = true;
                listMarcados[14] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic16_DragDrop(object sender, DragEventArgs e)
        {
            fila = 3;
            colum = 0;
            if (numero == listaNumPos2[15] && listMarcados[15] == true)
            {
                pic16.AllowDrop = true;
                listMarcados[15] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic17_DragDrop(object sender, DragEventArgs e)
        {
            fila = 3;
            colum = 1;
            if (numero == listaNumPos2[16] && listMarcados[16] == true)
            {
                pic17.AllowDrop = true;
                listMarcados[16] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic18_DragDrop(object sender, DragEventArgs e)
        {
            fila = 3;
            colum = 2;
            if (numero == listaNumPos2[17] && listMarcados[17] == true)
            {
                pic18.AllowDrop = true;
                listMarcados[17] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic19_DragDrop(object sender, DragEventArgs e)
        {
            fila = 3;
            colum = 3;
            if (numero == listaNumPos2[18] && listMarcados[18] == true)
            {
                pic19.AllowDrop = true;
                listMarcados[18] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic20_DragDrop(object sender, DragEventArgs e)
        {
            fila = 3;
            colum = 4;
            if (numero == listaNumPos2[19] && listMarcados[19] == true)
            {
                pic20.AllowDrop = true;
                listMarcados[19] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic21_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[20] && listMarcados[20] == true)
            {
                pic21.AllowDrop = true;
                listMarcados[20] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic22_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[21] && listMarcados[21] == true)
            {
                pic22.AllowDrop = true;
                listMarcados[21] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic23_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[22] && listMarcados[22] == true)
            {
                pic23.AllowDrop = true;
                listMarcados[22] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic24_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[23] && listMarcados[23] == true)
            {
                pic24.AllowDrop = true;
                listMarcados[23] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic25_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[24] && listMarcados[24] == true)
            {
                pic25.AllowDrop = true;
                listMarcados[24] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic26_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[25] && listMarcados[25] == true)
            {
                pic26.AllowDrop = true;
                listMarcados[25] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic27_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[26] && listMarcados[26] == true)
            {
                pic27.AllowDrop = true;
                listMarcados[26] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic28_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[27] && listMarcados[27] == true)
            {
                pic28.AllowDrop = true;
                listMarcados[27] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic29_DragDrop(object sender, DragEventArgs e)
        {
            if (numero == listaNumPos2[28] && listMarcados[28] == true)
            {
                pic29.AllowDrop = true;
                listMarcados[28] = false;
                validarerror = true;
                pictTurno.Enabled = false;
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictTurno_DragDrop(object sender, DragEventArgs e)
        {

            if (pictTurno.Image == pictJug1.Image)
            {
               timJug1.Stop();
            }
            else
            {
                timJug2.Stop();
            }


            if (cambiojug == true)
            {
                PictureBox pb = (PictureBox)sender;
                pb.Image = (Image)e.Data.GetData(DataFormats.Bitmap);
            }
        }

        private void pictTurno_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictTurno_MouseDown(object sender, MouseEventArgs e)
        {
            if (cambiojug == true)
            {
                PictureBox pb = (PictureBox)sender;
                pb.Select();
                pb.DoDragDrop(pb.Image, DragDropEffects.Copy);
            }
            cambiojug = true;
          
        }

        private void timJug2_Tick(object sender, EventArgs e)
        {
            s1 += 1;
            if (s1 == 59)
            {
                m1 += 1;
                s1 = 0;
            }
            if (m1 == 59)
            {
                h1 += 1;
                m1 = 0;
            }
            string hh = Convert.ToString(h1);
            string mm = Convert.ToString(m1);
            string ss = Convert.ToString(s1);
            lblTiemJug2.Text = hh+": "+mm +": "+ss;
            tie = DateTime.Now;
            // string tiempo = hh+"/" + "/" + mm +"/"+ ss;
            // System.TimeSpan duration = new System.TimeSpan(0, h1,m1,s1);

            Resul = tie.AddHours(h1);
            Resul = tie.AddMinutes(m1);
            Resul = tie.AddSeconds(s1);
        }

        private void timJug1_Tick_1(object sender, EventArgs e)
        {
            s += 1;
            if (s == 59)
            {
                m += 1;
                s = 0;
            }
            if (m == 59)
            {
                h += 1;
                m = 0;
            }
            string hh = Convert.ToString(h);
            string mm = Convert.ToString(m);
            string ss = Convert.ToString(s);
            lblTiemJug1.Text = hh+ ": " +mm +": "+ss;

            tie = DateTime.Now;
            // string tiempo = hh+"/" + "/" + mm +"/"+ ss;
            //   System.TimeSpan duration = new System.TimeSpan(0, h, m, s);

            Resul1 = tie.AddHours(h);
            Resul1 = tie.AddMinutes(m);
            Resul1 = tie.AddSeconds(s);
        }

        private void timHora_Tick(object sender, EventArgs e)
        {
            DateTime tie = DateTime.Now;
            lblHora.Text = tie.ToString();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            VictoriaJugadores v = new VictoriaJugadores();
            v.Show();
        }
        private void frmJuego_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((PictureBox)sender).Tag.ToString());
        }
    }
}
