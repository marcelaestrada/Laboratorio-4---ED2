using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Estructuras
{
    public class Compresor
    {
        /// <summary>
        /// Este método recibe un texto string, lo recorre buscando 
        /// los caracteres diferentes que haya y los agrega al 
        /// diccionario que devuelve de tipo <string, int>
        /// key : string; value: index
        /// </summary>
        /// <param name="texto">Texto que se recorre para crear el diccionario original</param> 
        /// <returns> Retorna el diccionario original para la compresión del mensaje </returns>
        public Dictionary<string, int> DiccionarioOriginal(string texto)
        {
            Dictionary<string, int> inicio = new Dictionary<string, int>();
            System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
            byte[] bytesLinea = codificador.GetBytes(texto);
            string cadena;
            int contador = 1;
            int evaluar = 0;
            string[] bytes = new string[bytesLinea.Length];

            for (int i = 0; i < bytesLinea.Length; i++)
            {
                bytes[i] += Convert.ToChar(bytesLinea[i]);
            }

            foreach (var item in bytes)
            {
                if (!inicio.ContainsKey(item))
                {
                    inicio.Add(item, contador);
                    contador++;
                }
            }

            return inicio;
        }

        /// <summary>
        /// Este método utiliza el diccionario original para cifrar la cadena 
        /// remplazando los caracteres y subcadenas por los index del diccionario 
        /// original mientras se van agregando más sub cadenas al diccionario.
        /// y remplazando sub cadenas por más index.
        /// </summary>
        /// <param name="original">Diccionario original</param> 
        /// <returns> Retorna un listado de enteros que representa el mensaje. </returns>
        private List<int> CadenaAIndex(Dictionary<string, int> original)
        {
            Dictionary<string, int> diccionario = original;
            List<int> mensajeEnNumeros = new List<int>();
            return mensajeEnNumeros;
        }

        /// <summary>
        /// Convertir un listado de enteros a una cadena de 
        /// ceros y unos que represente los bytes del mensaje comprimido.
        /// </summary>
        /// <param name="cadenaIndex">Listado de enteros con el mensaje original</param> 
        /// <returns> La cadena de ceros y unos para convertir a bytes </returns>
        private string CadenaDeBytesComprimidos(List<int> cadenaIndex)
        {
            return "";
        }

        /// <summary>
        /// Convertir un listado de enteros a una cadena de 
        /// ceros y unos que represente los bytes del mensaje comprimido.
        /// </summary>
        /// <param name="cadenaIndex">Listado de enteros con el mensaje original</param> 
        /// <returns> La cadena de ceros y unos para convertir a bytes </returns>

        private List<string> ListadoDeASCII(List<int> data)
        {
            List<string> resultado = new List<string>();
            foreach(var item in data)
            {
                string binario = Convert.ToString(item, 2).PadLeft(8, '0');
                resultado.Add(binario);
            }
            return resultado;
        }


        /// <summary>
        /// Método que segun el int mayor
        /// devuelve la cantidad de bites que va a ocupar cada index 
        /// del mensaje original.<!-- -->
        /// </summary>
        /// <param name="mayor">Entero mayor del menseaje comprimido</param> 
        /// <returns> Cantidad de bytes por cadena.<!-- --> </returns>
        private int CantidadMenorDeBitesDeMayor(int mayor)
        {
            return 1;
        }


        /// <summary>
        /// Metodo que recibe un elemento entero de la lista de enteros con elmensaje
        /// y lo convierte a un entero de bits rellenado con los ceros del mayor numero 
        /// de los que representan el mensaje.<!-- -->
        /// </summary>
        /// <param name="cadenaIndex">Listado de enteros con el mensaje original</param> 
        /// <returns> La cadena de ceros y unos para convertir a bytes </returns>
        private string DecimalACadenaConCerosExtra(int elementoIndex, int minBytesDeMayor)
        {
            return "";
        }
    }
}
