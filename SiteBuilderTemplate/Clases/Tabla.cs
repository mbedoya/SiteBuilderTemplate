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

        private bool tieneColumnaCreador;
        public bool TieneColumnaCreador
        {
            get { return tieneColumnaCreador; }            
        }

        private bool tieneColumnaFechaCreacion;
        public bool TieneColumnaFechaCreacion
        {
            get { return tieneColumnaFechaCreacion; }            
        }

        public string NombreForaneaPrimaria { get; set; }
        public string NombreOriginalForanea { get; set; }
        public bool TieneForaneaPrimaria { get; set; }

        public string NombreTablaHija { get; set; }

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

            if (columna.Nombre.ToLower() == "createdby")
            {
                tieneColumnaCreador = true;
            }

            if (columna.Nombre.ToLower() == "datecreated")
            {
                tieneColumnaFechaCreacion = true;
            }

            if (columna.Foranea && columna.Observaciones.ToLower().Contains("foreign"))
            {
                TieneForaneaPrimaria = true;
                NombreForaneaPrimaria = columna.Observaciones.Split(',')[0];
                columna.NombreTablaPrimaria = columna.Observaciones.Split(',')[0];
                NombreOriginalForanea = columna.Nombre;
            }
            else
            {
                if (columna.Foranea)
                {
                    if (columna.Observaciones.Contains(","))
                    {
                        columna.NombreTablaPrimaria = columna.Observaciones.Split(',')[0];
                    }
                    else
                    {
                        columna.NombreTablaPrimaria = columna.Observaciones;
                    }
                    
                }
            }

            Columnas.Add(columna);
        }

        public void CambiarEstadoMostrarColumna(int indice)
        {
            Columnas[indice].MostrarEnIndice = !Columnas[indice].MostrarEnIndice;
        }
    }
}
