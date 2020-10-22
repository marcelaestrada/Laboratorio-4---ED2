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
        public void reconstruirDiccionario(Dictionary<string, int> original, List<int> numeros)
        {
            Dictionary<int, string> originalI = new Dictionary<int, string>();
            Dictionary<int, string> agregados = new Dictionary<int, string>();
            string anterior = "";
            string actual = "";
            string union = "";
            int contador = original.Count;

            foreach(var item in original)
            {
                originalI.Add(item.Value, item.Key);
            }

            List<KeyValuePair<int,string>> listaDiccionario = originalI.ToList();

            foreach (var item in numeros)
            {
                foreach(var elem in listaDiccionario)
                {
                    if (elem.Key == item)
                    {
                        actual = elem.Value;
                        break;
                    }
                }

                //numero ya esta
                if (original.ContainsKey(anterior+actual))
                {
                    anterior += actual;
                    actual = "";
                }
                else {
                    contador++;
                    union = anterior += actual;
                    agregados.Add(contador, union);
                    anterior = actual;
                    actual = "";
                }
            }

            foreach(var item in agregados)
            {
                original.Add(item.Value, item.Key);
            }
        }
    }
}
