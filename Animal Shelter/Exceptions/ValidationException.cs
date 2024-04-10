namespace Animal_Shelter.Exceptions;

public class ValidationException: Exception
{
  public ValidationException(string message) : base(message)
  {
  }
}
