using System.Xml;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices
            (
                ShopTARge24Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergartens = new Kindergarten();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.KindergartenUploadFilesToDatabase(dto, kindergartens);
            }

            await _context.Kindergartens.AddAsync(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten kindergartens = new Kindergarten();

            kindergartens.Id = dto.Id;
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = dto.CreatedAt;
            kindergartens.UpdatedAt = DateTime.Now;

            _context.Kindergartens.Update(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}