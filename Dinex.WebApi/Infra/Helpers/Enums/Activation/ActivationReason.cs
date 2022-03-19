namespace Dinex.WebApi.Infra
{
    public enum ActivationReason
    {
        // success
        Success = 0,

        // errors
        InvalidCode = 1,
        ExpiredCode = 2,
    }
}
