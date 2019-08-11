namespace GUI
{
    partial class VictoriaJugadores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.DataGrJugadores = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DTGDatosJug = new System.Windows.Forms.DataGridView();
            this.Cantidad_de_Victorias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromedioVict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.DTGRTop5 = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCorreo = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrJugadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTGDatosJug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTGRTop5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGrJugadores
            // 
            this.DataGrJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrJugadores.Location = new System.Drawing.Point(21, 83);
            this.DataGrJugadores.Name = "DataGrJugadores";
            this.DataGrJugadores.ReadOnly = true;
            this.DataGrJugadores.Size = new System.Drawing.Size(369, 141);
            this.DataGrJugadores.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lista de los jugadores:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(21, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Consultar ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DTGDatosJug
            // 
            this.DTGDatosJug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGDatosJug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cantidad_de_Victorias,
            this.PromedioVict});
            this.DTGDatosJug.Location = new System.Drawing.Point(8, 314);
            this.DTGDatosJug.Name = "DTGDatosJug";
            this.DTGDatosJug.ReadOnly = true;
            this.DTGDatosJug.Size = new System.Drawing.Size(347, 60);
            this.DTGDatosJug.TabIndex = 3;
            // 
            // Cantidad_de_Victorias
            // 
            this.Cantidad_de_Victorias.HeaderText = "Cantidad de Victorias";
            this.Cantidad_de_Victorias.Name = "Cantidad_de_Victorias";
            this.Cantidad_de_Victorias.ReadOnly = true;
            this.Cantidad_de_Victorias.Width = 150;
            // 
            // PromedioVict
            // 
            this.PromedioVict.HeaderText = "Promedio del tiempo en conseguirlas.";
            this.PromedioVict.Name = "PromedioVict";
            this.PromedioVict.ReadOnly = true;
            this.PromedioVict.Width = 150;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Victorias y Promedios:";
            // 
            // DTGRTop5
            // 
            this.DTGRTop5.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.DTGRTop5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGRTop5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.tiempo});
            this.DTGRTop5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DTGRTop5.Location = new System.Drawing.Point(444, 83);
            this.DTGRTop5.Name = "DTGRTop5";
            this.DTGRTop5.Size = new System.Drawing.Size(343, 141);
            this.DTGRTop5.TabIndex = 5;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre Jugador";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 150;
            // 
            // tiempo
            // 
            this.tiempo.DividerWidth = 1;
            this.tiempo.HeaderText = "Mejores Tiempos";
            this.tiempo.Name = "tiempo";
            this.tiempo.ReadOnly = true;
            this.tiempo.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(440, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Top 5 :  *** Mejores Tiempos***";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(21, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 28);
            this.button2.TabIndex = 8;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCorreo
            // 
            this.btnCorreo.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnCorreo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorreo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorreo.ForeColor = System.Drawing.Color.White;
            this.btnCorreo.Location = new System.Drawing.Point(793, 163);
            this.btnCorreo.Name = "btnCorreo";
            this.btnCorreo.Size = new System.Drawing.Size(101, 30);
            this.btnCorreo.TabIndex = 9;
            this.btnCorreo.Text = "Compartir Top5";
            this.btnCorreo.UseVisualStyleBackColor = false;
            this.btnCorreo.Click += new System.EventHandler(this.btnCorreo_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(487, 269);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(300, 213);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // VictoriaJugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::GUI.Properties.Resources.fondo4;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(921, 516);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnCorreo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DTGRTop5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DTGDatosJug);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGrJugadores);
            this.DoubleBuffered = true;
            this.Name = "VictoriaJugadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VictoriaJugadores";
            this.Load += new System.EventHandler(this.VictoriaJugadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrJugadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTGDatosJug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTGRTop5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrJugadores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView DTGDatosJug;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad_de_Victorias;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromedioVict;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DTGRTop5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCorreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}