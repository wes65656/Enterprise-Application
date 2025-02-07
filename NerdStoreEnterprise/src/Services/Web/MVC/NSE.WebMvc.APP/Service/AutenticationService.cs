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
        _httpClient.BaseAddress = new Uri("https://localhost:7257/api/");
    }
    
    public async Task<string> Register(UserRegister userRegister)
    {
        var registerContent = new StringContent(
            JsonSerializer.Serialize(userRegister),
            Encoding.UTF8, 
            "application/json");
        
        var response = await _httpClient.PostAsync("identidade/Criar-conta", registerContent);
        
        if (!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Login(UserLogin userLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(userLogin),
            Encoding.UTF8, 
            "application/json");
        
        var response = await _httpClient.PostAsync("identidade/Autenticar", loginContent);
        
        if (!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadAsStringAsync();
    }
}