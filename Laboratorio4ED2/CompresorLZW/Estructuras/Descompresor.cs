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
        public string reconstruirDic(Dictionary<string, int> original, List<int> numeros)
        {
            Dictionary<int, string> newDicc = new Dictionary<int, string>();

            foreach (var item in original)
            {
                newDicc.Add(item.Value, item.Key);
            }
            int ultimaPos = newDicc.Count;


            int posAnterior = numeros[0];
            int posSiguiente = 0;
            string indice = newDicc[posAnterior];
            string cadenaAuxiliar = "";
            string mensaje = indice;

            int i = 1;
            while (i < numeros.Count)
            {
                posSiguiente = numeros[i];

                if (newDicc.ContainsKey(posSiguiente))
                    cadenaAuxiliar = newDicc[posSiguiente];
                else //Este caso sale cuando hay muchos caracteres iguales repetidos uno detras de otro....
                    cadenaAuxiliar = newDicc[posAnterior] + indice;

                mensaje += cadenaAuxiliar;
                indice = cadenaAuxiliar.Substring(0, 1);
                newDicc.Add(++ultimaPos, newDicc[posAnterior] + indice);
                posAnterior = posSiguiente;
                i++;
            }
            return mensaje;
        }
    }
}
