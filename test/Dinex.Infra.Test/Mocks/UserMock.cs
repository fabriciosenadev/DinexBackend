namespace Dinex.InfraTests
{
    [ExcludeFromCodeCoverage]
    public class UserMock
    {
        private readonly Faker _faker;

        internal UserMock(Faker faker)
        {
            _faker = faker;
        }

        internal User GetUserMock()
        {
            return new User { 
                FullName = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                IsActive = UserActivatioStatus.Inactive,
                Password = _faker.Random.String2(10),
                CreatedAt = DateTime.Now,
            };

        }
    }
}