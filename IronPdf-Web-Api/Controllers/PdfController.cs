using System.IdentityModel.Tokens.Jwt;
using IronPdf;
using IronPdf_Web_Api.Models;
using IronPdf_Web_Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdfApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        [HttpPost("generatePdfFromHtmlString")]
        public IActionResult GeneratePdfFromHtmlString([FromBody] PdfFromHtmlStringRequest request)
        {
            try
            {
                var urlDownloadPathDirectory = "https://localhost:5001/GeneratedPdfs/";
                
                //Use IronPDF to render Html to Pdf
                var renderer = new ChromePdfRenderer();
                var pdf = renderer.RenderHtmlAsPdf(request.HtmlContent);
                
                var (uploadFileName, uploadFilePath) = FilePathUtils.GetFilePath();

                //Use IronPDF to Save the file in the specified path
                pdf.SaveAs(uploadFilePath);
                
                urlDownloadPathDirectory = $"{urlDownloadPathDirectory}{uploadFileName}";

                return Ok(new { filePath = urlDownloadPathDirectory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("generatePdfFromHtmlUrl")]
        public IActionResult GeneratePdfFromHtmlUrl([FromBody] PdfFromHtmlUrlRequest request)
        {
            try
            {
                var urlDownloadPathDirectory = "https://localhost:5001/GeneratedPdfs/";

                //Use IronPDF to render a Webpage URL to Pdf
                var renderer = new ChromePdfRenderer();
                var pdf = renderer.RenderUrlAsPdf(request.HtmlUrl);
                var (uploadFileName, uploadFilePath) = FilePathUtils.GetFilePath();

                //Use IronPDF to Save the file in the specified path
                pdf.SaveAs(uploadFilePath);

                urlDownloadPathDirectory = $"{urlDownloadPathDirectory}{uploadFileName}";

                return Ok(new { filePath = urlDownloadPathDirectory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("extractPdfAsImages")]
        public IActionResult ExtractPdfAsImages([FromBody] ImagesFromPdfRequest request)
        {
            try
            {
                var pdf         = PdfDocument.FromUrl(new Uri(request.PdfFilePath));
                var pdfAsImages = pdf.ToPngImages("GeneratedImages/test_*.png");
                // Normalize paths to use forward slashes
                pdfAsImages = pdfAsImages.Select(path => path.Replace("\\", "/")).ToArray();

                for (int i = 0; i < pdfAsImages.Length; i++)
                {
                    pdfAsImages[i] = $"https://localhost:5001/{pdfAsImages[i]}";
                }

                return Ok(new { imageFilePaths = pdfAsImages });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}