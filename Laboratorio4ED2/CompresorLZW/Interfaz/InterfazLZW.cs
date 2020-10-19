using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Interfaz
{
    interface InterfazLZW
    {
        string Comprimir(string cadena);
        string Archivo();
        string Descomprimir(string cadena);
    }
}
