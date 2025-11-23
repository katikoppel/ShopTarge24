using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace KindergartenTest
{
    public class KindergartenTest : ShopTARge24.KindergartenTest.TestBase
    {
        [Fact]
        public async Task Should_AddKindergarten_WhenDataIsValid()
        {
            //Arrange
            KindergartenDto dto = MockKindergartenDto();

            //Act
            var result = await Svc<IKindergartenServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateData()
        {
            //Arrange and act
            KindergartenDto dto = MockKindergartenDto();
            var kindergarten = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto updatedDto = MockUpdateKindergartenDto();
            var result = await Svc<IKindergartenServices>().Update(updatedDto);

            //Assert
            Assert.DoesNotMatch(result.KindergartenName, kindergarten.KindergartenName);
            Assert.NotEqual(kindergarten.ChildrenCount, result.ChildrenCount);
        }

        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("37c9882a-34d4-479c-8027-1d6a9e971770");

            //Act
            await Svc<IKindergartenServices>().DetailAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);

        }

        [Fact]
        public async Task Should_AddKindergarten_WhenDataTypeIsValid()
        {
            //Arrange
            var dto = new KindergartenDto
            {
                GroupName = "KidsGroup",
                ChildrenCount = 40,
                KindergartenName = "Test Kindergarten",
                TeacherName = "Alice Apple",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            //Act
            var kindergarten = await Svc<IKindergartenServices>().Create(dto);

            //Assert
            Assert.IsType<int>(kindergarten.ChildrenCount);
            Assert.IsType<string>(kindergarten.GroupName);
            Assert.IsType<string>(kindergarten.TeacherName);
            Assert.IsType<DateTime>(kindergarten.CreatedAt);
        }

        [Fact]
        public async Task Should_CheckKindergarten_IdIsUnique()
        {
            //Arrange
            KindergartenDto dto1 = MockKindergartenDto();
            KindergartenDto dto2 = MockKindergartenDto();

            //Act
            var create1 = await Svc<IKindergartenServices>().Create(dto1);
            var create2 = await Svc<IKindergartenServices>().Create(dto2);

            //Assert
            Assert.NotEqual(create1.Id, create2.Id);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenKindergartenDeleted()
        {
            //Arrange
            KindergartenDto dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            //Act
            await Svc<IKindergartenServices>().Delete((Guid)created.Id!);

            //Assert
            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)created.Id!);
            Assert.Null(result);
        }


        private KindergartenDto MockKindergartenDto()
        {
            return new KindergartenDto
            {
                GroupName = "GroupOne",
                ChildrenCount = 15,
                KindergartenName = "Kindergarten ASD",
                TeacherName = "Mary Smith",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private KindergartenDto MockUpdateKindergartenDto()
        {
            return new KindergartenDto
            {
                GroupName = "GroupTwo",
                ChildrenCount = 20,
                KindergartenName = "Kindergarten 123",
                TeacherName = "John Smith",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
