namespace Dinex.Business;

public class BaseService
{
    public const int Success = 1;

    public const int InvalidId = 0;

    public const int DefaultCodeLength = 32;

    public readonly INotificationService Notification;
    public readonly IMapper _mapper;
    protected BaseService(IMapper mapper, INotificationService notification)
    {
        _mapper = mapper;
        Notification = notification;
    }

    protected bool ExecRequestValidation<TValidator, TRequest>(TValidator validator, TRequest request) where TValidator : AbstractValidator<TRequest> where TRequest : class
    {
        var validatorResult = validator.Validate(request);
        
        if (validatorResult.IsValid) return true;

        Notify(validatorResult);
        
        return false;
    }
    
    protected bool ExecEntityValidation<TValidator, TEntity>(TValidator validator, TEntity entity) where TValidator : AbstractValidator<TEntity> where TEntity : Entity
    {
        var validatorResult = validator.Validate(entity);
        
        if (validatorResult.IsValid) return true;

        Notify(validatorResult);
        
        return false;
    }

    private void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    private void Notify(string mensagem)
    {
        Notification.RaiseError(new NotificationDto(mensagem));
    }
}
