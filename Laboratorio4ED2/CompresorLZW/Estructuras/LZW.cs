﻿using CompresorLZW.Interfaz;
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
        int minBitesMayor = 0;
        int ceros = 0;

        public string Comprimir(string cadena)
        {
            string vacio = " ";
            cadena = cadena.Replace("\r\n", "\n");
            string comprimido = "";
            string cadenaCeros = "";
            List<int> mensajeNumeros = new List<int>();
            List<int> paraASCII = new List<int>();
            List<string> cadenaBinarios = new List<string>();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

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
            lineaArchivo += Convert.ToChar(minBitesMayor);

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

            Descompresor descompresor = new Descompresor();
            List<int> recuperado = new List<int>();
            Dictionary<string, int> diccionarioRecuperado = new Dictionary<string, int>();
            List<string> bitesLinea = new List<string>();
            List<string> cadenaBinarios = new List<string>();
            List<int> numeros = new List<int>();
            string cadenaOriginal = "";
            string descomprimido = "";
            string binario = "";
            string cadena = "";
            int ceros = bytesLinea[0];
            int caracteres = bytesLinea[1];
            int bites = bytesLinea[2];

            for (int i = 3; i <= caracteres+2; i++)
            {
                cadenaOriginal += Convert.ToChar(bytesLinea[i]);
            }

            diccionarioRecuperado = compresor.DiccionarioOriginal(cadenaOriginal);

            for(int i = caracteres + 3; i < bytesLinea.Count; i++)
            {
                binario += Convert.ToString(bytesLinea[i], 2).PadLeft(8, '0');
            }

            foreach(var item in binario)
            {
                bitesLinea.Add(Convert.ToString(item));
            }

            for(int i = 0; i < binario.Length - ceros; i++)
            {
                cadena += binario[i];
            }

            cadenaBinarios = compresor.codigosSplit(bites, cadena);

            foreach(var item in cadenaBinarios)
            {
                numeros.Add(Convert.ToInt32(item, 2));
            }

           var newDic =  descompresor.reconstruirDiccionario(diccionarioRecuperado, numeros);
            int flag = 0;

            
            foreach (var item in numeros)
            {
                descomprimido += newDic[item];
            }

            return descomprimido;

        }
    }
}
