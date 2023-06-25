namespace Dinex.Core;

public partial class User
{
    public enum Error
    {
        UserErrorToCreate,
        UserErrorToUpdate,

        UserNotFound,
        UserAlreadyExists,
    }
}
