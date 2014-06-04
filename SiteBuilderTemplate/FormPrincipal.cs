using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;

namespace SiteBuilderTemplate
{
    public partial class FormPrincipal : Form
    {
        public const string cadenaConexion = "Server=localhost;Database=felicis;Uid=root;Pwd=12345;";
        public const string rutaPlantillas = @"C:\Users\USUARIO\Documents\visual studio 2012\Projects\SiteBuilderTemplate\SiteBuilderTemplate\Plantillas\";
        public const string plantillaUI = "UI.html";
        public const string plantillaClase = "ModelClass.html";

        public FormPrincipal()
        {
            InitializeComponent();
        }        

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            string consultaSchemas = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA ORDER BY SCHEMA_NAME";

            MySqlConnection connection = new MySqlConnection(cadenaConexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSchemas, connection);
            DataTable results = new DataTable();
            adapter.Fill(results);

            cboSchemas.DisplayMember = "SCHEMA_NAME";
            cboSchemas.ValueMember = "SCHEMA_NAME";
            cboSchemas.DataSource = results;            
        }

        private void cboSchemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string consultaSchemas = 
                "SELECT TABLE_NAME " +
                " FROM INFORMATION_SCHEMA.TABLES " +
                " WHERE TABLE_SCHEMA ='" + cboSchemas.Text + "'" +
                " ORDER BY TABLE_NAME ";

            MySqlConnection connection = new MySqlConnection(cadenaConexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSchemas, connection);
            DataTable results = new DataTable();
            adapter.Fill(results);

            cboTablas.DisplayMember = "TABLE_NAME";
            cboTablas.ValueMember = "TABLE_NAME";
            cboTablas.DataSource = results;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string nombreTabla = cboTablas.Text;

            string consultaSchemas =
                "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, COLUMN_KEY " +
                " FROM INFORMATION_SCHEMA.COLUMNS " +
                " WHERE TABLE_SCHEMA ='" + cboSchemas.Text + "' " +
                " AND TABLE_NAME ='" + nombreTabla + "' ";

            MySqlConnection connection = new MySqlConnection(cadenaConexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSchemas, connection);
            DataTable results = new DataTable();
            adapter.Fill(results);

            //Generar textos para plantillas
            string namespaceModelo = txtNamespaceModelo.Text;
            string nombreModeloCompleto = namespaceModelo + "." + Pascal(nombreTabla) + txtPrefijoModelo.Text;
            string nombreNegocio = txtNombre.Text;
            string layout = txtLayout.Text;
            string encabezadoIndice = txtCssEncabezado.Text;
            string cuerpoIndice = txtCssCuerpo.Text;
            string propiedadaesClase = txtPropiedad.Text;
            int maximoCaracteresTabla = Convert.ToInt32(txtMaximoCaracteresTabla.Text);

            //Generación de código de UI
            string textoPlantillaUI = File.ReadAllText(rutaPlantillas + plantillaUI);
            string textoPlantillaClase = File.ReadAllText(rutaPlantillas + plantillaClase);

            string textoEncabezado = "";
            string textoCuerpo = "";
            string textoPropiedades = "";
            string clave = "";

            foreach (DataRow item in results.Rows)
            {
                if (item["COLUMN_KEY"].ToString().ToLower() == "pri")
                {
                    clave = item["COLUMN_NAME"].ToString();
                }

                //Encabezado UI
                textoEncabezado += "\r\n\t" + encabezadoIndice.Replace("@@", item["COLUMN_NAME"].ToString());

                //Cuerpo UI
                if (item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull) &&
                    Convert.ToInt32(item["CHARACTER_MAXIMUM_LENGTH"]) > maximoCaracteresTabla)
                {
                    textoCuerpo += "\r\n\t" + cuerpoIndice.Replace("@@", "@WebSite.WebUtilities.Misc.PreviewText(item." + item["COLUMN_NAME"].ToString() + "," + maximoCaracteresTabla.ToString() + ")");
                }
                else
                {
                    textoCuerpo += "\r\n\t" + cuerpoIndice.Replace("@@", "@item." + item["COLUMN_NAME"].ToString());
                }
                

                //Propiedadaes Clase
                textoPropiedades += "\r\n\t" + propiedadaesClase.Replace("@@",
                    MapearTipoDato(item["DATA_TYPE"].ToString()) + " " + item["COLUMN_NAME"].ToString());
                
            }

            txtCodigo.Text = textoPlantillaUI.Replace("@@Nombre", nombreNegocio)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Encabezado", textoEncabezado)
                .Replace("@@Cuerpo", textoCuerpo)
                .Replace("@@Clave", clave);

            txtCodigo.Text += "\r\n\t" + textoPlantillaClase.Replace("@@Namespace", namespaceModelo)
                .Replace("@@Clase", Pascal(nombreTabla) + txtPrefijoModelo.Text)
                .Replace("@@Campos", textoPropiedades);
        }

        private string Pascal(string texto)
        {
            return texto.Substring(0, 1).ToUpper() + texto.Substring(1, texto.Length - 1);
        }

        private string MapearTipoDato(string tipoBD)
        {
            tipoBD = tipoBD.ToLower();

            if (tipoBD.Contains("int"))
            {
                return "int";
            }
            else
            {
                if (tipoBD.Contains("varchar"))
                {
                    return "string";
                }
                else
                {
                    if (tipoBD.Contains("date"))
                    {
                        return "DateTime";
                    }
                    else
                    {
                        return "object";
                    }
                }
            }
        }
    }
}
