using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CIIADHEL_CR.helpers
{
    public class FilesServices
    {
        public async Task<string> DownloadPdfFileAsync()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "MROC.pdf");

            if (File.Exists(filePath))
                return filePath;

            var httpClient = new HttpClient();
            var pdfBytes = await httpClient.GetByteArrayAsync("http://www.uvairlines.com/admin/resources/mroc.pdf");
            
            try
            {
                File.WriteAllBytes(filePath, pdfBytes);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Error en los archivos");
            }
        }





    }
}
