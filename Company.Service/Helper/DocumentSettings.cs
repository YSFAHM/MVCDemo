using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1 Get Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //2 Get File Name + for Uniqueness we concat it with guid
            var fileName = Guid.NewGuid() + "-" + file.FileName;

            //3 Combine FolderPath + FilePath

            var filePath = Path.Combine(folderPath, fileName);

            // Save file
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;

        }

        public static bool DeleteFile(string fileName, string folderName)
        {
            try
            {
                //1 Get Folder Path
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

                //2 Combine FolderPath + FileName
                var filePath = Path.Combine(folderPath, fileName);

                //3 Check if file exists, then delete
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
