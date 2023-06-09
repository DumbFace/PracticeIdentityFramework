using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PracticeIdentity.Services.TokenServices
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, IdentityUser user);
        string ValidateToken(string token);
    }
}