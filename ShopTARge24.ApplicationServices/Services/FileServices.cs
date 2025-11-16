using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge24Context _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ShopTARge24Context context
            )
        {
            _webHost = webHost;
            _context = context;
        }

        public async Task UploadFilesToDB(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        KindergartenFileToDatabase files = new KindergartenFileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            KindergartenId = domain.Id,
                        };
                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.KindergartenFileToDatabases.Add(files);
                    }
                }
            }
        }

        public void RemoveImagesFromDB(Guid kindergartenId)
        {
            var files = _context.KindergartenFileToDatabases
                .Where(f => f.KindergartenId == kindergartenId)
                .ToList();

            if (files.Any())
            {
                _context.KindergartenFileToDatabases.RemoveRange(files);
                _context.SaveChanges();
            }
        }

        public async Task RemoveImageFromDB(Guid imageId)
        {
            var image = await _context.KindergartenFileToDatabases
                .FirstOrDefaultAsync(f => f.Id == imageId);

            if (image != null)
            {
                _context.KindergartenFileToDatabases.Remove(image);
                await _context.SaveChangesAsync();
            }


        }

    }
}