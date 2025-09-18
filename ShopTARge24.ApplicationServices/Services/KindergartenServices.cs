using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Data.Migrations;


namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge24Context _context;

        public KindergartenServices
            (
                ShopTARge24Context context
            )
        {
            _context = context;
        }

        public async Task<Kindergartens> Create(KindergartenDto dto)
        {
            Kindergartens kindergartens = new Kindergartens();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdatedAt = DateTime.Now;

            await _context.Kindergartens.AddAsync(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            Kindergartens kindergartens = new Kindergartens();

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

        public async Task<Kindergartens> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergartens> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}