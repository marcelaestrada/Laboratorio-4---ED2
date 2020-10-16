using System;
using System.Collections.Generic;
using System.Text;

namespace LZW.Estructuras
{
    public class LZW
    {
        public void Comprimir(string texto)
        {
            Compresor compresor = new Compresor();
            compresor.DiccionarioOriginal(texto);
        }
    }
}
