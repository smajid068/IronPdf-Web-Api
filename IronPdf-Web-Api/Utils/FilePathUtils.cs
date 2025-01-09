namespace IronPdf_Web_Api.Utils
{
    public class FilePathUtils
    {
        public static (string FileName, string FilePath) GetFilePath()
        {

            var basePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPdfs");

            if (!Directory.Exists(basePathDirectory))
            {
                Directory.CreateDirectory(basePathDirectory);
            }
            var fileName = $"{Guid.NewGuid()}.pdf";
            var filePath = $"{basePathDirectory}/{fileName}";

            return (fileName, filePath);
        }
    }
}
