﻿using CompresorLZW.Estructuras;
using System;
using System.IO;

namespace Prueba_compresion
{
    public class Program
    {
        static void Main(string[] args)
        {
            CompresorLZW.Estructuras.Compresor compresor = new CompresorLZW.Estructuras.Compresor();
            CompresorLZW.Estructuras.LZW lZW = new CompresorLZW.Estructuras.LZW();
            //FileStream file = new FileStream(@"C:\Users\marce\Desktop\2020\Semestre II 2020\Estructura de datos II\Laboratorio\Laboratorio-4---ED2\Laboratorio4ED2\Prueba compresion\Cuesta.txt", FileMode.Open, FileAccess.Read);
            //FileStream file = new FileStream(@"C:\Users\Usuario DELL\Desktop\4to semestre 2020\Estructura de datos II\LAB\Laboratorio-4---ED2\Laboratorio4ED2\Prueba compresion\Cuesta.txt", FileMode.Open, FileAccess.Read);
            //StreamReader lector = new StreamReader(file);
            //string texto = lector.ReadToEnd();
            //var diccionarioOriginal = compresor.DiccionarioOriginal(texto);

            lZW.Comprimir("Joshua Valey");
            lZW.Descomprimir(lZW.Archivo());
            
            
        }
    }
}
