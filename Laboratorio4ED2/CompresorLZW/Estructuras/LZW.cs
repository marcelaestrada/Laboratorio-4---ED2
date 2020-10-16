using System;
using System.Collections.Generic;
using System.Text;

namespace CompresorLZW.Estructuras
{
    class LZW
    {

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

    }
}
