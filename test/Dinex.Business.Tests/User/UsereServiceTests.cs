using AutoMapper;
using Bogus;
using Dinex.Business;
using Dinex.Core;
using Dinex.Infra;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinex.Business.UserTests
{
    [ExcludeFromCodeCoverage]
    public class UsereServiceTests
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ICryptographyService _cryptographyService;
        private readonly ICodeManagerService _codeManagerService;
        private readonly Faker _faker;

        public UsereServiceTests()
        {
            _faker = new Faker("pt_BR");

            _userRepository = Substitute.For<IUserRepository>();
            _notificationService = Substitute.For<INotificationService>();
            _cryptographyService = Substitute.For<ICryptographyService>();
            _codeManagerService = Substitute.For<ICodeManagerService>();

            var mapperConfig = new MapperConfiguration(opt => 
            {
                opt.AddProfile(new UserMapper());
            });

            _mapper = mapperConfig.CreateMapper();

            _userService = new UserService(_userRepository,
                _cryptographyService,
                _mapper,
                _notificationService,
                _codeManagerService);
        }

        private UserRequestDto GetUserRequestDtoMock()
        {
            var password = _faker.Random.String2(10);
            var request = new UserRequestDto
            {
                Email = _faker.Internet.Email(),
                FullName = _faker.Person.FullName.ToString(),
                IsActive = UserActivatioStatus.Inactive,
                Password = password,
                ConfirmPassword = password
            };

            return request;
        }
        private User GetUserMock(UserRequestDto request)
        {
            var user = new User
            {
                CreatedAt = DateTime.UtcNow,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
            };
            return user;
        }

        [Fact]
        public async Task Should_Create_User_Successfully()
        {
            var request = GetUserRequestDtoMock();
            
            var userMock = GetUserMock(request);

            _userRepository.AddAsync(userMock)
                .ReturnsForAnyArgs(1);            

            var result = await _userService
                .CreateAsync(request);

            Assert.NotNull(result.Email);
            Assert.NotNull(result.FullName);
            Assert.True(result.Id == userMock.Id);
        }

        [Fact]
        public async Task Should_Fail_Create_User()
        {
            var request = GetUserRequestDtoMock();

            var userMock = GetUserMock(request);

            _userRepository.AddAsync(userMock)
                .ReturnsForAnyArgs(0);

            var result = await _userService
                .CreateAsync(request);

            Assert.Null(result.Email);
            Assert.Null(result.FullName);
            Assert.True(result.Id == Guid.Empty);
        }
    }
}
