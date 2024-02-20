using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthenticationManager
{
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication);
    Task<string> CreateToken();
}