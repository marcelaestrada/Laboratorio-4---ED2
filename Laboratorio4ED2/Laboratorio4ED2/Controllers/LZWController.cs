using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompresorLZW.Estructuras;

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

            try
            {
                string cadena = System.IO.File.ReadAllText(nombre);
                compresor.Comprimir(cadena);
                string lineaArchivo = compresor.Archivo();
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
