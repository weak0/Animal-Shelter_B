namespace Animal_Shelter;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int ExpiresDate { get; set; }
    public string JwtIssuer { get; set; }
}