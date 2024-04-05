using Animal_Shelter;

namespace AnimalShelterTests.Mocks;

public class AuthenticationSettingsMock : IAuthenticationSettings
{
    public string JwtKey { get; set; } = "this is a test key with a minimum length of 16 characters";
    public int ExpiresDate { get; set; } = 1;
    public string JwtIssuer { get; set; } = "test";
}