using System;

namespace Darewise_Challenge.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    } 
}

