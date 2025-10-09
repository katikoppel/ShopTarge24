using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void KindergartenUploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
    }
}
