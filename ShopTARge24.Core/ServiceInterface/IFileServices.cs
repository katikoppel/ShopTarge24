using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
    }
}
