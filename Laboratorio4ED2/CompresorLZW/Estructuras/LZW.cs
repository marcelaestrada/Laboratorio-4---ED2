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
            string cadenaCeros = "";
            string resultado = "";
            Compresor compresor = new Compresor();
            List<int> mensajeNumeros = new List<int>();
            List<int> paraASCII = new List<int>();
            List<string> cadenaBinarios = new List<string>();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            int minBitesMayor = 0;

            diccionario = compresor.DiccionarioOriginal(cadena);
            mensajeNumeros = compresor.CadenaAIndex(diccionario, cadena, diccionario.Count);
            minBitesMayor = compresor.MinBitesValorMayor(mensajeNumeros.Max());
            comprimido = compresor.CadenaDeByteComprimidos(mensajeNumeros, minBitesMayor);
            cadenaCeros = compresor.GenerarCerosExtra(compresor.CantidadCerosExtra(comprimido));
            comprimido += cadenaCeros;
            cadenaBinarios = compresor.codigosSplit(8, comprimido);

            foreach(var item in cadenaBinarios)
            {
                int numero = compresor.CadenaBinAInt(item);
                paraASCII.Add(numero);
            }

            foreach(var item in paraASCII)
            {
                resultado += Convert.ToChar(item);
            }

            return resultado;
        }

        public string Descomprimir(string cadena)
        {
            throw new NotImplementedException();
        }
    }
}
