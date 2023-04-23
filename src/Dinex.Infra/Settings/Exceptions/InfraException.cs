namespace Dinex.Infra 
{ 
    public class InfraException : Exception
    {
        public InfraException() : base()
        {

        }

        public InfraException(string message) : base(message)
        {

        }

        public InfraException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
