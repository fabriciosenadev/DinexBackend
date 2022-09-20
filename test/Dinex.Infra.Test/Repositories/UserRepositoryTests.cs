namespace Dinex.InfraTests.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepositoryTests
    {
        const int Success = 1;

        private readonly Faker _faker;
        private readonly UserMock _mock;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _faker = new Faker("pt_BR");
            _mock = new UserMock(_faker);
            _userRepository = Substitute.For<IUserRepository>();
        }

        [Fact]
        public async Task Should_Fail_Add_User()
        {
            var user = _mock.GetUserMock();

            var result = await _userRepository.AddAsync(user);

            Assert.NotEqual(Success, result);
        }

        [Fact]
        public async Task Should_Add_User_Successfully()
        {
            var user = _mock.GetUserMock();
            user.Id = Guid.NewGuid();

            var result = await _userRepository.AddAsync(user);

            await _userRepository.Received().AddAsync(user);

            var userSaved = await _userRepository.GetByIdAsync(user.Id);


            Assert.Equal(Success, result);
            Assert.NotEqual(Guid.Empty, user.Id);
        }
    }
}
