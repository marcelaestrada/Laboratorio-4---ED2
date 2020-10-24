using CompresorLZW.Interfaz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using Laboratorio4ED2;

namespace CompresorLZW.Estructuras
{
    public class LZW : InterfazLZW
    {
        Compresor compresor = new Compresor();
        Dictionary<string, int> original = new Dictionary<string, int>();
        List<string> listaJSON = new List<string>();
        string resultado = "";
        string nombreOriginal;
        string nombreComprimido;
        string rutaf;
        int minBitesMayor = 0;
        int ceros = 0;
        int bytesOriginal = 0;
        int bytesComprimido = 0;

        public string Comprimir(string cadena, string nombre, string nuevoNombre, string ruta)
        {
            bytesOriginal = Encoding.ASCII.GetBytes(cadena).Length;
            nombreOriginal = nombre;
            nombreComprimido = nuevoNombre;
            rutaf = ruta;
            string comprimido = "";
            string cadenaCeros = "";
            List<int> mensajeNumeros = new List<int>();
            List<int> paraASCII = new List<int>();
            List<string> cadenaBinarios = new List<string>();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            diccionario = compresor.DiccionarioOriginal(cadena);

            foreach (var item in diccionario)
            {
                original.Add(item.Key, item.Value);
            }

            mensajeNumeros = compresor.CadenaAIndex(diccionario, cadena, diccionario.Count);
            minBitesMayor = compresor.MinBitesValorMayor(mensajeNumeros.Max());
            comprimido = compresor.CadenaDeByteComprimidos(mensajeNumeros, minBitesMayor);
            ceros = compresor.CantidadCerosExtra(comprimido);
            cadenaCeros = compresor.GenerarCerosExtra(compresor.CantidadCerosExtra(comprimido));
            comprimido += cadenaCeros;
            cadenaBinarios = compresor.codigosSplit(8, comprimido);


            foreach (var item in cadenaBinarios)
            {
                int numero = compresor.CadenaBinAInt(item);
                paraASCII.Add(numero);
            }


            foreach (var item in paraASCII)
            {
                resultado += Convert.ToChar(item);
            }

            bytesComprimido = Encoding.ASCII.GetBytes(resultado).Length;
            llenarJSON();
            return resultado;
        }

        public string Archivo()
        {
            string lineaArchivo = "";
            int caracteres = original.Count;

            lineaArchivo += Convert.ToChar(ceros);
            lineaArchivo += Convert.ToChar(caracteres);
            lineaArchivo += Convert.ToChar(minBitesMayor);

            foreach (var item in original)
            {
                lineaArchivo += Convert.ToChar(item.Key);
            }

            lineaArchivo += resultado;

            return lineaArchivo;
        }

        public string Descomprimir(string comprimido)
        {
            List<int> bytesLinea = new List<int>();
            foreach (var item in comprimido)
            {
                bytesLinea.Add(Convert.ToInt32(item));
            }

            Descompresor descompresor = new Descompresor();
            List<int> recuperado = new List<int>();
            Dictionary<string, int> diccionarioRecuperado = new Dictionary<string, int>();
            List<string> bitesLinea = new List<string>();
            List<string> cadenaBinarios = new List<string>();
            List<int> numeros = new List<int>();
            string cadenaOriginal = "";
            string binario = "";
            string cadena = "";
            int ceros = bytesLinea[0];
            int caracteres = bytesLinea[1];
            int bites = bytesLinea[2];

            for (int i = 3; i <= caracteres + 2; i++)
            {
                cadenaOriginal += Convert.ToChar(bytesLinea[i]);
            }

            diccionarioRecuperado = compresor.DiccionarioOriginal(cadenaOriginal);

            for (int i = caracteres + 3; i < bytesLinea.Count; i++)
            {
                binario += Convert.ToString(bytesLinea[i], 2).PadLeft(8, '0');
            }

            foreach (var item in binario)
            {
                bitesLinea.Add(Convert.ToString(item));
            }

            for (int i = 0; i < binario.Length - ceros; i++)
            {
                cadena += binario[i];
            }

            cadenaBinarios = compresor.codigosSplit(bites, cadena);

            foreach (var item in cadenaBinarios)
            {
                numeros.Add(Convert.ToInt32(item, 2));
            }

            return descompresor.reconstruirDic(diccionarioRecuperado, numeros);

        }

        public string GetAllCompress()
        {
            string ruta = $"./compresiones.txt";
            FileStream filestream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(filestream);
            var listaRegistros = new List<RegistroCompress>();

            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();
                var values = line.Split("|");
                var registro = new RegistroCompress
                {
                    NombreOriginal = values[0],
                    NombreComprimido = values[1],
                    RutaF = values[2],
                    RazonCompresion = Convert.ToDecimal(values[3]),
                    FactorCompresion = Convert.ToDecimal(values[4]),
                    PorcentajeReduccion = Convert.ToDecimal(values[5])
                };
                listaRegistros.Add(registro);
            }
            string jsonRegistros = JsonConvert.SerializeObject(listaRegistros);
            return jsonRegistros;

        }

        public void llenarJSON()
        {
            string nombre = $"./compresiones.txt";
            try
            {
                FileStream filestream = new FileStream(nombre, FileMode.Append, FileAccess.Write);
                StreamWriter documento = new StreamWriter(filestream);
                var registro = new RegistroCompress
                {
                    NombreOriginal = nombreOriginal,
                    NombreComprimido = nombreComprimido,
                    RutaF = rutaf,
                    RazonCompresion = Math.Round(Decimal.Divide(bytesComprimido, bytesOriginal), 2),
                    FactorCompresion = Math.Round(Decimal.Divide(bytesOriginal, bytesComprimido), 2),
                    PorcentajeReduccion = Math.Round(Decimal.Divide(bytesComprimido, bytesOriginal) * 100, 2)
                };

                string escribir = registro.ToString();
                documento.WriteLine(escribir);
                documento.Close();

                filestream.Close();

            }
            catch (Exception)
            {

                throw new Exception();
            }
           

        }

        public string NombreOriginal(string nombreComprimido)
        {
            string retorno = "default.txt";
            string ruta = $"./compresiones.txt";
            FileStream filestream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(filestream);

            while (!fileReader.EndOfStream)
            {
                string linea = fileReader.ReadLine();
                if (linea.Contains(nombreComprimido))
                {
                    var array = linea.Split("|");
                    retorno = array[0];
                }
            }

            return retorno;
        }
    }
}
