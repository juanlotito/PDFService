namespace PDFService.API.Services.Interface
{
    public interface IPdfService
    {
        public Task<byte[]> CompaginatePDF(List<IFormFile> files);
        public Task<byte[]> HtmlToPDF(string html);
    }
}
