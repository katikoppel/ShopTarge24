using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARge24.Core.Dto.OpenWeatherDto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IOpenWeatherServices
    {
        Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto);
    }
}
