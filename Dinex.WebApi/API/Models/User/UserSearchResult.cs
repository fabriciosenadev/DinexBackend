﻿namespace Dinex.WebApi.API.Models
{
    public class UserSearchResult
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
