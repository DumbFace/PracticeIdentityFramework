using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PracticeIdentity.Areas.Identity.CustomValidateUser
{
    public class CustomPasswordValidatorForRegister<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        private readonly IHttpContextAccessor _httpContext;
        public CustomPasswordValidatorForRegister(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (_httpContext.HttpContext.Request.Path.Value.Contains("Register"))
            {
                var username = await manager.GetUserNameAsync(user);
                if (username.ToLower().Equals(password.ToLower()))
                    return IdentityResult.Failed(new IdentityError { Description = "Mật khẩu và password không thể giống nhau", Code = "SameUserPass" });
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Success;
            }
        }
    }
}