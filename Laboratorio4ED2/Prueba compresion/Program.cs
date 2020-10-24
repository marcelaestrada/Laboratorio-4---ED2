using CompresorLZW.Estructuras;
using System;
using System.IO;

namespace Prueba_compresion
{
    public class Program
    {
        static void Main(string[] args)
        {
            //CompresorLZW.Estructuras.Compresor compresor = new CompresorLZW.Estructuras.Compresor();
            CompresorLZW.Estructuras.LZW lZW = new CompresorLZW.Estructuras.LZW();
            FileStream file = new FileStream(@"C:\Users\marce\Desktop\2020\Semestre II 2020\Estructura de datos II\Laboratorio\Laboratorio-4---ED2\Laboratorio4ED2\Prueba compresion\Cuesta.txt", FileMode.Open, FileAccess.Read);
            //FileStream file = new FileStream(@"C:\Users\Usuario DELL\Desktop\4to semestre 2020\Estructura de datos II\LAB\Laboratorio-4---ED2\Laboratorio4ED2\Prueba compresion\Cuesta.txt", FileMode.Open, FileAccess.Read);
            StreamReader lector = new StreamReader(file);
            string texto = lector.ReadToEnd();
            var diccionarioOriginal = lZW.Comprimir(texto);

            //lZW.Comprimir("Muy buenos dias jovenes, como estan?");
            //lZW.Comprimir("Guatemala feliz...! que tus aras no profane jamás el verdugo; ni haya esclavos que laman el yugo ni tiranos que escupan tu faz." +
               //"Si mañana tu suelo sagrado lo amenaza invasión extranjera, libre al viento tu hermosa bandera a vencer o a morir llamará.");
          
           string desc = lZW.Descomprimir(lZW.Archivo());

            int flag = 0;
        }
    }
}
