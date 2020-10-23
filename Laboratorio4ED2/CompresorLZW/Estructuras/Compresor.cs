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
            /*System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();*/
            //byte[] bytesLinea = codificador.GetBytes(texto);
            List<byte> bytesLinea = new List<byte>();
            for(int i = 0; i < texto.Length; i++)
            {
                bytesLinea.Add(Convert.ToByte(texto[i]));
            }

            string cadena;
            int contador = 1;
            int evaluar = 0;
            string[] bytes = new string[bytesLinea.Count];

            for (int i = 0; i < bytesLinea.Count; i++)
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

            string anterior = "";
            string siguiente;
            

            int i  = 0;
            while (i < mensaje.Length)
            {
                siguiente = mensaje[i].ToString();
                if (diccionario.ContainsKey(anterior + siguiente))
                {
                    anterior += siguiente;
                   
                }
                else
                {
                    //agregar la cadena más larga que existe + 1 caracter
                    diccionario.Add(anterior + siguiente, ++ultimoIndex);
                    //Agregar cadena más larga que existe al listado de números
                    mensajeEnNumeros.Add(diccionario[anterior]);
                    //moverse un caracter
                    anterior = siguiente;
                    
                }
                i++;
            }
            mensajeEnNumeros.Add(diccionario[anterior]);


            //Prueba de descompresion:
            string numerosString = "";
           
            var nuevoDic = new Dictionary<int, string>();
            foreach (var item in diccionario)
            {
                nuevoDic.Add(item.Value, item.Key);
            }

            string descompresionPrueba = "";

            foreach (var item in mensajeEnNumeros)
            {
                descompresionPrueba += nuevoDic[item];
                numerosString += item + ",";
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
        public int CantidadCerosExtra(string cadenaBinaria) => 8 - (cadenaBinaria.Length % 8);

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
        public string GenerarCerosExtra(int cantidadCerosExtra, string cadenaVacia = "")
                                            => cadenaVacia.PadRight(cantidadCerosExtra, '0');

        /// <summary>
        /// Divide el string en una lista de binarios
        /// </summary>
        /// <returns>Lista de binarios</returns>
        public List<string> codigosSplit(int splitSize, string codigoBinario)
        {
            int stringLength = codigoBinario.Length;
            List<string> codigos = new List<string>();
            for (int i = 0; i < stringLength; i += splitSize)
            {
                if ((i + splitSize) <= stringLength)
                {
                    codigos.Add(codigoBinario.Substring(i, splitSize));
                }
                else
                {
                    codigos.Add(codigoBinario.Substring(i));
                }
            }
            return codigos;
        }

        /// <summary>
        /// Este metodo recibe una cadena binaria y la convierte a un entero para poder convertir a ASCII
        /// </summary>
        /// <returns>Entero equivalente al binario recibido</returns>
        public int CadenaBinAInt(string cadenaBinaria)
        {
            int resultado = 0;

            int[] baseDecimal = { 128, 64, 32, 16, 8, 4, 2, 1 };

            for (int i = 0; i < 8; i++)
                if (cadenaBinaria[i] == '1') resultado += baseDecimal[i];

            return resultado;
        }

    }
}
