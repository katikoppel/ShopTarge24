using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace SpaceshipTest
{
    public class SpaceshipTest : ShopTARge24.SpaceshipTest.TestBase
    {
        [Fact]
        public async Task Should_AddSpaceship_WhenDataIsValid()
        {
            //Arrange
            SpaceshipDto dto = MockSpaceshipDto();

            //Act
            var result = await Svc<ISpaceshipServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("752df694-2274-42f9-81ee-98b3d6f7c104");

            //act
            await Svc<ISpaceshipServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);

        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //arrange and act
            SpaceshipDto dto = MockSpaceshipDto();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            SpaceshipDto updatedDto = MockUpdateSpaceshipDto();
            var result = await Svc<ISpaceshipServices>().Update(updatedDto);

            //assert
            Assert.DoesNotMatch(result.Name, createSpaceship.Name);
            Assert.NotEqual(createSpaceship.EnginePower, result.EnginePower);
        }

        [Fact]
        public async Task Should_AddSpaceship_WhenDataTypeIsValid()
        {
            // arrange
            var dto = new SpaceshipDto
            {
                Name = "GalaxyShip",
                Classification = "Huge Ship",
                BuiltDate = DateTime.UtcNow.AddYears(10),
                Crew = 65,
                EnginePower = 100,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // act
            var spaceship = await Svc<ISpaceshipServices>().Create(dto);

            //assert
            Assert.IsType<int>(spaceship.Crew);
            Assert.IsType<int>(spaceship.EnginePower);
            Assert.IsType<string>(spaceship.Name);
            Assert.IsType<string>(spaceship.Classification);
            Assert.IsType<DateTime>(spaceship.BuiltDate);
        }

        [Fact]
        public async Task Should_CheckSpaceship_IdIsUnique()
        {
            //arrange
            SpaceshipDto dto1 = MockSpaceshipDto();
            SpaceshipDto dto2 = MockSpaceshipDto();

            //act
            var create1 = await Svc<ISpaceshipServices>().Create(dto1);
            var create2 = await Svc<ISpaceshipServices>().Create(dto2);

            //assert 
            Assert.NotEqual(create1.Id, create2.Id);
        }

        private SpaceshipDto MockSpaceshipDto()
        {
            return new SpaceshipDto
            {
                Name = "Spaceship123",
                Classification = "Big ship",
                BuiltDate = DateTime.UtcNow.AddYears(-1),
                Crew = 123,
                EnginePower = 546,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private SpaceshipDto MockUpdateSpaceshipDto()
        {
            return new SpaceshipDto
            {
                Name = "Galactica",
                Classification = "Spaceship",
                BuiltDate = DateTime.UtcNow.AddDays(56),
                Crew = 987,
                EnginePower = 10000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
    }
}
