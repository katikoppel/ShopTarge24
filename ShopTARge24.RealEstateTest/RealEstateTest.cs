using System.Threading.Tasks;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using Moq;

namespace ShopTARge24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }

        //ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        //Should_GetByIdRealEstate_WhenReturnsEqual()
        //Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        //ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("752df694-2274-42f9-81ee-98b3d6f7c104");

            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);

        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            //arrange

            //act

            //assert
        }
    }
}
