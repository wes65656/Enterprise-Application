using Microsoft.AspNetCore.Identity;

namespace NSE.Identidade.API.Extensions;

/// <summary>
/// Classe que descreve mensagens de erro de identidade em português.
/// </summary>
public class IdentityMensagensPortugues : IdentityErrorDescriber
{
    /// <summary>
    /// Mensagem de erro padrão.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de erro padrão.</returns>
    public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Ocorreu um erro desconhecido." }; }

    /// <summary>
    /// Mensagem de erro de falha de concorrência.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de falha de concorrência.</returns>
    public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Falha de concorrência otimista, o objeto foi modificado." }; }

    /// <summary>
    /// Mensagem de erro de senha incorreta.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha incorreta.</returns>
    public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Senha incorreta." }; }

    /// <summary>
    /// Mensagem de erro de token inválido.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de token inválido.</returns>
    public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Token inválido." }; }

    /// <summary>
    /// Mensagem de erro de login já associado.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de login já associado.</returns>
    public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Já existe um usuário com este login." }; }

    /// <summary>
    /// Mensagem de erro de login inválido.
    /// </summary>
    /// <param name="userName">Nome de usuário inválido.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de login inválido.</returns>
    public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"O login '{userName}' é inválido, pode conter apenas letras ou dígitos." }; }

    /// <summary>
    /// Mensagem de erro de email inválido.
    /// </summary>
    /// <param name="email">Email inválido.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de email inválido.</returns>
    public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"O email '{email}' é inválido." }; }

    /// <summary>
    /// Mensagem de erro de login duplicado.
    /// </summary>
    /// <param name="userName">Nome de usuário duplicado.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de login duplicado.</returns>
    public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"O login '{userName}' já está sendo utilizado." }; }

    /// <summary>
    /// Mensagem de erro de email duplicado.
    /// </summary>
    /// <param name="email">Email duplicado.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de email duplicado.</returns>
    public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"O email '{email}' já está sendo utilizado." }; }

    /// <summary>
    /// Mensagem de erro de nome de permissão inválido.
    /// </summary>
    /// <param name="role">Nome de permissão inválido.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de nome de permissão inválido.</returns>
    public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"A permissão '{role}' é inválida." }; }

    /// <summary>
    /// Mensagem de erro de nome de permissão duplicado.
    /// </summary>
    /// <param name="role">Nome de permissão duplicado.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de nome de permissão duplicado.</returns>
    public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"A permissão '{role}' já está sendo utilizada." }; }

    /// <summary>
    /// Mensagem de erro de usuário já possui senha.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de usuário já possui senha.</returns>
    public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "O usuário já possui uma senha definida." }; }

    /// <summary>
    /// Mensagem de erro de lockout não habilitado.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de lockout não habilitado.</returns>
    public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "O lockout não está habilitado para este usuário." }; }

    /// <summary>
    /// Mensagem de erro de usuário já possui permissão.
    /// </summary>
    /// <param name="role">Nome da permissão.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de usuário já possui permissão.</returns>
    public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"O usuário já possui a permissão '{role}'." }; }

    /// <summary>
    /// Mensagem de erro de usuário não possui permissão.
    /// </summary>
    /// <param name="role">Nome da permissão.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de usuário não possui permissão.</returns>
    public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"O usuário não tem a permissão '{role}'." }; }

    /// <summary>
    /// Mensagem de erro de senha muito curta.
    /// </summary>
    /// <param name="length">Comprimento mínimo da senha.</param>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha muito curta.</returns>
    public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"As senhas devem conter ao menos {length} caracteres." }; }

    /// <summary>
    /// Mensagem de erro de senha requer caractere não alfanumérico.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha requer caractere não alfanumérico.</returns>
    public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "As senhas devem conter ao menos um caracter não alfanumérico." }; }

    /// <summary>
    /// Mensagem de erro de senha requer dígito.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha requer dígito.</returns>
    public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "As senhas devem conter ao menos um digito ('0'-'9')." }; }

    /// <summary>
    /// Mensagem de erro de senha requer caractere em caixa baixa.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha requer caractere em caixa baixa.</returns>
    public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "As senhas devem conter ao menos um caracter em caixa baixa ('a'-'z')." }; }

    /// <summary>
    /// Mensagem de erro de senha requer caractere em caixa alta.
    /// </summary>
    /// <returns>Objeto <see cref="IdentityError"/> com a mensagem de senha requer caractere em caixa alta.</returns>
    public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "As senhas devem conter ao menos um caracter em caixa alta ('A'-'Z')." }; }
}