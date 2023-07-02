namespace Dinex.Core;

public partial class NotificationDto
{
    public string Message { get; set; }

    public NotificationDto(string message)
    {
        Message = message;
    }

}
