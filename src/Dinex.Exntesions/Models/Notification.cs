﻿namespace Dinex.Extensions;

public partial class Notification
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

}
