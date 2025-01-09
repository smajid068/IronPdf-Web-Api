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
        public IActionResult GeneratePdfFromHtmlString([FromBody] PdfRequestFromHtmlString request)
        {
            try
            {
                var urlDownloadPathDirectory = "https://localhost:5001/GeneratedPdfs/";
                
                //Use IronPDF to render Html to Pdf
                IronPdf.License.LicenseKey = "IRONSUITE.DEVTEAM.IX5994-1B96F73E10-AVL72YRY2OX-LTOAP6UCU5AX-XPVWHC2RK6V6-4SUIBDY6NH57-GGVE6ZAQDR3X-MCYFIIXNHXKE-RGY7Q6-L62LNUAI4R22EA-ENTERPRISE-42GQE6.RENEW.SUPPORT.29.MAY.2034";
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
        public IActionResult GeneratePdfFromHtmlUrl([FromBody] PdfRequestFromHtmlUrl request)
        {
            try
            {
                var urlDownloadPathDirectory = "https://localhost:5001/GeneratedPdfs/";

                //Use IronPDF to render a Webpage URL to Pdf
                IronPdf.License.LicenseKey = "IRONSUITE.DEVTEAM.IX5994-1B96F73E10-AVL72YRY2OX-LTOAP6UCU5AX-XPVWHC2RK6V6-4SUIBDY6NH57-GGVE6ZAQDR3X-MCYFIIXNHXKE-RGY7Q6-L62LNUAI4R22EA-ENTERPRISE-42GQE6.RENEW.SUPPORT.29.MAY.2034";
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
    }
}