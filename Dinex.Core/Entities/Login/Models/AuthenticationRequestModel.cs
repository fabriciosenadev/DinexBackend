﻿namespace Dinex.Core
{
    public class AuthenticationRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
