using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteBuilderTemplate.Clases
{
    public class Columna
    {
        public string Nombre { get; set; }
        public bool MostrarEnIndice { get; set; }
        public string Tipo { get; set; }
        public int Tamano { get; set; }
        public bool Primaria { get; set; }
        public bool Foranea { get; set; }        
        public string Observaciones { get; set; }        

        public Columna()
        {
            MostrarEnIndice = false;            
        }

        public bool EsArchivo
        {
            get
            {
                return Observaciones.ToLower().Contains("file") || Observaciones.ToLower().Contains("image");
            }
        }

        public bool SoloLectura
        {
            get
            {
                return Observaciones.ToLower().Contains("readonly");
            }
        }

        public bool EsPassword
        {
            get
            {
                return Observaciones.ToLower().Contains("password");
            }
        }

        public string NombreTablaPrimaria { get; set; }

    }
}
