using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Interfaz
{
    interface InterfazLZW
    {
        string Comprimir(string cadena);
        string Archivo(string nombre);
        string NombreOriginal();
        string Descomprimir(string cadena);
    }
}
