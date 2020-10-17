using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Estructuras
{
    class LZW
    {
        /// <summary>
        /// Convertir un listado den enteros (los index del mensaje) 
        /// a una cadena de ceros y unos que repreenten el mensaje comprimido.
        /// </summary>
        /// <param name="cadenaDeIndex"> Listado con los index que remplazan los caracteres y fraces del mensaje </param>
        /// <param name="minBitesDeMayor">Minimo de bites que necesita el número mayor para cer escrito en binario</param>
        /// <returns></returns>
        private string CadenaDeByteComprimidos(List<int> cadenaDeIndex, int minBitesDeMayor)
        {
            string resultado = "";
            foreach (var item in cadenaDeIndex)
                resultado += DecimalToPadBinaryString(item, minBitesDeMayor);

            return resultado;
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
        private int MinBitesValorMayor(int mayor) => Convert.ToString(mayor, 2).Length;



        /// <summary>
        /// Cálculo de la cantidad de ceros que se agregarán al 
        /// ultimo byte de la cadena binaria. Este dato debe ir en la metadata de la compresión 
        /// </summary>
        /// <param name="cadenaBinaria">cadena obtenida del método de ceros y unos con el contenido total del mensaje</param>
        /// <returns>Cantidad de ceros que se agregan al último byte</returns>
        private int CantidadCerosExtra(string cadenaBinaria) => 8 - (cadenaBinaria.Length % 8);

        /// <summary>
        /// Cadena de ceros extra a concatenar a la cadena binaria del mensaje completo.
        /// </summary>
        /// <param name="cantidadCerosExtra">Cantidad entera de ceros que se le 
        /// agregan al último byte del mensaje</param>
        /// <param name="cadenaVacia">Esta cadena SIEMPRE debe ser vacía</param>
        /// <returns></returns>
        private string GenerarCerosExtra(int cantidadCerosExtra, string cadenaVacia = "")
                                            => cadenaVacia.PadRight(cantidadCerosExtra, '0');


        /// <summary>
        /// Este método utiliza el diccionario original para cifrar la cadena 
        /// remplazando los caracteres y subcadenas por los index del diccionario 
        /// original mientras se van agregando más sub cadenas al diccionario.
        /// y remplazando sub cadenas por más index.
        /// </summary>
        /// <param name="original"> diccionario original</param>
        /// <param name="mensaje">Cadena del mensaje</param>
        /// <param name="ultimoIndex">valor de la ultima llave del diccioneario</param>
        /// <returns></returns>
        private List<int> CadenaAIndex(Dictionary<string, int> original, string mensaje, int ultimoIndex)
        {
            Dictionary<string, int> diccionario = original;
            List<int> mensajeEnNumeros = new List<int>();

            int i = 0;
            while (i < mensaje.Length)
            {
                string auxiliar = mensaje[i].ToString();
               //string auxiliarKey;
                int j = i+1;
                if (original.ContainsKey(auxiliar))
                {
                    while (original.ContainsKey(auxiliar))
                    {
                        auxiliar += mensaje[j];
                        j++;
                    }
                    //auxiliar se va al diccionario al ser la nueva cadena que no está en el 
                    diccionario.Add(auxiliar, ultimoIndex++);
                    //diccionario[auxiliar - 1] que debe estar en el diccionario se agrega a la  lista de enteros
                    int largoSubString = auxiliar.Length - 1;
                    mensajeEnNumeros.Add(diccionario[auxiliar.Substring(0,largoSubString)]);

                    i = j - 1;
                    //o j

                }
                else
                {
                    original.Add(auxiliar, ultimoIndex + 1);
                }

            }

            return mensajeEnNumeros;
        }




    }
}
