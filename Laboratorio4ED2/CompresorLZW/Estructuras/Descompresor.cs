using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace CompresorLZW.Estructuras
{
    public class Descompresor
    {
        //Método para reconstruir diccionario 
        /*public void reconstruirDiccionario(Dictionary<string, int> original, List<int> numeros)
        {
            Dictionary<int, string> originalI = new Dictionary<int, string>();
            Dictionary<int, string> agregados = new Dictionary<int, string>();
            string anterior = "";
            string actual = "";
            string union = "";
            int contador = original.Count;

            foreach (var item in original)
            {
                originalI.Add(item.Value, item.Key);
            }

            List<KeyValuePair<int, string>> listaDiccionario = originalI.ToList();

            foreach (var item in numeros)
            {
                foreach (var elem in listaDiccionario)
                {
                    if (elem.Key == item)
                    {
                        actual = elem.Value;
                        break;
                    }
                }

                //numero ya esta
                if (original.ContainsKey(anterior + actual))
                {
                    anterior += actual;
                    actual = "";
                }
                else
                {
                    contador++;
                    union = anterior += actual;
                    agregados.Add(contador, union);
                    KeyValuePair<int, string> valorNuevo = new KeyValuePair<int, string>(contador, union);
                    listaDiccionario.Add(valorNuevo);
                    anterior = actual;
                    actual = "";
                }
            }

            foreach (var item in agregados)
            {
                if (!original.ContainsKey(item.Value))
                {

                    original.Add(item.Value, item.Key);
                }
            }
            int flasg = 0;
        }*/

        public Dictionary<int, string> reconstruirDiccionario(Dictionary<string, int> original, List<int> numeros)
        {
            Dictionary<int, string> originalI = new Dictionary<int, string>();
            Dictionary<int, string> agregados = new Dictionary<int, string>();
            string anterior = "";
            string actual = "";
            string union = "";
            int contador = original.Count;

            foreach (var item in original)
            {
                originalI.Add(item.Value, item.Key);
            }

            List<KeyValuePair<int, string>> listaDiccionario = originalI.ToList();

            foreach (var item in numeros)
            {
                foreach (var elem in listaDiccionario)
                {
                    if (elem.Key == item)
                    {
                        actual = elem.Value;
                        break;
                    }
                }
                //|||||||VVVVVVVVV||||||||
                //numero ya esta
                if (original.ContainsKey(anterior + actual) || agregados.ContainsValue(anterior+actual))
                {
                    anterior += actual;
                    actual = "";
                }
                else
                {
                    contador++;
                    union = anterior += actual;
                    agregados.Add(contador, union);
                    KeyValuePair<int, string> valorNuevo = new KeyValuePair<int, string>(contador, union);
                    listaDiccionario.Add(valorNuevo);
                    anterior = actual;
                    actual = "";
                }
            }
            var newDic = new Dictionary<int, string>();
            foreach (var item in listaDiccionario)
            {
                newDic.Add(item.Key, item.Value);
            }

            return newDic;
        }


    }
}
