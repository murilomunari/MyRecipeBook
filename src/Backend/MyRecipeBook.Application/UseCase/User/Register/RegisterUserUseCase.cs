using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCase.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        
        Validate(request);
        
        //Mapear a request em uma entidade
        
        //Criptografia da senha
        
        //Salvar no banco de dados
        
        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        
        var result = validator.Validate(request);

        if (result.IsValid ==false)
        {
            var erroes = result.Errors.Select(x => x.ErrorMessage);
            
            throw new Exception();
        }
    }
}