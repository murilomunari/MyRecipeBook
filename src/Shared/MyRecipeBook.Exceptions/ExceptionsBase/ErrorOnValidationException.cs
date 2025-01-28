namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    public IList<String> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<String> errors)
    {
        ErrorMessages = errors;
    }
}