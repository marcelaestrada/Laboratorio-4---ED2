using CompresorLZW.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompresorLZW.Estructuras
{
    public class LZW : InterfazLZW
    {
        Compresor compresor = new Compresor();
        Dictionary<string, int> original = new Dictionary<string, int>();
        string resultado = "";
        int ceros = 0;

        public string Comprimir(string cadena)
        {
            string comprimido = "";
            string cadenaCeros = "";
            List<int> mensajeNumeros = new List<int>();
            List<int> paraASCII = new List<int>();
            List<string> cadenaBinarios = new List<string>();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            int minBitesMayor = 0;

            diccionario = compresor.DiccionarioOriginal(cadena);
            
            foreach(var item in diccionario)
            {
                original.Add(item.Key, item.Value);
            }

            mensajeNumeros = compresor.CadenaAIndex(diccionario, cadena, diccionario.Count);
            minBitesMayor = compresor.MinBitesValorMayor(mensajeNumeros.Max());
            comprimido = compresor.CadenaDeByteComprimidos(mensajeNumeros, minBitesMayor);
            ceros = compresor.CantidadCerosExtra(comprimido);
            cadenaCeros = compresor.GenerarCerosExtra(compresor.CantidadCerosExtra(comprimido));
            comprimido += cadenaCeros;
            cadenaBinarios = compresor.codigosSplit(8, comprimido);


            foreach(var item in cadenaBinarios)
            {
                int numero = compresor.CadenaBinAInt(item);
                paraASCII.Add(numero);
            }


            foreach (var item in paraASCII)
            {
                resultado += Convert.ToChar(item);
            }
        

            return resultado;
        }

        public string Archivo()
        {
            string lineaArchivo = "";
            int caracteres = original.Count;

            lineaArchivo += Convert.ToChar(ceros);
            lineaArchivo += Convert.ToChar(caracteres);

            foreach(var item in original)
            {
                lineaArchivo += Convert.ToChar(item.Key);
            }

            lineaArchivo += resultado;

            return lineaArchivo;
        }

        public string Descomprimir(string comprimido)
        {
            // System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
            //byte[] bytesLinea = codificador.GetBytes(comprimido);
            List<int> bytesLinea = new List<int>();
            foreach (var item in comprimido)
            {
                bytesLinea.Add(Convert.ToInt32(item));
            }
            List<int> recuperado = new List<int>();
            Dictionary<string, int> diccionarioRecuperado = new Dictionary<string, int>();
            string cadenaOriginal = "";
            string descomprimido = "";
            string binario = "";
            int ceros = bytesLinea[0];
            int caracteres = bytesLinea[1];

            for (int i = 2; i <= caracteres+1; i++)
            {
                cadenaOriginal += Convert.ToChar(bytesLinea[i]);
            }

            diccionarioRecuperado = compresor.DiccionarioOriginal(cadenaOriginal);
            compresor.CadenaAIndex(diccionarioRecuperado, cadenaOriginal, diccionarioRecuperado.Count);

            for(int i = caracteres + 2; i < bytesLinea.Count; i++)
            {
                binario += Convert.ToString(bytesLinea[i], 2).PadLeft(8, '0');
            }

            //Quitar los N Ceros al final de la cadena y dividir en la cantidad de bites del número mayor
            //convertir esos grupos de ceros a su correlativo en entero para formar la cadena de números original
            //Ir creando el diccionario para poder descomprimir... (revisar clase de descompresión)


            return descomprimido;

        }
    }
}
