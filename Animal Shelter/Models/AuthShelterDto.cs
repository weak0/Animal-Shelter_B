namespace Animal_Shelter.Models;

public class AuthShelterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
 
}

public class AuthShelterRegisterDto : AuthShelterDto
{
    public string Name { get; set; }
}