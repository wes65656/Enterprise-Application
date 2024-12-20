using System.ComponentModel.DataAnnotations;

namespace NSE.Identidade.API.Models;

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

public class UserLogin
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é invalido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }
}