using Microsoft.AspNetCore.Mvc;
using PDFService.API.Models;
using PDFService.API.Models.HtmlToPdf;
using PDFService.API.Services.Interface;
using Swashbuckle.Swagger.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PDFService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("/api/pdf/combine")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(OperationId = "1")]
        public async Task<IActionResult> CompaginatePDF([FromForm, Required] List<IFormFile> files)
        {
            try 
            {
                var compaginado = await _pdfService.CompaginatePDF(files);

                return Ok(new Response(200, false, File(compaginado, "application/pdf", "Compaginado.pdf")));
            }
            catch (Exception ex)
            {
                var response = new Response(500, true, ex?.Message ?? "Hubo un error no controlado.");

                return StatusCode(response.status, response);
            }
        }

        [HttpPost("/api/pdf/html")]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "1")]
        public async Task<IActionResult> HtmlToPDF([FromBody] HtmlToPDFRequest request)
        {
            try
            {
                var pdfBytes = await _pdfService.HtmlToPDF(request.Html);

                if (request.TipoRespuesta.ToLower() == "descarga")
                    return File(pdfBytes, "application/pdf", "HtmlToPdf.pdf");

                else if (request.TipoRespuesta.ToLower() == "bits")
                    return Ok(new Response(200, false, new { File = pdfBytes }));

                else
                    return BadRequest(new Response(500, true, "El tipo de respuesta debe ser 'descarga' o 'bits'."));
                
            }
            catch (Exception ex)
            {
                var response = new Response(500, true, ex?.Message ?? "Hubo un error no controlado.");
                return StatusCode(response.status, response);
            }
        }

    }
}
