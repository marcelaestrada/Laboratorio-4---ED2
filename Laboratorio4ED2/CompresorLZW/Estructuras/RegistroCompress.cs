using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio4ED2
{
    public class RegistroCompress
    {
        public string NombreOriginal { get; set; }
        public string NombreComprimido { get; set; }
        public string RutaF { get; set; }
        public decimal RazonCompresion { get; set; }
        public decimal FactorCompresion { get; set; }
        public decimal PorcentajeReduccion { get; set; }

        public override string ToString()
        {
            return $"{NombreOriginal}|{NombreComprimido}|{RutaF}|{RazonCompresion}|{FactorCompresion}|{PorcentajeReduccion}";
        }
    }
}
