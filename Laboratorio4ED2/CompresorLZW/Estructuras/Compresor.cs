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
        public List<int> CadenaAIndex(Dictionary<string, int> original, string mensaje, int ultimoIndex)
        {
            Dictionary<string, int> diccionario = original;
            List<int> mensajeEnNumeros = new List<int>();

            int i = 0;
            while (i < mensaje.Length)
            {
                string auxiliar = mensaje[i].ToString();
                //string auxiliarKey;
                int j = i + 1;
                if (original.ContainsKey(auxiliar))
                {
                    while (original.ContainsKey(auxiliar) && j < mensaje.Length)
                    {
                        auxiliar += mensaje[j];
                        j++;
                    }
                    if (!original.ContainsKey(auxiliar))
                    {

                        //auxiliar se va al diccionario al ser la nueva cadena que no está en el 
                        diccionario.Add(auxiliar, ultimoIndex++);
                        //diccionario[auxiliar - 1] que debe estar en el diccionario se agrega a la  lista de enteros
                        int largoSubString = auxiliar.Length - 1;
                        mensajeEnNumeros.Add(diccionario[auxiliar.Substring(0, largoSubString)]);

                        i = j - 1;
                    }
                    else
                    {
                        mensajeEnNumeros.Add(diccionario[auxiliar]);
                        break;
                    }


                }
                else
                {
                    original.Add(auxiliar, ultimoIndex + 1);
                }

            }

            return mensajeEnNumeros;
        }

        /// <summary>
        /// Metodo que recive un elemento entero de la lista de enteros con elmensaje
        /// y lo convierte a un string de bits rellenado con los ceros del mayor numero 
        /// de los que representan el mensaje.
        /// </summary>
        /// <param name="elementoIndex">valor entero representado por una llave del diccionario</param>
        /// <param name="minBytesDeMayor">Minimo de bites que necesita la llave más grande para ser escrita</param>
        /// <returns> Cadena con ceros extra cumpliendo con el minimo de bytes del valor mayor en el mensaje </returns>
        private string DecimalToPadBinaryString(int elementoIndex, int minBitesDeMayor)
                                    => Convert.ToString(elementoIndex, 2).PadLeft(minBitesDeMayor, '0');

        /// <summary>
        /// Segun el entero mayor devuelve
        /// la cantidad de bites que ocupará cada index del 
        /// mensaje original.  
        /// </summary>
        /// <param name="mayor"></param>
        /// <returns>Bites totales por index en el mensaje</returns>
        public int MinBitesValorMayor(int mayor) => Convert.ToString(mayor, 2).Length;

        /// <summary>
        /// Cálculo de la cantidad de ceros que se agregarán al 
        /// ultimo byte de la cadena binaria. Este dato debe ir en la metadata de la compresión 
        /// </summary>
        /// <param name="cadenaBinaria">cadena obtenida del método de ceros y unos con el contenido total del mensaje</param>
        /// <returns>Cantidad de ceros que se agregan al último byte</returns>
        private int CantidadCerosExtra(string cadenaBinaria) => 8 - (cadenaBinaria.Length % 8);

        /// <summary>
        /// Convertir un listado de enteros a una cadena de 
        /// ceros y unos que represente los bytes del mensaje comprimido.
        /// </summary>
        /// <param name="cadenaIndex">Listado de enteros con el mensaje original</param> 
        /// <returns> La cadena de ceros y unos para convertir a bytes </returns>
        public string CadenaDeByteComprimidos(List<int> cadenaDeIndex, int minBitesDeMayor)
        {
            string resultado = "";
            foreach (var item in cadenaDeIndex)
                resultado += DecimalToPadBinaryString(item, minBitesDeMayor);

            return resultado;
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
        /// Cadena de ceros extra a concatenar a la cadena binaria del mensaje completo.
        /// </summary>
        /// <param name="cantidadCerosExtra">Cantidad entera de ceros que se le 
        /// agregan al último byte del mensaje</param>
        /// <param name="cadenaVacia">Esta cadena SIEMPRE debe ser vacía</param>
        /// <returns></returns>
        private string GenerarCerosExtra(int cantidadCerosExtra, string cadenaVacia = "")
                                            => cadenaVacia.PadRight(cantidadCerosExtra, '0');
    }
}
