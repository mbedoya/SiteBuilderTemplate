using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteBuilderTemplate.Clases
{
    public class Tabla
    {
        private bool tieneArchivos;

        public string Nombre { get; set; }
        public List<Columna> Columnas { get; set; }
        public bool TieneArchivos {
            get { return tieneArchivos; }            
        }

        public Tabla()
        {
            Columnas = new List<Columna>();
        }

        public void AdicionarColumna(Columna columna)
        {
            if(columna.EsArchivo)
            {
                tieneArchivos = true;
            }
            Columnas.Add(columna);
        }

        public void CambiarEstadoMostrarColumna(int indice)
        {
            Columnas[indice].MostrarEnIndice = !Columnas[indice].MostrarEnIndice;
        }
    }
}
