namespace Animal_Shelter;

public interface IAuthenticationSettings
{
    public string JwtKey { get; set; }
    public int ExpiresDate { get; set; }
    public string JwtIssuer { get; set; }
}
public class AuthenticationSettings : IAuthenticationSettings
{
    public string JwtKey { get; set; }
    public int ExpiresDate { get; set; }
    public string JwtIssuer { get; set; }
}