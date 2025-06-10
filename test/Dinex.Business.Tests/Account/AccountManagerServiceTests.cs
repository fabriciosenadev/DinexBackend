using AutoMapper;
using Bogus;
using Dinex.Business;
using Dinex.Core;
using Dinex.Infra;
using Dinex.Extensions;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;

namespace Dinex.Business.AccountTests
{
    [ExcludeFromCodeCoverage]
    public class AccountManagerServiceTests
    {
        private readonly AccountManagerService _service;
        private readonly IUserService _userService;
        private readonly ICategoryManager _categoryManager;
        private readonly IEmailService _emailService;
        private readonly ICodeManagerService _codeManagerService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly Faker _faker;

        public AccountManagerServiceTests()
        {
            _faker = new Faker("pt_BR");

            _userService = Substitute.For<IUserService>();
            _categoryManager = Substitute.For<ICategoryManager>();
            _emailService = Substitute.For<IEmailService>();
            _codeManagerService = Substitute.For<ICodeManagerService>();
            _notificationService = Substitute.For<INotificationService>();

            var mapperConfig = new MapperConfiguration(cfg => { });
            _mapper = mapperConfig.CreateMapper();

            _service = new AccountManagerService(
                _userService,
                _categoryManager,
                _emailService,
                _codeManagerService,
                _mapper,
                _notificationService);
        }

        private User GetUserMock(string email)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FullName = _faker.Person.FullName,
                UserAccount = new Account
                {
                    Email = email,
                    Password = _faker.Random.String2(10)
                }
            };
        }

        [Fact]
        public async Task Should_Send_Activation_Code_Successfully()
        {
            var email = _faker.Internet.Email();
            var user = GetUserMock(email);
            var generatedCode = _faker.Random.String2(10);

            _codeManagerService.GenerateCode(BaseService.DefaultCodeLength)
                .Returns(generatedCode);
            _userService.GetByEmailAsync(email).Returns(user);
            _emailService.SendByTemplateAsync(Arg.Any<SendEmailDto>())
                .Returns("ok");

            var result = await _service.SendActivationCodeAsync(email);

            await _codeManagerService.Received(1)
                .AssignCodeToUserAsync(user.Id, generatedCode, CodeReason.Activation);
            await _emailService.Received(1)
                .SendByTemplateAsync(Arg.Is<SendEmailDto>(d => d.EmailTo == email && d.GeneratedCode == generatedCode));
            Assert.Equal("ok", result);
        }

        [Fact]
        public async Task Should_Activate_Account_Successfully()
        {
            var email = _faker.Internet.Email();
            var activationCode = _faker.Random.String2(6);
            var user = GetUserMock(email);

            _userService.GetByEmailAsync(email).Returns(user);

            await _service.ActivateAccountAsync(email, activationCode);

            await _codeManagerService.Received(1)
                .ValidateActivationCode(activationCode, user.Id);
            await _codeManagerService.Received(1)
                .ClearAllCodesByUserAsync(user.Id, CodeReason.Activation);
            await _userService.Received(1)
                .ActivateUserAsync(user);
        }
    }
}
