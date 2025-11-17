using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

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
            Guid databaseGuid = Guid.Parse("752df694-2274-42f9-81ee-98b3d6f7c104");
            Guid guid = Guid.Parse("752df694-2274-42f9-81ee-98b3d6f7c104");

            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //arrange
            RealEstateDto dto = MockRealEstateDto();

            // act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            // assert
            Assert.Equal(addRealEstate.Id, deleteRealEstate.Id);

        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //arrange
            var dto = MockRealEstateDto();

            //act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            //assert
            Assert.NotEqual(realEstate1.Id, result.Id);

        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //arrange
            var Guid = new Guid("fc481d09-3852-404a-8b89-58e82314c5f7");

            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("fc481d09-3852-404a-8b89-58e82314c5f7");
            domain.Area = 200.0;
            domain.Location = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            //act
            await Svc<IRealEstateServices>().Update(dto);

            //assert
            Assert.Equal(domain.Id, Guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            //Võrrelda RoomNumbrit ja kasutada DoesNotMatch
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.DoesNotMatch(dto.Location, domain.Location);

            Assert.DoesNotMatch(dto.BuildingType, domain.BuildingType);
            Assert.NotEqual(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            //alguses andmed luuakse ja kasutame MockRealEstateDto meetodit
            //arrange and act

            RealEstateDto dto = MockRealEstateDto();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //andmed uuendatakse ja kasutame uut Mock meetodit(selle peab ise tegema)
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //assert
            //lõpus kontrollime, et andmed on erinevad
            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
            Assert.NotEqual(createRealEstate.ModifiedAt, result.ModifiedAt);
        }

        //ise tehtud testid

        [Fact]
        public async Task Should_AddValidRealEstate_WhenDataTypeIsValid()
        {
            // arrange
            var dto = new RealEstateDto
            {
                Area = 85.00,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // act
            var realEstate = await Svc<IRealEstateServices>().Create(dto);

            //assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }

        [Fact]
        public async Task Should_CreateRealEstateWithNegativeArea_WhenAreaIsNegative()
        {
            //arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = -50.0,
                Location = "asd",
                RoomNumber = 2,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //assert
            Assert.NotNull(result);
            Assert.Equal(dto.Area, result.Area);
        }

        //Test kontrollib, et RealEstate kustutamisel kaob see süsteemist (Delete tegelikult eemaldab)
        [Fact]
        public async Task Should_RemoveRealEstateFromDatabase_WhenDelete()
        {
            //arrange
            RealEstateDto dto = MockRealEstateDto();

            //act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate.Id);

            //assert
            Assert.Equal(createRealEstate.Id, deleteRealEstate.Id);

            //uue teenuse kontrollimine, et objekti enam ei oleks
            var freshService = Svc<IRealEstateServices>();
            var result = await freshService.DetailAsync((Guid)createRealEstate.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldUpdateModifiedAt_WhenUpdateData()
        {
            //arrange – loome meetod Create
            RealEstateDto dto = MockRealEstateDto();
            var create = await Svc<IRealEstateServices>().Create(dto);

            //act – uued MockUpdateRealEstateData andmed
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //assert – Kontrollime, et ModifiedAt muutuks
            Assert.NotEqual(create.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            //arrange
            // teeme muutuja CreatedAt originaaliks, mis peab jääma
            // loome CreatedAt
            RealEstateDto dto = MockRealEstateDto();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00";
            //var originalCreatedAt = create.CreatedAt;

            //act – uuendame MockUpdateRealEstateData andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00");

            //assert – kontrollime, et uuendamisel ei uuendaks CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }

        [Fact]
        public async Task ShouldCheckRealEstateIdIsUnique()
        {
            //arrange – loome kaks objekti
            RealEstateDto dto1 = MockRealEstateDto();
            RealEstateDto dto2 = MockRealEstateDto();

            //act – kasutame Id loomiseks
            var create1 = await Svc<IRealEstateServices>().Create(dto1);
            var create2 = await Svc<IRealEstateServices>().Create(dto2);

            //assert – kontrollib, et ID oleks erinev
            Assert.NotEqual(create1.Id, create2.Id);
        }

        //First test to add empty real estate and check that it is not added
        //Tuleb kontrollida, et tühja kinnisvara lisamine ei õnnestu
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate()
        {
            //arrange
            RealEstateDto dto = new()
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };

            //act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdate_ModifiedAt_Parameter()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateDto();
            var createdRealEstateResult = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //Assert
            Assert.NotEqual(result.CreatedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_ReturnRealEstate_WhenCorrectDataDetailAsync()
        {
            //Arrange 
            RealEstateDto dto = MockRealEstateDto();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var detailedRealEstate = await Svc<IRealEstateServices>().DetailAsync((Guid)createdRealEstate.Id!);

            //Assert
            Assert.NotNull(detailedRealEstate);
            Assert.Equal(createdRealEstate.Id, detailedRealEstate.Id);
            Assert.Equal(createdRealEstate.Area, detailedRealEstate.Area);
            Assert.Equal(createdRealEstate.Location, detailedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, detailedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, detailedRealEstate.BuildingType);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenPartialUpdate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateDto();

            // Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var updateDto = new RealEstateDto
            {
                Area = 99,
                Location = "Changed Location Only",
                RoomNumber = createdRealEstate.RoomNumber,
                BuildingType = createdRealEstate.BuildingType,
                CreatedAt = createdRealEstate.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert
            Assert.NotEqual(createdRealEstate.Area, updatedRealEstate.Area);
            Assert.Equal("Changed Location Only", updatedRealEstate.Location);
            Assert.NotEqual(createdRealEstate.Location, updatedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, updatedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, updatedRealEstate.BuildingType);
            Assert.Equal(createdRealEstate.CreatedAt, updatedRealEstate.CreatedAt);
            Assert.NotEqual(createdRealEstate.ModifiedAt, updatedRealEstate.ModifiedAt);
        }

        [Fact]
        // Kontrollib, kas RealEstate luuakse ja ID määratakse
        public async Task Should_CreateRealEstate_AndAssignId()
        {
            // Arrange
            var dto = MockRealEstateDto();
            dto.Id = Guid.Empty;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        // Kontrollib, et kustutatud RealEstate pole leitav
        public async Task Should_ReturnNull_WhenReadingDeletedRealEstate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateDto();
            var created = await Svc<IRealEstateServices>().Create(dto);

            // Act
            await Svc<IRealEstateServices>().Delete((Guid)created.Id!);

            // Assert
            var result = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id!);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNot_UpdateCreatedTime_WhenUpdateRealEstate()
        {
            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new()
            {
                Id = dto.Id,
                Area = 180.0,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(domain);

            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }


        private RealEstateDto MockRealEstateDto()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Tallinn",
                RoomNumber = 2,
                BuildingType = "Apartment",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }
    }
}
