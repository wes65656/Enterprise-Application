using System.Text;
using System.Text.Json;
using NSE.WebMvc.APP.Models;

namespace NSE.WebMvc.APP.Service;

public class AutenticationService : IAutenticationService
{
    private readonly HttpClient _httpClient;
    
    public AutenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> Register(UserRegister userRegister)
    {
        var registerContent = new StringContent(JsonSerializer.Serialize(userRegister),
            Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/Criar-conta", registerContent);
        
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }

    public async Task<string> Login(UserLogin userLogin)
    {
        var loginContent = new StringContent(JsonSerializer.Serialize(userLogin),
                               Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/Autenticar", loginContent);
        
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }
}