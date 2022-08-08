namespace Dinex.Business
{
    public class UserAmountManager : BaseService, IUserAmountManager
    {
        public readonly IUserAmountAvailableService _userAmountAvailableService;
        public readonly ILaunchService _launchService;

        public UserAmountManager(
            IMapper mapper,
            INotificationService notificationService)
            : base(mapper, notificationService)
        {

        }

        /**
         * TODO: criar uma regra adicional para quando não houver saldo disponível
         * para que seja avaliado se existe lançamentos antes de devolver o saldo zerado
         * 
         * WARNING: esta regra só será aplicada caso não exista saldo disponível e existam lançamentos
         */
    }
}
