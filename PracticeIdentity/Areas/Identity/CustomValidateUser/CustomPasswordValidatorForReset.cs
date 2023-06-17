using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PracticeIdentity.Areas.Identity.CustomValidateUser
{
    public class CustomPasswordValidatorForReset<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        private readonly IHttpContextAccessor _httpContext;
        public CustomPasswordValidatorForReset(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (_httpContext.HttpContext.Request.Path.Value.Contains("ChangePassword"))
            {
                var username = await manager.GetUserNameAsync(user);
                if (password.ToLower().Contains("password"))
                    return IdentityResult.Failed(new IdentityError { Description = "Mật khẩu không được có từ password", Code = "PasswordContainsPassword" });
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Success;
            }
        }
    }
}