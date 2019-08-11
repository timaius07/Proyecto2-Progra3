using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BO;
using GUI.Properties;
using System.Threading;
using Entidades;
using System.IO;
using System.Drawing.Imaging;

namespace GUI
{
    public partial class frmJuego : Form

    {
        
        int h; // variable para el control del jug 1
        int m;// variable para el control del tiempo jug 1
        int s;// variable para el control del tiempo jug 1
        int segunTieTotalJug1;// variable para el control del tiempo jug 1
        int segunTieTotalJug2;// variable para el control del tiempo jug 2
        int h1; // variable para el control del tiempo jug 2
        int m1;// variable para el control del tiempo jug 2
        int s1;// variable para el control del tiempo jug 2k
        int fila = 0; //variable que contiene la fila en el tablero
        int colum = 0; // variable que contiene la columna del tablero
        int contturnos = 0; //cuenta los errorres del jugador
        int numdado = 0; // variable que contiene el numero de resultado del dado
        bool comodin = false;//variable que indica si es esta usando el comodin
        bool ganar = false;//variable que indica si el jugador gana
        Image comodinVic = null; // contiene la imagen del ganador
        PictureBox pic = new PictureBox();
        //variables usadas para combinar las imagenes
        Stream imagen1, imagen2;
        MemoryStream mm = new MemoryStream();
        //**********
        DateTime tie = DateTime.Now;

        DadoBO dbo;
        CuadroBO cbo;  
        /// <summary>
        /// variable que recibe un jugador el cual es el que se logea
        /// </summary>
        public Jugador jugador { get; set; }
        /// <summary>
        ///variable que recibe un jugador el cual es el que se logea
        /// </summary>
        public Jugador jugador1 { get; set; }
        List<Image> listaPictB = new List<Image>();//imagenes con todos los numeros
        List<int> listaNumPos = new List<int>();//lista con los 30 numeros a usar
        List<int> listaNumPos2 = new List<int>();//almacena los numeros en el orden de
        //acomodo de la listaNumPos
        List<bool> listMarcados = new List<bool>();//con la que se verifica que 
        //posision del tablero no esta en uso
        List<int> listaRepetidos = new List<int>();//verifica la cantidad de veces
        //que aparece el numero en el dado
        int numerodado = 0; //almacena el doble del numero del dado
        Random rnd = new Random();//instancia de numeros aleatorios
        bool iniciarjuego = false;//verifica si se inicia el juego para
        //realizar el random de jugadores
        //impide que se pueda cambiar la ficha en el dragdrop
        bool cambiojug = true;    
        bool validarerror = false;//valida error al colocar la ficha correcta
        //vector con los numeros del juego
        int[] numeros = new int[] { 2, 4, 6, 8, 10, 12 };
        public frmJuego()
        {
            InitializeComponent();
            dbo = new DadoBO();
            cbo = new CuadroBO();
            pic.Size = new Size (51,50);
        }
        /// <summary>
        /// Carga el formulario y habilta las opciones de drag and drop
        /// para los picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmJuego_Load(object sender, EventArgs e)
        {
            lblJug1.Text = jugador.Nombre;
            lblJug2.Text = jugador1.Nombre;
            pictJug1.Image = jugador.Imagen.Foto;
            pictJug2.Image = jugador1.Imagen.Foto;

            timHora.Start();
            this.Inicializar_DragDrop();
            listaPictB = new List<Image>();
            pictJug1.AllowDrop = true;
            pictJug2.AllowDrop = true;
            pictTurno.AllowDrop = true;
            cbo.Llenar_Cuadro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Lanzar_Dado();
            IniciarTiempos();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            iniciarjuego = true;
            this.Insertar_ImageList();
            this.Lista_Random();
            btn1.Enabled = false;
            btnDado.Enabled = true;
        }


