using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Estructuras
{
    public class DatosCompresion
    {
        public string nombreOriginal { get; set; }
        public string nombreComprimido { get; set; }
        public string rutaComprimido { get; set; }
        public decimal razonCompresion { get; set; }
        public decimal factorCompresion { get; set; }
        public decimal porcentajeCompresion { get; set; }
    }
}
