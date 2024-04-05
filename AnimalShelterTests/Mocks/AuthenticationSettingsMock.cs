using Animal_Shelter;

namespace AnimalShelterTests.Mocks;

public class AuthenticationSettingsMock : IAuthenticationSettings
{
    public string JwtKey { get; set; } = "JanPawelGralWKartyIMiałŁepObdarty";
    public int ExpiresDate { get; set; } = 1;
    public string JwtIssuer { get; set; } = "test";
}