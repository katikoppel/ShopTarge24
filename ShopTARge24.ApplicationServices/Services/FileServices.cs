using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARge24Context _context;

        public FileServices
            (
                ShopTARge24Context context
            )
        {
            _context = context;
        }

        public void KindergartenUploadFilesToDatabase(KindergartenDto dto, Kindergarten domain)
        {
            //toimub kontroll, kas on vähemalt üks fail või mitu
            if (dto.Files != null && dto.Files.Count > 0)
            {
                //tuleb kasutada foreachi, et mitu faili ülesse laadida
                foreach (var file in dto.Files)
                {
                    //foreachi sees tuleb kasutada using-t
                    using (var target = new MemoryStream())
                    {
                        KindergartenFileToDatabase files = new KindergartenFileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            KindergartenId = domain.Id
                        };

                        //andmed salvestada andmebaasi
                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.KindergartenFileToDatabases.Add(files);
                    }
                }
            }
        }
    }
}
