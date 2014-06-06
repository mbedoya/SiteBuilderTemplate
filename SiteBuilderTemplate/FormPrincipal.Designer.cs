namespace SiteBuilderTemplate
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRutaExportar = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNombreBD = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNombreDatos = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPrefijoDatos = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPrefijoNegocio = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNombreNegocio = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNombreModelo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLayout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrefijoModelo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSchemas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.cboTablas = new System.Windows.Forms.ComboBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCarTextArea = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaximoCaracteresTabla = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPropiedad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCssCuerpo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCssEncabezado = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvColumnas = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tabla:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvColumnas);
            this.groupBox1.Controls.Add(this.txtRutaExportar);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtNombreBD);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtNombreDatos);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtPrefijoDatos);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtPrefijoNegocio);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtNombreNegocio);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtNombreModelo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtLayout);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPrefijoModelo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNamespace);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboSchemas);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGenerar);
            this.groupBox1.Controls.Add(this.cboTablas);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 608);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // txtRutaExportar
            // 
            this.txtRutaExportar.Location = new System.Drawing.Point(89, 282);
            this.txtRutaExportar.Name = "txtRutaExportar";
            this.txtRutaExportar.Size = new System.Drawing.Size(182, 20);
            this.txtRutaExportar.TabIndex = 26;
            this.txtRutaExportar.Text = "C:\\Plantillas";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 285);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "Exportar a:";
            // 
            // txtNombreBD
            // 
            this.txtNombreBD.Location = new System.Drawing.Point(361, 238);
            this.txtNombreBD.Name = "txtNombreBD";
            this.txtNombreBD.Size = new System.Drawing.Size(182, 20);
            this.txtNombreBD.TabIndex = 24;
            this.txtNombreBD.Text = "felicis";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(288, 241);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "Nombre BD:";
            // 
            // txtNombreDatos
            // 
            this.txtNombreDatos.Location = new System.Drawing.Point(89, 238);
            this.txtNombreDatos.Name = "txtNombreDatos";
            this.txtNombreDatos.Size = new System.Drawing.Size(182, 20);
            this.txtNombreDatos.TabIndex = 22;
            this.txtNombreDatos.Text = "Data";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 241);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Datos:";
            // 
            // txtPrefijoDatos
            // 
            this.txtPrefijoDatos.Location = new System.Drawing.Point(361, 197);
            this.txtPrefijoDatos.Name = "txtPrefijoDatos";
            this.txtPrefijoDatos.Size = new System.Drawing.Size(182, 20);
            this.txtPrefijoDatos.TabIndex = 20;
            this.txtPrefijoDatos.Text = "DAL";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(288, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Pref Datos:";
            // 
            // txtPrefijoNegocio
            // 
            this.txtPrefijoNegocio.Location = new System.Drawing.Point(89, 197);
            this.txtPrefijoNegocio.Name = "txtPrefijoNegocio";
            this.txtPrefijoNegocio.Size = new System.Drawing.Size(182, 20);
            this.txtPrefijoNegocio.TabIndex = 18;
            this.txtPrefijoNegocio.Text = "BO";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Pref Negocio:";
            // 
            // txtNombreNegocio
            // 
            this.txtNombreNegocio.Location = new System.Drawing.Point(89, 152);
            this.txtNombreNegocio.Name = "txtNombreNegocio";
            this.txtNombreNegocio.Size = new System.Drawing.Size(182, 20);
            this.txtNombreNegocio.TabIndex = 16;
            this.txtNombreNegocio.Text = "Business";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 155);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Negocio:";
            // 
            // txtNombreModelo
            // 
            this.txtNombreModelo.Location = new System.Drawing.Point(361, 115);
            this.txtNombreModelo.Name = "txtNombreModelo";
            this.txtNombreModelo.Size = new System.Drawing.Size(182, 20);
            this.txtNombreModelo.TabIndex = 14;
            this.txtNombreModelo.Text = "Models";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(288, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Modelo:";
            // 
            // txtLayout
            // 
            this.txtLayout.Location = new System.Drawing.Point(361, 155);
            this.txtLayout.Name = "txtLayout";
            this.txtLayout.Size = new System.Drawing.Size(182, 20);
            this.txtLayout.TabIndex = 12;
            this.txtLayout.Text = "AdminLayout";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Layout:";
            // 
            // txtPrefijoModelo
            // 
            this.txtPrefijoModelo.Location = new System.Drawing.Point(89, 112);
            this.txtPrefijoModelo.Name = "txtPrefijoModelo";
            this.txtPrefijoModelo.Size = new System.Drawing.Size(182, 20);
            this.txtPrefijoModelo.TabIndex = 10;
            this.txtPrefijoModelo.Text = "DataModel";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pref Modelo:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(361, 70);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(182, 20);
            this.txtNamespace.TabIndex = 8;
            this.txtNamespace.Text = "BusinessManager";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "NS Negocio:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(89, 70);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(182, 20);
            this.txtNombre.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre:";
            // 
            // cboSchemas
            // 
            this.cboSchemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSchemas.FormattingEnabled = true;
            this.cboSchemas.Location = new System.Drawing.Point(89, 25);
            this.cboSchemas.Name = "cboSchemas";
            this.cboSchemas.Size = new System.Drawing.Size(182, 21);
            this.cboSchemas.TabIndex = 4;
            this.cboSchemas.SelectedIndexChanged += new System.EventHandler(this.cboSchemas_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Schema:";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(468, 579);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 2;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // cboTablas
            // 
            this.cboTablas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTablas.FormattingEnabled = true;
            this.cboTablas.Location = new System.Drawing.Point(361, 25);
            this.cboTablas.Name = "cboTablas";
            this.cboTablas.Size = new System.Drawing.Size(182, 21);
            this.cboTablas.TabIndex = 1;
            this.cboTablas.SelectedIndexChanged += new System.EventHandler(this.cboTablas_SelectedIndexChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(16, 689);
            this.txtCodigo.Multiline = true;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCodigo.Size = new System.Drawing.Size(599, 75);
            this.txtCodigo.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 671);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(595, 645);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Básicas";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(595, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Varios";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCarTextArea);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtMaximoCaracteresTabla);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtPropiedad);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCssCuerpo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCssEncabezado);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(15, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(564, 175);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // txtCarTextArea
            // 
            this.txtCarTextArea.Location = new System.Drawing.Point(385, 66);
            this.txtCarTextArea.Name = "txtCarTextArea";
            this.txtCarTextArea.Size = new System.Drawing.Size(161, 20);
            this.txtCarTextArea.TabIndex = 14;
            this.txtCarTextArea.Text = "150";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(300, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Car Text Area:";
            // 
            // txtMaximoCaracteresTabla
            // 
            this.txtMaximoCaracteresTabla.Location = new System.Drawing.Point(385, 26);
            this.txtMaximoCaracteresTabla.Name = "txtMaximoCaracteresTabla";
            this.txtMaximoCaracteresTabla.Size = new System.Drawing.Size(161, 20);
            this.txtMaximoCaracteresTabla.TabIndex = 12;
            this.txtMaximoCaracteresTabla.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(300, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Max Car Tabla:";
            // 
            // txtPropiedad
            // 
            this.txtPropiedad.Location = new System.Drawing.Point(106, 108);
            this.txtPropiedad.Name = "txtPropiedad";
            this.txtPropiedad.Size = new System.Drawing.Size(182, 20);
            this.txtPropiedad.TabIndex = 10;
            this.txtPropiedad.Text = "public @@  { get; set; }";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Propiedad:";
            // 
            // txtCssCuerpo
            // 
            this.txtCssCuerpo.Location = new System.Drawing.Point(106, 66);
            this.txtCssCuerpo.Name = "txtCssCuerpo";
            this.txtCssCuerpo.Size = new System.Drawing.Size(182, 20);
            this.txtCssCuerpo.TabIndex = 8;
            this.txtCssCuerpo.Text = "<td scope=\"row\">@@</td>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "CSS Cuerpo:";
            // 
            // txtCssEncabezado
            // 
            this.txtCssEncabezado.Location = new System.Drawing.Point(106, 26);
            this.txtCssEncabezado.Name = "txtCssEncabezado";
            this.txtCssEncabezado.Size = new System.Drawing.Size(182, 20);
            this.txtCssEncabezado.TabIndex = 6;
            this.txtCssEncabezado.Text = "<th class=\"top\" scope=\"col\">@@</th>";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "CSS Encabezado:";
            // 
            // dgvColumnas
            // 
            this.dgvColumnas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumnas.Location = new System.Drawing.Point(19, 320);
            this.dgvColumnas.Name = "dgvColumnas";
            this.dgvColumnas.Size = new System.Drawing.Size(524, 253);
            this.dgvColumnas.TabIndex = 27;
            this.dgvColumnas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColumnas_CellContentClick);
            this.dgvColumnas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColumnas_CellValueChanged);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 776);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtCodigo);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Builder ";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.ComboBox cboTablas;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSchemas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrefijoModelo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLayout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCssEncabezado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCssCuerpo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPropiedad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaximoCaracteresTabla;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCarTextArea;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNombreModelo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNombreNegocio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrefijoNegocio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPrefijoDatos;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNombreDatos;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNombreBD;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtRutaExportar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dgvColumnas;
    }
}

