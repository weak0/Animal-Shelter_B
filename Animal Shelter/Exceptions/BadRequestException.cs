namespace Animal_Shelter.Exceptions;

public class BadRequestException: Exception
{
  public BadRequestException(string message) : base(message)
    {

    }

}
