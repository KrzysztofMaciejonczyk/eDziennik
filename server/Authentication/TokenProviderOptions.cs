﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace EDziennik.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "EDziennikAudience";
        public static string Issuer { get; } = "EDziennik";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("EDziennikSecretSecurityKeyEDziennik"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
