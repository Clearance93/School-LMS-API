using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace OrganizationServices
{
    public class PDFExtractorService
    {
        public static string ExtractTextFromPdf(string base64String)
        {
            byte[] fileBytes = Convert.FromBase64String(base64String);

            using var stream = new MemoryStream(fileBytes);

            var sb = new StringBuilder();

            using var reader = new PdfReader(stream);

            using var document = new PdfDocument(reader);

            for (int i = 1; i <= document.GetNumberOfPages(); i++)
            {
                var page = document.GetPage(i);

                string text = PdfTextExtractor.GetTextFromPage(page);
                sb.AppendLine(text);
            }

            return sb.ToString();
        }
    }
}
