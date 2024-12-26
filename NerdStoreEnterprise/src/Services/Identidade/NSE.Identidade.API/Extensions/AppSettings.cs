namespace NSE.Identidade.API.Extensions;

/// <summary>
/// Configurações do app
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Chave de criptografia
    /// </summary>
    public string Secret { get; set; }
    
    /// <summary>
    /// Horas de expiração
    /// </summary>
    public int ExpirationHours { get; set; }
    
    /// <summary>
    /// Emissor do token
    /// </summary>
    public string Issuer { get; set; }
    
    /// <summary>
    /// Valido em
    /// </summary>
    public string ValidIn { get; set; }
}