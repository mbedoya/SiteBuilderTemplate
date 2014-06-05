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
        public const string plantillaIndiceUI = "IndexUI.html";
        public const string plantillaEditUI = "EditUI.html";
        public const string plantillaCreateUI = "CreateUI.html";
        public const string plantillaClase = "ModelClass.html";
        public const string plantillaBO = "BOClass.html";
        public const string plantillaDAL = "DALClass.html";
        public const string plantillaSPGET = "ProceduresGET.html";
        public const string plantillaController = "Controller.html";

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
            string nombreNamespace = txtNamespace.Text;
            string nombreModelo = txtNombreModelo.Text;
            string nombreDatos = txtNombreDatos.Text;
            string prefijoModelo = txtPrefijoModelo.Text;
            string prefijoDatos = txtPrefijoDatos.Text;
            string prefijoNegocio = txtPrefijoNegocio.Text; 
            string nombreModeloCompleto = nombreNamespace + "." + nombreModelo  + "." + Pascal(nombreTabla) + prefijoModelo;
            string nombreNegocioAlias = txtNombre.Text;
            string nombreNegocio = txtNombreNegocio.Text;
            string nombrePrefijoNegocio = txtPrefijoNegocio.Text;
            string layout = txtLayout.Text;
            string encabezadoIndice = txtCssEncabezado.Text;
            string cuerpoIndice = txtCssCuerpo.Text;
            string propiedadaesClase = txtPropiedad.Text;
            int maximoCaracteresTabla = Convert.ToInt32(txtMaximoCaracteresTabla.Text);
            int caracteresTextArea = Convert.ToInt32(txtCarTextArea.Text);
            string nombreBD = txtNombreBD.Text;
            string rutaParaExportar = txtRutaExportar.Text;
            string tipoDatoClave = "";

            //Generación de código de UI
            string textoPlantillaIndiceUI = File.ReadAllText(rutaPlantillas + plantillaIndiceUI);
            string textoPlantillaEditUI = File.ReadAllText(rutaPlantillas + plantillaEditUI);
            string textoPlantillaClase = File.ReadAllText(rutaPlantillas + plantillaClase);
            string textoPlantillaCreateUI = File.ReadAllText(rutaPlantillas + plantillaCreateUI);
            string textoPlantillaBO = File.ReadAllText(rutaPlantillas + plantillaBO);
            string textoPlantillaDAL = File.ReadAllText(rutaPlantillas + plantillaDAL);
            string textoPlantillaSPGET = File.ReadAllText(rutaPlantillas + plantillaSPGET);
            string textoPlantillaController = File.ReadAllText(rutaPlantillas + plantillaController);

            string textoEncabezadoIndiceUI = "";
            string textoCuerpoIndiceUI = "";
            string textoCuerpoEditUI = "";
            string textoCuerpoGetData = "";
            string textoCuerpoSetData = "";
            string textoCamposSP = "";
            string textoCamposSPInsert = "";
            string textoParametrosConTipo = "";
            string textoParametros = "";
            string textoParametrosInsert = "";
            string textoSetParametros = "";
            string textoPropiedades = "";
            string clave = "";
            bool textAreaAdicionado = false;

            foreach (DataRow item in results.Rows)
            {
                bool clavePrimaria = false;
                string tipoDatoCompleto = "";

                if (item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull))
                {
                    tipoDatoCompleto = item["DATA_TYPE"].ToString() + "(" + item["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
                }
                else
                {
                    tipoDatoCompleto = item["DATA_TYPE"].ToString();
                }

                if (item["COLUMN_KEY"].ToString().ToLower() == "pri")
                {
                    clave = item["COLUMN_NAME"].ToString();
                    tipoDatoClave = tipoDatoCompleto;
                    
                    clavePrimaria = true;
                }

                //Campos SP
                textoCamposSP += "\r\n\t\t" + item["COLUMN_NAME"].ToString() + ",";
                textoParametros += " p" + item["COLUMN_NAME"].ToString() + ",";
                textoParametrosConTipo += " p" + item["COLUMN_NAME"].ToString() + " " + tipoDatoCompleto + ",";

                if (!clavePrimaria)
                {
                    textoCamposSPInsert += "\r\n\t\t" + item["COLUMN_NAME"].ToString() + ",";
                    textoParametrosInsert += " p" + item["COLUMN_NAME"].ToString() + ",";
                    textoSetParametros += item["COLUMN_NAME"].ToString() + " = p" + item["COLUMN_NAME"].ToString() + ",";
                }

                //Encabezado Indice UI
                textoEncabezadoIndiceUI += "\r\n\t" + encabezadoIndice.Replace("@@", item["COLUMN_NAME"].ToString());

                //Cuerpo Indice UI
                if (item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull) &&
                    Convert.ToInt32(item["CHARACTER_MAXIMUM_LENGTH"]) > maximoCaracteresTabla)
                {
                    textoCuerpoIndiceUI += "\r\n\t" + cuerpoIndice.Replace("@@", "@WebSite.WebUtilities.Misc.PreviewText(item." + item["COLUMN_NAME"].ToString() + "," + maximoCaracteresTabla.ToString() + ")");
                }
                else
                {
                    textoCuerpoIndiceUI += "\r\n\t" + cuerpoIndice.Replace("@@", "@item." + item["COLUMN_NAME"].ToString());
                }


                //Mapeo Datos
                if (MapearTipoDato(item["DATA_TYPE"].ToString()) == "string")
                {
                    textoCuerpoGetData += "\r\n\t " + nombreTabla + "." + item["COLUMN_NAME"].ToString() + " = Convert.ToString(item[\"" + item["COLUMN_NAME"].ToString() + "\"]);";
                }
                else
                {
                    if (MapearTipoDato(item["DATA_TYPE"].ToString()) == "int")
                    {
                        textoCuerpoGetData += "\r\n\t " + nombreTabla + "." + item["COLUMN_NAME"].ToString() + " = Convert.ToInt32(item[\"" + item["COLUMN_NAME"].ToString() + "\"]);";
                    }
                    else
                    {
                        if (MapearTipoDato(item["DATA_TYPE"].ToString()) == "DateTime")
                        {
                            textoCuerpoGetData += "\r\n\t " + nombreTabla + "." + item["COLUMN_NAME"].ToString() + " = Convert.ToDateTime(item[\"" + item["COLUMN_NAME"].ToString() + "\"]);";
                        }
                        else
                        {
                            if (MapearTipoDato(item["DATA_TYPE"].ToString()) == "bool")
                            {
                                textoCuerpoGetData += "\r\n\t " + nombreTabla + "." + item["COLUMN_NAME"].ToString() + " = Convert.ToBoolean(item[\"" + item["COLUMN_NAME"].ToString() + "\"]);";
                            }
                            else
                            {
                                textoCuerpoGetData += "\r\n\t " + nombreTabla + "." + item["COLUMN_NAME"].ToString() + " = item[\"" + item["COLUMN_NAME"].ToString() + "\"];";
                            }             
                        }
                    }
                }
                
                //Set Data
                textoCuerpoSetData += "\r\n\t " + "MySqlParameter param" + item["COLUMN_NAME"].ToString() + " = new MySqlParameter(\"p" + item["COLUMN_NAME"].ToString() +
                    "\"," + nombreTabla + "." + item["COLUMN_NAME"].ToString() + ");";
                textoCuerpoSetData += "\r\n\t " + "param" + item["COLUMN_NAME"].ToString() + ".Direction = ParameterDirection.Input;";
                textoCuerpoSetData += "\r\n\t " + "adapter.SelectCommand.Parameters.Add(param" + item["COLUMN_NAME"].ToString() + ");";

                //La clave primaria no se puede editar
                if (!clavePrimaria)
                {
                    if (item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull) &&
                    Convert.ToInt32(item["CHARACTER_MAXIMUM_LENGTH"]) > caracteresTextArea)
                    {
                        //Cuerpo Edit UI
                        textoCuerpoEditUI +=
                            "\r\n\t" + "<div class=\"editor-label\">" +
                            "\r\n\t @Html.LabelFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                            "\r\n\t" + "</div>" +
                            "\r\n\t" + "<div class=\"editor-field\">" +
                            "\r\n\t <textarea class=\"ckeditor\" name=\"" + item["COLUMN_NAME"].ToString() + "\" >@if(Model != null){ @Model." + item["COLUMN_NAME"].ToString() +"; }</textarea>" +
                            "\r\n\t @Html.ValidationMessageFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                            "\r\n\t" + "</div>";

                        textAreaAdicionado = true;
                    }
                    else
                    {
                        if (item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull))
                        {
                            //Cuerpo Edit UI
                            textoCuerpoEditUI +=
                                "\r\n\t" + "<div class=\"editor-label\">" +
                                "\r\n\t @Html.LabelFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                                "\r\n\t" + "</div>" +
                                "\r\n\t" + "<div class=\"editor-field\">" +
                                "\r\n\t @Html.EditorFor(model => model." + item["COLUMN_NAME"].ToString() + ", new { maxLength = " + item["CHARACTER_MAXIMUM_LENGTH"].ToString() + " })" +
                                "\r\n\t @Html.ValidationMessageFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                                "\r\n\t" + "</div>";                
                        }
                        else
                        {
                            //Cuerpo Edit UI
                            textoCuerpoEditUI +=
                                "\r\n\t" + "<div class=\"editor-label\">" +
                                "\r\n\t @Html.LabelFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                                "\r\n\t" + "</div>" +
                                "\r\n\t" + "<div class=\"editor-field\">" +
                                "\r\n\t @Html.EditorFor(model => model." + item["COLUMN_NAME"].ToString() + ", new { maxLength = 10})" +
                                "\r\n\t @Html.ValidationMessageFor(model => model." + item["COLUMN_NAME"].ToString() + ")" +
                                "\r\n\t" + "</div>";                
                        }

                        
                    }

                    
                }                

                //Propiedadaes Clase
                textoPropiedades += "\r\n\t" + propiedadaesClase.Replace("@@",
                    MapearTipoDato(item["DATA_TYPE"].ToString()) + " " + item["COLUMN_NAME"].ToString());
                
            }

            textoCamposSP = textoCamposSP.Substring(0, textoCamposSP.Length - 1);
            textoParametros = textoParametros.Substring(0, textoParametros.Length - 1);
            textoCamposSPInsert = textoCamposSPInsert.Substring(0, textoCamposSPInsert.Length - 1);
            textoParametrosInsert = textoParametrosInsert.Substring(0, textoParametrosInsert.Length - 1);
            textoSetParametros = textoSetParametros.Substring(0, textoSetParametros.Length - 1);
            textoParametrosConTipo = textoParametrosConTipo.Substring(0, textoParametrosConTipo.Length - 1);

            txtCodigo.Text = "";

            txtCodigo.Text += "INDICE UI \r\n\r\n";

            DirectoryInfo directorioUI = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\WebSite\Views\Manage" + Pascal(nombreTabla));

            File.WriteAllText(directorioUI.FullName + "\\Index.cshtml", textoPlantillaIndiceUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Encabezado", textoEncabezadoIndiceUI)
                .Replace("@@Cuerpo", textoCuerpoIndiceUI)
                .Replace("@@Clave", clave));
            txtCodigo.Text += textoPlantillaIndiceUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Encabezado", textoEncabezadoIndiceUI)
                .Replace("@@Cuerpo", textoCuerpoIndiceUI)
                .Replace("@@Clave", clave);

            txtCodigo.Text += "\r\n\r\n CREATE UI \r\n\r\n";

            File.WriteAllText(directorioUI.FullName + "\\Create.cshtml", textoPlantillaCreateUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Cuerpo", textoCuerpoEditUI)
                .Replace("@@Clave", clave)
                .Replace("@@Scripts", textAreaAdicionado ? "@section scripts{ @Scripts.Render(\"~/Scripts/ckeditor/ckeditor.js\") }" : ""));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaCreateUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Cuerpo", textoCuerpoEditUI)
                .Replace("@@Clave", clave)
                .Replace("@@Scripts", textAreaAdicionado ? "@section scripts{ @Scripts.Render(\"~/Scripts/ckeditor/ckeditor.js\") }" : "");

            txtCodigo.Text += "\r\n\r\n EDIT UI \r\n\r\n";

            File.WriteAllText(directorioUI.FullName + "\\Edit.cshtml", textoPlantillaEditUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Cuerpo", textoCuerpoEditUI)
                .Replace("@@Clave", clave)
                .Replace("@@Scripts", textAreaAdicionado ? "@section scripts{ @Scripts.Render(\"~/Scripts/ckeditor/ckeditor.js\") }" : ""));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaEditUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)                
                .Replace("@@Cuerpo", textoCuerpoEditUI)
                .Replace("@@Clave", clave)
                .Replace("@@Scripts", textAreaAdicionado ? "@section scripts{ @Scripts.Render(\"~/Scripts/ckeditor/ckeditor.js\") }" : "");
            
            txtCodigo.Text += "\r\n\r\n MODELO \r\n\r\n";

            DirectoryInfo directorioModelo = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Models");
            File.WriteAllText(directorioModelo.FullName + "\\" + Pascal(nombreTabla) + txtPrefijoModelo.Text + ".cs", textoPlantillaClase.Replace("@@Namespace", nombreNamespace + "." + nombreModelo)
                .Replace("@@Clase", Pascal(nombreTabla) + txtPrefijoModelo.Text)
                .Replace("@@Campos", textoPropiedades));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaClase.Replace("@@Namespace", nombreNamespace + "." + nombreModelo)
                .Replace("@@Clase", Pascal(nombreTabla) + txtPrefijoModelo.Text)
                .Replace("@@Campos", textoPropiedades);

            txtCodigo.Text += "\r\n\r\n BO \r\n\r\n";

            DirectoryInfo directorioNegocio = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Business");
            File.WriteAllText(directorioNegocio.FullName + "\\" + Pascal(nombreTabla) + prefijoNegocio + ".cs", textoPlantillaBO.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Clave", clave)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaBO.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Clave", clave)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos);

            txtCodigo.Text += "\r\n\r\n DAL \r\n\r\n";

            DirectoryInfo directorioDatos = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Data");
            File.WriteAllText(directorioDatos.FullName + "\\" + Pascal(nombreTabla) + prefijoDatos + ".cs", textoPlantillaDAL.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos)
                .Replace("@@Clase", Pascal(nombreTabla))
                .Replace("@@CuerpoMapeo", textoCuerpoGetData)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@SetData", textoCuerpoSetData));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaDAL.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)                
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)                
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos)
                .Replace("@@Clase", Pascal(nombreTabla))                
                .Replace("@@CuerpoMapeo", textoCuerpoGetData)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@SetData", textoCuerpoSetData);


            txtCodigo.Text += "\r\n\r\n SP GET \r\n\r\n";

            DirectoryInfo directorioSQL = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\SQL");
            File.WriteAllText(directorioSQL.FullName + "\\ScriptProcedures.sql", textoPlantillaSPGET.Replace("@@Clave", clave)
                .Replace("@@CamposInsert", textoCamposSPInsert)
                .Replace("@@ParametrosInsert", textoParametrosInsert)
                .Replace("@@TipoDatoClave", tipoDatoClave)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Clase", Pascal(nombreTabla))
                .Replace("@@Campos", textoCamposSP)
                .Replace("@@ParametrosConTipo", textoParametrosConTipo)
                .Replace("@@SetParametros", textoSetParametros)
                .Replace("@@Parametros", textoParametros)
                .Replace("@@NombreBD", nombreBD));
            txtCodigo.Text += "\r\n\r\n" + 
                textoPlantillaSPGET.Replace("@@Clave", clave)
                .Replace("@@CamposInsert", textoCamposSPInsert)
                .Replace("@@ParametrosInsert", textoParametrosInsert)
                .Replace("@@TipoDatoClave", tipoDatoClave)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Clase", Pascal(nombreTabla))
                .Replace("@@Campos", textoCamposSP)
                .Replace("@@ParametrosConTipo", textoParametrosConTipo)
                .Replace("@@SetParametros", textoSetParametros)
                .Replace("@@Parametros", textoParametros)
                .Replace("@@NombreBD", nombreBD);


            txtCodigo.Text += "\r\n\r\n Controller \r\n\r\n";

            DirectoryInfo directorioControlador = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\WebSite\Controllers\Admin");
            File.WriteAllText(directorioControlador.FullName + "\\Manage" + Pascal(nombreTabla) + "Controller.cs", textoPlantillaController.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@Clase", Pascal(nombreTabla)));
            txtCodigo.Text += "\r\n\r\n" + textoPlantillaController.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)                
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)                
                .Replace("@@Clase", Pascal(nombreTabla));
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
                        if (tipoBD.Contains("bit"))
                        {
                            return "bool";
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
}
