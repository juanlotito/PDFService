using PDFService.API.Services.Interface;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PDFService.API.Services
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> CompaginatePDF(List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0) throw new Exception("No se detectaron archivos, compruebe la subida de los mismos");

                if (files.Count == 1) throw new Exception("Se está enviando un único PDF, se esperan mínimo dos");

                using (var outputDocument = new PdfDocument())
                {
                    foreach (var formFile in files)
                    {
                        if (!formFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)) throw new Exception("Uno o más archivos no son PDF");

                        if (!formFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase)) throw new Exception("Uno o más archivos no tienen un tipo MIME de PDF");

                        if (formFile.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await formFile.CopyToAsync(stream);
                                stream.Position = 0; // IMPORTANTE! restablecer la posición del stream
                                using (var inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import))
                                {
                                    foreach (var page in inputDocument.Pages)
                                    {
                                        outputDocument.AddPage((PdfPage)page);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Uno o mas archivos están vacios, compruebe el contenido de los mismos");
                        }
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        outputDocument.Save(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> HtmlToPDF(string html)
        {
            var config = new PdfGenerateConfig
            {
                MarginTop = 20,
                MarginBottom = 20,
                MarginLeft = 20,
                MarginRight = 20,
                PageSize = PdfSharpCore.PageSize.A4
            };

            using (var document = PdfGenerator.GeneratePdf(html, config))
            {
                using (var stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    return stream.ToArray();
                }
            }
        }
    }
}
