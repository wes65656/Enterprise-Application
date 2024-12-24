using System.ComponentModel.DataAnnotations;

namespace NSE.Identidade.API.Models;

/// <summary>
/// This class is used to store the user register information
/// </summary>
public class UserRegister
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é invalido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }
    
    [Required]
    [Compare(nameof(UserRegister.Password), ErrorMessage = "Os campos {0} e {1} nao conferem")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}

/// <summary>
/// This class is used to store the user login information
/// </summary>
public class UserLogin
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é invalido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }
}

/// <summary>
/// This class is used to store the user answer login information
/// </summary>
public class UserAnswerLogin
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
}

/// <summary>
/// This class is used to store the user token information
/// </summary>
public class UserToken
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaim> Claims { get; set; }
}

/// <summary>
/// This class is used to store the user claim information
/// </summary>
public class UserClaim
{
    public string Value { get; set; }
    public string Type { get; set; }
}