using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers;

[ApiController]
[Route("api/identidade")]
public class AuthController : Controller  // Later we can fix this providing a base class with common behavior
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    
    [HttpPost("Criar-conta")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        System.Diagnostics.Debugger.Break();
        
        if (!ModelState.IsValid) return BadRequest();   
        
        var user = new IdentityUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, userRegister.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok();
        }
        
        return BadRequest();
    }
    
    [HttpPost("Autenticar")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

        if (result.Succeeded)
        {
            return Ok();
        }
        
        return BadRequest();
    }
}