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
            MostrarEnIndice = true;
        }

        public bool EsArchivo
        {
            get
            {
                return Observaciones.ToLower().Contains("file") || Observaciones.ToLower().Contains("image");
            }
        }
    }
}