        /// <summary>
        /// metodo que realiza el lanzamiento del dado
        /// y cambia los turnos de los jugadores
        /// </summary>
        public void Lanzar_Dado()
        {
            validarerror = false;
            Activar_Allow_Drop();

            this.Random_Jug();
            for (int i = 0; i < 20; i++)
            {
                numdado = dbo.LanzarDado();
                pictBoxDado.Image = (Image)Resources.ResourceManager.GetObject("dado_" + numdado);
                Refresh();
                Thread.Sleep(i * 15);
            }
            numerodado = numdado * 2;
            pictTurno.Enabled = true;
            iniciarjuego = false;
            int cont = 0;
            for (int i = 0; i < listaRepetidos.Count; i++)
            {
                if (listaRepetidos[i] == numdado)
                {
                    cont++;
                }
            }
            if (cont >= 5)
            {
                MessageBox.Show("Lo sentimos el doble del número no esta Disponible. ", "Error al colocar",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (comodin == true)
                {
                    panelGanador.Visible = true;
                }
            }
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
                
                }
                else
                {
                    pictTurno.Image = pictJug2.Image;
                   
                }
            }
            else
            {
                if (pictTurno.Image == pictJug1.Image)
                {                
                    pictTurno.Image = pictJug2.Image;
                    this.IniciarTiempos();
                }
                else
                {               
                    pictTurno.Image = pictJug1.Image;
                    this.IniciarTiempos();
                }
            }

        }
        /// <summary>
        /// Metodo encargado de realizar la sumatoria del tiempo de las jugadas
        /// </summary>
       public void IniciarTiempos()
        {
            if (pictTurno.Image == pictJug1.Image)
            {
                timJug1.Start();
                timJug2.Stop();                
            }
            else
            {
                timJug2.Start();
                timJug1.Stop();
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
                numeros = new int[] { 2, 4, 6, 8, 10, 12 };
            }
            else if (aleatorio == 1)
            {
                numeros = new int[] { 12, 10, 8, 6, 4, 2 };
            }
            else if (aleatorio == 2)
            {
                numeros = new int[] { 6, 4, 8, 12, 2, 10 };
            }
            else if (aleatorio == 3)
            {
                numeros = new int[] { 10, 8, 12, 2, 4, 6 };
            }
            else if (aleatorio == 4)
            {
                numeros = new int[] { 4, 12, 8, 10, 6, 2 };
            }
            else if (aleatorio == 5)
            {
                numeros = new int[] { 8, 2, 6, 12, 10, 4 };
            }
            else if (aleatorio == 6)
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
        
     

        private void pic1_DragDrop(object sender, DragEventArgs e)
        {
            fila = 0;
            colum = 0;
            if (numerodado == listaNumPos2[0] && listMarcados[0] == true)
            {               
                listMarcados[0] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                //fusionar imagenes            
                listaPictB[0] = this.Fusionar_Imagenes(pictTurno.Image, pic1.Image);             
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[1] && listMarcados[1] == true)
            {              
                listMarcados[1] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[25] = this.Fusionar_Imagenes(pictTurno.Image, pic2.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[2] && listMarcados[2] == true)
            {
                listMarcados[2] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[15] = this.Fusionar_Imagenes(pictTurno.Image, pic3.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[3] && listMarcados[3] == true)
            {     
                listMarcados[3] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[20] = this.Fusionar_Imagenes(pictTurno.Image, pic4.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[4] && listMarcados[4] == true)
            {             
                listMarcados[4] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[5] = this.Fusionar_Imagenes(pictTurno.Image, pic5.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[5] && listMarcados[5] == true)
            {              
                listMarcados[5] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[10] = this.Fusionar_Imagenes(pictTurno.Image, pic6.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[6] && listMarcados[6] == true)
            {             
                listMarcados[6] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[6] = this.Fusionar_Imagenes(pictTurno.Image, pic7.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[7] && listMarcados[7] == true)
            {               
                listMarcados[7] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[26] = this.Fusionar_Imagenes(pictTurno.Image, pic8.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[8] && listMarcados[8] == true)
            {                
                listMarcados[8] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[11] = this.Fusionar_Imagenes(pictTurno.Image, pic9.Image);
                this.Jugada_Ganadora();
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
            colum = 4;
            if (numerodado == listaNumPos2[9] && listMarcados[9] == true)
            {               
                listMarcados[9] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[1] = this.Fusionar_Imagenes(pictTurno.Image, pic10.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[10] && listMarcados[10] == true)
            {               
                listMarcados[10] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[7] = this.Fusionar_Imagenes(pictTurno.Image, pic11.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[11] && listMarcados[11] == true)
            {                
                listMarcados[11] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[16] = this.Fusionar_Imagenes(pictTurno.Image, pic12.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[12] && listMarcados[12] == true)
            {               
                listMarcados[12] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[12] = this.Fusionar_Imagenes(pictTurno.Image, pic13.Image);
                this.Jugada_Ganadora();
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
            colum = 3;
            if (numerodado == listaNumPos2[13] && listMarcados[13] == true)
            {                
                listMarcados[13] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[27] = this.Fusionar_Imagenes(pictTurno.Image, pic14.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[14] && listMarcados[14] == true)
            {               
                listMarcados[14] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[21] = this.Fusionar_Imagenes(pictTurno.Image, pic15.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[15] && listMarcados[15] == true)
            {                
                listMarcados[15] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[17] = this.Fusionar_Imagenes(pictTurno.Image, pic16.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[16] && listMarcados[16] == true)
            {              
                listMarcados[16] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[22] = this.Fusionar_Imagenes(pictTurno.Image, pic17.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[17] && listMarcados[17] == true)
            {               
                listMarcados[17] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[2] = this.Fusionar_Imagenes(pictTurno.Image, pic18.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[18] && listMarcados[18] == true)
            {            
                listMarcados[18] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[18] = this.Fusionar_Imagenes(pictTurno.Image, pic19.Image);
                this.Jugada_Ganadora();
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
            if (numerodado == listaNumPos2[19] && listMarcados[19] == true)
            {              
                listMarcados[19] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[8] = this.Fusionar_Imagenes(pictTurno.Image, pic20.Image);
                this.Jugada_Ganadora();
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
            fila = 4;
            colum = 0;
            if (numerodado == listaNumPos2[20] && listMarcados[20] == true)
            {              
                listMarcados[20] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[23] = this.Fusionar_Imagenes(pictTurno.Image, pic21.Image);
                this.Jugada_Ganadora();
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
            fila = 4;
            colum = 1;
            if (numerodado == listaNumPos2[21] && listMarcados[21] == true)
            {              
                listMarcados[21] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[13] = this.Fusionar_Imagenes(pictTurno.Image, pic22.Image);
                this.Jugada_Ganadora();
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
            fila = 4;
            colum = 2;
            if (numerodado == listaNumPos2[22] && listMarcados[22] == true)
            {              
                listMarcados[22] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[28] = this.Fusionar_Imagenes(pictTurno.Image, pic23.Image);
                this.Jugada_Ganadora();
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
            fila = 4;
            colum = 3;
            if (numerodado == listaNumPos2[23] && listMarcados[23] == true)
            {             
                listMarcados[23] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[9] = this.Fusionar_Imagenes(pictTurno.Image, pic24.Image);
                this.Jugada_Ganadora();
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
            fila = 4;
            colum = 4;

            if (numerodado == listaNumPos2[24] && listMarcados[24] == true)
            {              
                listMarcados[24] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[24] = this.Fusionar_Imagenes(pictTurno.Image, pic25.Image);
                this.Jugada_Ganadora();
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
            fila = 5;
            colum = 0;
            if (numerodado == listaNumPos2[25] && listMarcados[25] == true)
            {         
                listMarcados[25] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[29] = this.Fusionar_Imagenes(pictTurno.Image, pic26.Image);
                this.Jugada_Ganadora();
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
            fila = 5;
            colum = 1;
            if (numerodado == listaNumPos2[26] && listMarcados[26] == true)
            {
                listMarcados[26] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[3] = this.Fusionar_Imagenes(pictTurno.Image, pic27.Image);
                this.Jugada_Ganadora();
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
            fila = 5;
            colum = 2;
            if (numerodado == listaNumPos2[27] && listMarcados[27] == true)
            {
                listMarcados[27] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[14] = this.Fusionar_Imagenes(pictTurno.Image, pic28.Image);
                this.Jugada_Ganadora();
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
            fila = 5;
            colum = 3;
            if (numerodado == listaNumPos2[28] && listMarcados[28] == true)
            {
                
                listMarcados[28] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[4] = this.Fusionar_Imagenes(pictTurno.Image, pic29.Image);
                this.Jugada_Ganadora();
            }
            else if (validarerror == false)
            {
                cambiojug = false;
                MessageBox.Show("No se puede colocar la ficha. Posicion Invalida", "Error al colocar",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic30_DragDrop(object sender, DragEventArgs e)
        {
            fila = 5;
            colum = 4;
            if (numerodado == listaNumPos2[29] && listMarcados[29] == true)
            {
              
                listMarcados[29] = false;
                validarerror = true;
                pictTurno.Enabled = false;
                cbo.Validar_PosicionCuad(fila, colum);
                listaPictB[19] = this.Fusionar_Imagenes(pictTurno.Image, pic30.Image);
                this.Jugada_Ganadora();

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
            if (cambiojug == true)
            {
                PictureBox pb = (PictureBox)sender;
                pb.Image = (Image)e.Data.GetData(DataFormats.Bitmap);
                contturnos = 0;                          
            }
            else
            {
                contturnos += 1;
            }

            if (contturnos == 2)
            {
                MessageBox.Show("Has fallado 2 veces. Perdiste el turno", "Cambio Turno",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                contturnos = 0;
                this.Lanzar_Dado();
            }
            if (comodin == true)
            {
                timJug1.Stop();
                timJug2.Stop();
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
                this.Lista_Random();  
            }

            cambiojug = true;
        }

        private void timJug2_Tick(object sender, EventArgs e)
        {
            segunTieTotalJug2++;
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
            lblTiemJug2.Text = hh + ": " + mm + ": " + ss;           
        }

        private void timJug1_Tick_1(object sender, EventArgs e)
        {
            segunTieTotalJug1++;
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
            lblTiemJug1.Text = hh + ": " + mm + ": " + ss;    
        }

        private void timHora_Tick(object sender, EventArgs e)
        {
            DateTime tie = DateTime.Now;
            lblHora.Text = tie.ToString();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            VictoriaJugadores v = new VictoriaJugadores();
            v.Show(this);
        }


        /// <summary>
        /// metodo que verifica a cada movimiento, si se forma 
        /// la linea de 5 y si se utiliza el comodin
        /// </summary>
        public void Jugada_Ganadora()
        {
            listaRepetidos.Add(numdado);
            //devuelve un verdadero si la jugada realizada formo
            //una linea o una columna de 5
            ganar = cbo.Ganar(comodin);
            if (comodin == true & ganar == true)
            {
                comodinVic = JugadaComodin(ganar);
                this.Victoria(ganar, comodinVic);
            }
            else if (ganar == false & comodin == true)
            {
                panelGanador.Visible = true;
            }else if (ganar == true)
            {
                timJug1.Stop();
                timJug2.Stop();
                comodinVic = pictTurno.Image;
                this.Victoria(ganar, comodinVic);
                panelGanador.Visible = true;
                btnDado.Enabled = false;
            }

        }
        /// <summary>
        /// metodo que muestra un panel con el ganador y el perdedor de la 
        /// partida con sus respectivos tiempos
        /// </summary>
        /// <param name="ganar"> boolean verifica si la jugada es correcta</param>
        /// <param name="ganador"> image contiene la imagen del ganador</param>
        public void Victoria(bool ganar, Image ganador)
        {
          
            if (pictTurno.Image == pictJug1.Image & ganar == true)
            {
                panelGanador.Visible = true;
                pictGanador.Image = ganador;
                lblTiempoEfec.Text = lblTiemJug1.Text;
                if (comodin == false)
                {
                    pictPerdedor.Image = pictJug2.Image;
                }                         
            }
            else if (pictTurno.Image == pictJug2.Image & ganar == true)
            {
                panelGanador.Visible = true;
                pictGanador.Image = ganador;
                lblTiempoEfec.Text = lblTiemJug2.Text;
                if (comodin == false)
                {
                    pictPerdedor.Image = pictJug1.Image;
                }                         
            }
        }
        /// <summary>
        /// verifica cual de los jugadores hizo menos tiempo 
        /// para declararlo ganador luego de usar el codin
        /// </summary>
        /// <param name="ganar">variable que verifica si la jugada comodin
        /// le permitio ganar al jugador</param>
        /// <returns></returns>
        public Image JugadaComodin(bool ganar)
        {
            Image  jugvictoria = null  ;  
                   
            if (ganar == true & comodin == true)
            {
               if (segunTieTotalJug1> segunTieTotalJug2)
                {
                    jugvictoria = pictJug2.Image;
                    pictPerdedor.Image = pictJug1.Image;
                }
                else
                {
                    //cambiar por pictGanador
                    jugvictoria = pictJug1.Image;
                    pictPerdedor.Image = pictJug2.Image;
                }              
            }
            //retorna la imagen del jugador que gana con el comodin
            return jugvictoria;
        }
        /// <summary>
        /// guarda las victorias del jugador en la BD
        /// </summary>
        /// <param name="ganar"></param>
        public void Guadar_Victoria(Image jugadorG)
        {
            VictoriaBO vic = new VictoriaBO();
            if (pictJug1.Image == jugadorG )
            {
                vic.GuardarVictoria(segunTieTotalJug1, jugador.Id);
            }
            else if (pictJug2.Image == jugadorG)
            {
                vic.GuardarVictoria(segunTieTotalJug2, jugador1.Id);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelGanador.Visible = false;        
            this.Guadar_Victoria(comodinVic);
        }

        private void btnComodin_Click(object sender, EventArgs e)
        {
            panelGanador.Visible = false;
            this.Lanzar_Dado();
            comodin = true;
            btnComodin.Enabled = false;
            btnDado.Enabled = false;
        }

        private void btnDado_MouseClick(object sender, MouseEventArgs e)
        {
            IniciarTiempos();
        }

        public Image Fusionar_Imagenes(Image turno, Image numero)
        {
            Image mezcla;
            imagen1 = GetStream(numero, (ImageFormat.Jpeg));
            imagen2 = GetStream2(turno, (ImageFormat.Jpeg));
            int x = 75;
            int y = 55;
            int altorel = 44;
            int anchorel = 65;
            bool relativo = false;

            mm = dbo.mezclar(imagen1, imagen2, x, y, altorel, anchorel, relativo);
            mezcla = Image.FromStream(mm, true);
           return mezcla ;
        }

        public Stream GetStream(Image img, ImageFormat format)
        {
            var ms = new MemoryStream();
            img.Save(ms, format);
            return ms;
        }

        private void frmJuego_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Realmente desea Salir?", " Salir a Menu",
            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //llama al formulario padre que fue quien lo invoco para volverlo a mostrar
                this.Owner.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }

      

        public Stream GetStream2(Image img, ImageFormat format)
        {
            var ms = new MemoryStream();
            img.Save(ms, format);
            return ms;
        }
    }
}
