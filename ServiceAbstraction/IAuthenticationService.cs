using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO_S;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
