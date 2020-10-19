using CompresorLZW.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompresorLZW.Estructuras
{
    public class LZW : InterfazLZW
    {
        public string Comprimir(string cadena)
        {
            string comprimido = "";
            Compresor compresor = new Compresor();
            List<int> mensajeNumeros = new List<int>(); 
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            int minBitesMayor = 0;

            diccionario = compresor.DiccionarioOriginal(cadena);
            mensajeNumeros = compresor.CadenaAIndex(diccionario, cadena, diccionario.Count);
            minBitesMayor = compresor.MinBitesValorMayor(mensajeNumeros.Max());
            comprimido = compresor.CadenaDeByteComprimidos(mensajeNumeros, minBitesMayor);

            return comprimido;
        }

        public string Descomprimir(string cadena)
        {
            throw new NotImplementedException();
        }
    }
}
