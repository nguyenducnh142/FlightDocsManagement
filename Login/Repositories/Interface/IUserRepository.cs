using LoginService.Models;

namespace LoginService.Repositories.Interface
{
    public interface IUserRepository
    {
        AuthenticationToken GenerateAuthToken(Login user);
    }
}
