using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompresorLZW.Estructuras;
using System.Text;

namespace Laboratorio4ED2.Controllers
{
    [ApiController]
    [Route("api")]
    public class LZWController : Controller
    {
        [HttpPost("compress/{name}")]
        public async Task<ActionResult> Compress([FromForm] IFormFile file, string name)
        {
            LZW compresor = new LZW();
            string nombre = $"./{name}.lzw";
            FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter documento = new StreamWriter(filestream);
            string nombreOriginal = file.FileName;

            try
            {
                var texto = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        texto.AppendLine(await reader.ReadLineAsync());
                }
                compresor.Comprimir(texto.ToString());
                documento.WriteLine(compresor.Archivo(nombreOriginal));
                documento.Close();
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("decompress")]
        public async Task<ActionResult> Decompress([FromForm] IFormFile file)
        {
            LZW descompresor = new LZW();
            string nombre = file.FileName;
            string path = $"./{nombre}";
            FileStream fileRecuperado = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(fileRecuperado);

            try
            {
                string textoDescomprimido = descompresor.Descomprimir(rd.ReadToEnd());
                string nombreOriginal = descompresor.NombreOriginal();
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
