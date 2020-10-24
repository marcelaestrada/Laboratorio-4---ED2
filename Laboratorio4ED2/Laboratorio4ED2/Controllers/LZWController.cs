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
        LZW compresorLZW = new LZW();

        [HttpPost("compress/{name}")]
        public async Task<ActionResult> Compress([FromForm] IFormFile file, string name)
        {
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
                compresorLZW.Comprimir(texto.ToString(),file.FileName, name, nombre);
                documento.WriteLine(compresorLZW.Archivo(nombreOriginal));
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
            string nombre = file.FileName;
            string path = $"./{nombre}";
            FileStream fileRecuperado = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(fileRecuperado);

            try
            {
                string textoDescomprimido = compresorLZW.Descomprimir(rd.ReadToEnd());
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("compressions")]
        public async Task<string> Compressions()
        {
            return compresorLZW.JSONCompresiones();
        }
    }
}
