using NSE.WebMvc.APP.Models;

namespace NSE.WebMvc.APP.Service;

public interface IAutenticationService
{
    Task<string> Register(UserRegister userRegister);
    Task<string> Login(UserLogin userLogin);
}