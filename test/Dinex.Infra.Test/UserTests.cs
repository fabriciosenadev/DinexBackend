

namespace Dinex.InfraTests
{
    [ExcludeFromCodeCoverage]
    public class UserTests
    {
        const int Success = 1;

        private readonly Faker _faker;
        private readonly UserMock _mock;
        private readonly IUserRepository _userRepository;

        public UserTests()
        {
            _faker = new Faker("pt_BR");
            _mock = new UserMock(_faker);
            _userRepository = Substitute.For<IUserRepository>();
        }

        [Fact]
        public async Task Should_Fail_Insert_User()
        {
            var user = _mock.GetUserMock();

            var result = await _userRepository.AddAsync(user);

            Assert.NotEqual(Success, result);
        }

        [Fact]
        public async Task Should_Insert_User_Successfully()
        {
            var user = _mock.GetUserMock();
            user.Id = Guid.NewGuid();

            var result = await _userRepository.AddAsync(user);

            Assert.Equal(Success, result);
            Assert.NotEqual(Guid.Empty,user.Id);
        }
    }
}