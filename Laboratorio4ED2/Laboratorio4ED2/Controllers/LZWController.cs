using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompresorLZW.Estructuras;
using System.Text;
using System.Security.Cryptography.Xml;

namespace Laboratorio4ED2.Controllers
{
    [ApiController]
    [Route("api")]
    public class LZWController : Controller
    {
        //LZW compresorLZW = new LZW();

        [HttpPost("compress/{name}")]
        public async Task<IActionResult> Compress([FromForm] IFormFile file, string name)
        {
            LZW compresorLZW = new LZW();
            string nombre = $"./{name}.lzw";
            try
            {
                FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter documento = new StreamWriter(filestream);
                string nombreOriginal = file.FileName;


                var texto = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        texto.AppendLine(await reader.ReadLineAsync());
                }
                compresorLZW.Comprimir(texto.ToString(), file.FileName, name+".lzw", nombre);
                documento.WriteLine(compresorLZW.Archivo());
                string fileType = "text/plain";
               
                var fileResult = File(filestream, fileType, nombre);
                //documento.Close();
                return fileResult;

                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("decompress")]
        public async Task<IActionResult> Decompress([FromForm] IFormFile file)
        {
            LZW compresorLZW = new LZW();
            try
            {
                string nombre = file.FileName;
                string path = $"./{nombre}";
                FileStream fileRecuperado = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader rd = new StreamReader(fileRecuperado);


                string textoDescomprimido = compresorLZW.Descomprimir(rd.ReadToEnd());
                string nombreOriginal = compresorLZW.NombreOriginal(nombre); 
                string fileType = "text/plain";
                var fileResult = File(fileRecuperado, fileType, nombreOriginal);
                return fileResult;

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("compressions")]
        public string Compressions()
        {
            LZW lzw = new LZW();
            return lzw.GetAllCompress();
        }
    }
}
