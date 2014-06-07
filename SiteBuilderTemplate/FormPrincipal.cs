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
using SiteBuilderTemplate.Clases;

namespace SiteBuilderTemplate
{
    public partial class FormPrincipal : Form
    {
        Tabla tabla;

        private enum Destino
        {
            MySql,
            SqlServer,
            Oracle,
            Mongo
        }

        public const string cadenaConexion = "Server=localhost;Database=ecocore;Uid=root;Pwd=12345;";
        public const string rutaPlantillas = @"C:\Users\USUARIO\Documents\visual studio 2012\Projects\SiteBuilderTemplate\SiteBuilderTemplate\Plantillas\";
        public const string plantillaIndiceUI = "IndexUI.html";
        public const string plantillaEditUI = "EditUI.html";
        public const string plantillaCreateUI = "CreateUI.html";
        public const string plantillaClase = "ModelClass.html";
        public const string plantillaBO = "BOClass.html";
        public const string plantillaBOConcreta = "ConcreteBOClass.html";
        public const string plantillaDAL = "DALClass.html";
        public const string plantillaDALConcreto = "ConcreteDALClass.html";
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

            txtNombreBD.Text = cboSchemas.Text;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {   
            EjecutarProceso(tabla, Destino.MySql);
        }

        private void EjecutarProceso(Tabla tabla,Destino destino)
        {
            List<Columna> columnas = tabla.Columnas;
            string nombreTabla = tabla.Nombre;

            //Generar textos para plantillas
            string nombreNamespace = txtNamespace.Text;
            string nombreModelo = txtNombreModelo.Text;
            string nombreDatos = txtNombreDatos.Text;
            string prefijoModelo = txtPrefijoModelo.Text;
            string prefijoDatos = txtPrefijoDatos.Text;
            string prefijoNegocio = txtPrefijoNegocio.Text;
            string nombreModeloCompleto = nombreNamespace + "." + nombreModelo + "." + Pascal(nombreTabla) + prefijoModelo;
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
            string rutaParaExportar = txtRutaExportar.Text + @"\" + cboSchemas.Text;
            string tipoDatoClave = "";

            //Generación de código de UI
            string textoPlantillaIndiceUI = File.ReadAllText(rutaPlantillas + plantillaIndiceUI);
            string textoPlantillaEditUI = File.ReadAllText(rutaPlantillas + plantillaEditUI);
            string textoPlantillaClase = File.ReadAllText(rutaPlantillas + plantillaClase);
            string textoPlantillaCreateUI = File.ReadAllText(rutaPlantillas + plantillaCreateUI);
            string textoPlantillaBO = File.ReadAllText(rutaPlantillas + plantillaBO);
            string textoPlantillaBOConcreta = File.ReadAllText(rutaPlantillas + plantillaBOConcreta);
            string textoPlantillaDAL = File.ReadAllText(rutaPlantillas + plantillaDAL);
            string textoPlantillaDALConcreto = File.ReadAllText(rutaPlantillas + plantillaDALConcreto);
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
            string textoPropiedadesArchivo = "";            
            string textoCamposArchivo = "";
            string textoHiddenArchivosEdit = "";
            string textoSaveFiles = "";
            string clave = "";
            bool textAreaAdicionado = false;
            string textoNegocioCreador = "";
            string textoNegocioFechaCreacion = "";

            foreach (Columna item in columnas)
            {
                bool clavePrimaria = false;
                string tipoDatoCompleto = "";

                if (item.Tamano> 0)
                {
                    tipoDatoCompleto = item.Tipo + "(" + item.Tamano.ToString() + ")";
                }
                else
                {
                    tipoDatoCompleto = item.Tipo;
                }

                if (item.Primaria)
                {
                    clave = item.Nombre;
                    tipoDatoClave = tipoDatoCompleto;

                    clavePrimaria = true;
                }

                //Campos SP
                textoCamposSP += "\r\n\t\t" + item.Nombre + ",";
                textoParametros += " p" + item.Nombre + ",";
                textoParametrosConTipo += " p" + item.Nombre + " " + tipoDatoCompleto + ",";

                //Archivo?
                if (item.EsArchivo || item.SoloLectura)
                {
                    if (item.EsArchivo)
                    {
                        //Parametros Archivo
                        textoPropiedadesArchivo += "\r\n\t public HttpPostedFileBase file" + item.Nombre + " { get; set; }";

                        textoCamposArchivo += "\r\n\t" + "<div class=\"editor-label\">" +
                                "\r\n\t @Html.LabelFor(model => model." + item.Nombre + ")" +
                                "\r\n\t" + "</div>" +
                                "\r\n\t" + "<div class=\"editor-field-file\">" +
                                "\r\n\t\t <input type='file' name=\"file" + item.Nombre + "\">" +
                                "\r\n\t\t @if(Model != null) {  Html.RenderAction(\"FileDisplay\", \"Admin\", new { url = Model." + item.Nombre + " }); } " +
                                "\r\n\t" + "</div>";

                        textoSaveFiles +=
                            "\r\n\t if (" + nombreTabla + ".file" + item.Nombre + " != null){" +
                            "\r\n\t string filePath = FileManager.SaveFile(" + nombreTabla + ".file" + item.Nombre + ");" +
                            "\r\n\t if (!String.IsNullOrEmpty(filePath))" +
                            "\r\n\t\t {" +
                            "\r\n\t" + nombreTabla + "." + item.Nombre + " = filePath;" +
                            "\r\n\t\t };" +
                            "\r\n\t }";
                    }                    

                    textoHiddenArchivosEdit += "\r\n\t @Html.HiddenFor(model => model." + item.Nombre + ")";
                }            

                if (!clavePrimaria)
                {
                    textoCamposSPInsert += "\r\n\t\t" + item.Nombre + ",";
                    textoParametrosInsert += " p" + item.Nombre + ",";
                    textoSetParametros += item.Nombre + " = p" + item.Nombre + ",";
                }


                if(item.MostrarEnIndice)
                {
                    //Encabezado Indice UI
                    textoEncabezadoIndiceUI += "\r\n\t" + encabezadoIndice.Replace("@@", item.Nombre);

                    //Cuerpo Indice UI
                    if (item.Tamano > 0 && item.Tamano > maximoCaracteresTabla)
                    {
                        textoCuerpoIndiceUI += "\r\n\t" + cuerpoIndice.Replace("@@", "@WebSite.WebUtilities.Misc.PreviewText(item." + item.Nombre + "," + maximoCaracteresTabla.ToString() + ")");
                    }
                    else
                    {
                        textoCuerpoIndiceUI += "\r\n\t" + cuerpoIndice.Replace("@@", "@item." + item.Nombre);
                    }
                }


                string tipoMapeo = "String";
                //Mapeo Datos
                if (MapearTipoDato(item.Tipo) == "int")
                {
                    tipoMapeo = "Int32";
                }
                else
                {
                    if (MapearTipoDato(item.Tipo) == "DateTime")
                    {
                        tipoMapeo = "DateTime";
                    }
                    else
                    {
                        if (MapearTipoDato(item.Tipo) == "bool")
                        {

                            tipoMapeo = "Boolean";
                        }
                    }
                }

                textoCuerpoGetData += "\r\n\t if (item[\"" + item.Nombre + "\"].GetType() != typeof(DBNull))" +
                                "\r\n\t {" +
                                "\r\n\t " + nombreTabla + "." + item.Nombre + " = Convert.To" + tipoMapeo + "(item[\"" + item.Nombre + "\"]);" +
                                "\r\n\t }";
                

                //Set Data
                textoCuerpoSetData += "\r\n\t " + "MySqlParameter param" + item.Nombre + " = new MySqlParameter(\"p" + item.Nombre +
                    "\"," + nombreTabla + "." + item.Nombre + ");";
                textoCuerpoSetData += "\r\n\t " + "param" + item.Nombre + ".Direction = ParameterDirection.Input;";
                textoCuerpoSetData += "\r\n\t " + "adapter.SelectCommand.Parameters.Add(param" + item.Nombre + ");";

                //La clave primaria no se puede editar, los archivos se muestran diferente
                if (!clavePrimaria && !item.EsArchivo)
                {
                    if (item.Tamano > 0 && item.Tamano > caracteresTextArea)
                    {
                        //Cuerpo Edit UI
                        textoCuerpoEditUI +=
                            "\r\n\t" + "<div class=\"editor-label\">" +
                            "\r\n\t @Html.LabelFor(model => model." + item.Nombre + ")" +
                            "\r\n\t" + "</div>" +
                            "\r\n\t" + "<div class=\"editor-field\">" +
                            "\r\n\t <textarea class=\"ckeditor\" name=\"" + item.Nombre + "\" >@if(Model != null){ @Model." + item.Nombre + "; }</textarea>" +
                            "\r\n\t @Html.ValidationMessageFor(model => model." + item.Nombre + ")" +
                            "\r\n\t" + "</div>";

                        textAreaAdicionado = true;
                    }
                    else
                    {
                        string metodoHTML = "Editor";
                        if(item.SoloLectura)
                        {
                            metodoHTML = "Display";
                        }

                        if (item.Tamano > 0)
                        {
                            //Cuerpo Edit UI
                            textoCuerpoEditUI +=
                                "\r\n\t" + "<div class=\"editor-label\">" +
                                "\r\n\t @Html.LabelFor(model => model." + item.Nombre + ")" +
                                "\r\n\t" + "</div>" +
                                "\r\n\t" + "<div class=\"editor-field\">" +
                                "\r\n\t @Html." + metodoHTML +  "For(model => model." + item.Nombre + ", new { maxLength = " + item.Tamano.ToString() + " })" +
                                "\r\n\t @Html.ValidationMessageFor(model => model." + item.Nombre + ")" +
                                "\r\n\t" + "</div>";
                        }
                        else
                        {
                            //Cuerpo Edit UI
                            textoCuerpoEditUI +=
                                "\r\n\t" + "<div class=\"editor-label\">" +
                                "\r\n\t @Html.LabelFor(model => model." + item.Nombre + ")" +
                                "\r\n\t" + "</div>" +
                                "\r\n\t" + "<div class=\"editor-field\">" +
                                "\r\n\t @Html." + metodoHTML + "For(model => model." + item.Nombre + ", new { maxLength = 10})" +
                                "\r\n\t @Html.ValidationMessageFor(model => model." + item.Nombre + ")" +
                                "\r\n\t" + "</div>";
                        }


                    }


                }

                //Propiedadaes Clase
                textoPropiedades += "\r\n\t" + propiedadaesClase.Replace("@@", MapearTipoDato(item.Tipo) + " " + item.Nombre);

            }

            textoCamposSP = textoCamposSP.Substring(0, textoCamposSP.Length - 1);
            textoParametros = textoParametros.Substring(0, textoParametros.Length - 1);
            textoCamposSPInsert = textoCamposSPInsert.Substring(0, textoCamposSPInsert.Length - 1);
            textoParametrosInsert = textoParametrosInsert.Substring(0, textoParametrosInsert.Length - 1);
            textoSetParametros = textoSetParametros.Substring(0, textoSetParametros.Length - 1);
            textoParametrosConTipo = textoParametrosConTipo.Substring(0, textoParametrosConTipo.Length - 1);

            if(tabla.TieneColumnaCreador)
            {
                textoNegocioCreador = "\r\n\t if (!SecurityManager.SesionStarted())" +
                    "\r\n\t {" +
                    "\r\n\t throw new Exception(\"Session not started\");" +
                    "\r\n\t }" +
                    "\r\n\t " + nombreTabla + ".CreatedBy = SecurityManager.GetLoggedUser()." + clave + ";";
            }
     
            if(tabla.TieneColumnaFechaCreacion)
            {
                textoNegocioFechaCreacion = "\r\n\t " + nombreTabla + ".DateCreated = DateTime.Now;";
            }

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
            string textoCreateUI = textoPlantillaCreateUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Cuerpo", textoCuerpoEditUI + textoCamposArchivo)
                .Replace("@@Clave", clave);
            File.WriteAllText(directorioUI.FullName + "\\Create.cshtml", textoCreateUI);
            txtCodigo.Text += "\r\n\r\n" + textoCreateUI;

            txtCodigo.Text += "\r\n\r\n EDIT UI \r\n\r\n";
            string textoEditUI = textoPlantillaEditUI.Replace("@@Nombre", nombreNegocioAlias)
                .Replace("@@Modelo", nombreModeloCompleto)
                .Replace("@@Layout", layout)
                .Replace("@@Cuerpo", textoCuerpoEditUI + textoCamposArchivo + textoHiddenArchivosEdit)
                .Replace("@@Clave", clave);
            File.WriteAllText(directorioUI.FullName + "\\Edit.cshtml", textoEditUI);
            txtCodigo.Text += "\r\n\r\n" + textoEditUI;

            txtCodigo.Text += "\r\n\r\n MODELO \r\n\r\n";
            string textoModelo = textoPlantillaClase.Replace("@@Namespace", nombreNamespace + "." + nombreModelo)
                .Replace("@@Clase", Pascal(nombreTabla) + txtPrefijoModelo.Text)
                .Replace("@@Campos", textoPropiedades + textoPropiedadesArchivo);

            DirectoryInfo directorioModelo = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Models");
            File.WriteAllText(directorioModelo.FullName + "\\" + Pascal(nombreTabla) + txtPrefijoModelo.Text + ".cs", textoModelo);
            txtCodigo.Text += "\r\n\r\n" + textoModelo;

            txtCodigo.Text += "\r\n\r\n BO \r\n\r\n";

            DirectoryInfo directorioNegocio = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Business");
            string textoBO = textoPlantillaBO.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Clave", clave)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos)
                .Replace("@@SaveFiles",textoSaveFiles);

            File.WriteAllText(directorioNegocio.FullName + "\\Base" + Pascal(nombreTabla) + prefijoNegocio + ".cs", textoBO);
            txtCodigo.Text += "\r\n\r\n" + textoBO;

            DirectoryInfo directorioNegocioConcreto = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Business");
            string textoBOConcreto = textoPlantillaBOConcreta.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@NombreClase", nombreTabla)                
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@Creador", textoNegocioCreador)
                .Replace("@@FechaCreacion", textoNegocioFechaCreacion);

            File.WriteAllText(directorioNegocioConcreto.FullName + "\\_" + Pascal(nombreTabla) + prefijoNegocio + ".cs", textoBOConcreto);
            txtCodigo.Text += "\r\n\r\n" + textoBOConcreto;


            txtCodigo.Text += "\r\n\r\n DAL \r\n\r\n";

            DirectoryInfo directorioDatos = Directory.CreateDirectory(rutaParaExportar + @"\" + Pascal(nombreTabla) + @"\BusinessManager\Data");
            string textoDAL = textoPlantillaDAL.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos)
                .Replace("@@Clase", Pascal(nombreTabla))
                .Replace("@@CuerpoMapeo", textoCuerpoGetData)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@SetData", textoCuerpoSetData);

            File.WriteAllText(directorioDatos.FullName + "\\Base" + Pascal(nombreTabla) + prefijoDatos + ".cs", textoDAL);
            txtCodigo.Text += "\r\n\r\n" + textoDAL;

            string textoConcretoDAL = textoPlantillaDALConcreto.Replace("@@Namespace", nombreNamespace)                
                .Replace("@@NombreDatos", nombreDatos)
                .Replace("@@ClaseDatos", Pascal(nombreTabla) + prefijoDatos);

            File.WriteAllText(directorioDatos.FullName + "\\" + Pascal(nombreTabla) + prefijoDatos + ".cs", textoConcretoDAL);
            txtCodigo.Text += "\r\n\r\n" + textoConcretoDAL;


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
            string textoController = textoPlantillaController.Replace("@@Namespace", nombreNamespace)
                .Replace("@@NombreModelo", nombreModelo)
                .Replace("@@NombreNegocio", nombreNegocio)
                .Replace("@@ClaseNegocio", Pascal(nombreTabla) + prefijoNegocio)
                .Replace("@@NombreClase", nombreTabla)
                .Replace("@@Modelo", Pascal(nombreTabla) + prefijoModelo)
                .Replace("@@Clase", Pascal(nombreTabla));

            File.WriteAllText(directorioControlador.FullName + "\\Manage" + Pascal(nombreTabla) + "Controller.cs", textoController);
            txtCodigo.Text += "\r\n\r\n" + textoController;
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
                            return "string";
                        }                        
                    }
                }
            }
        }

        private void cboTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreTabla = cboTablas.Text;

            string consultaSchemas =
                "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, COLUMN_KEY, COLUMN_COMMENT " +
                " FROM INFORMATION_SCHEMA.COLUMNS " +
                " WHERE TABLE_SCHEMA ='" + cboSchemas.Text + "' " +
                " AND TABLE_NAME ='" + nombreTabla + "' ";

            MySqlConnection connection = new MySqlConnection(cadenaConexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSchemas, connection);
            DataTable results = new DataTable();
            adapter.Fill(results);

            tabla = new Tabla(); 
            
            tabla.Nombre = nombreTabla;
            foreach (DataRow item in results.Rows)
            {
                tabla.AdicionarColumna(new Columna()
                {
                    Nombre = item["COLUMN_NAME"].ToString(),
                    Tipo = item["DATA_TYPE"].ToString(),
                    Tamano = item["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull) ? Convert.ToInt32(item["CHARACTER_MAXIMUM_LENGTH"]) : 0,
                    Primaria = item["COLUMN_KEY"].ToString().ToLower() == "pri",
                    Observaciones = item["COLUMN_COMMENT"].ToString()
                });
            }

            dgvColumnas.DataSource = tabla.Columnas;            
            
        }

        private void dgvColumnas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                tabla.CambiarEstadoMostrarColumna(e.RowIndex);
            }
        }

        private void dgvColumnas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
