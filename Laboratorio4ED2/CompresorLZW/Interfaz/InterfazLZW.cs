using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Interfaz
{
    interface InterfazLZW
    {
        string Comprimir(string cadena, string nombre, string nombreNuevo, string ruta);
        string Archivo();
        void llenarJSON();
        string Descomprimir(string cadena);
    }
}
