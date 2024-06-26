﻿namespace Dinex.Core;

public class HistoryFileRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public TransactionActivity QueueType { get; set; }

    //[Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required IFormFile FileHistory { get; set; }
}
