using ReactCRUD.Core.Domain;
using System.Threading.Tasks;

namespace ReactCRUD.Core.ServiceInterface
{
    public interface ISchoolInterface
    {
        Task<School> SchoolDetail(Guid id);
        Task<School> CreateSchool(School school);
    }
}