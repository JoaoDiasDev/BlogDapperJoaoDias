using System.Net.Http.Headers;

namespace BlogDapperJoaoDias.Helpers
{
    public class UploadHelpers
    {
        private IWebHostEnvironment _environment;
        public UploadHelpers(IWebHostEnvironment hosting)
        {
            _environment = hosting;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var result = "";
            if (file.Length > 0)
            {
                try
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                    var fileExtension = Path.GetExtension(fileName);
                    var folderPath = Path.Combine(_environment.WebRootPath + "/upload/");
                    var newFileName = myUniqueFileName + fileExtension;
                    fileName = Path.Combine(folderPath + newFileName);
                    await using (var fileStream = File.Create(fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.DisposeAsync();
                    }
                    result = newFileName;
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }

            return result;
        }
    }
}
