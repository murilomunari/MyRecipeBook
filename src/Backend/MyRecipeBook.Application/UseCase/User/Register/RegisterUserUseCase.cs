using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using FluentValidation;
using MyRecipeBook.Exceptions.ExceptionsBase; // Certifique-se de que está usando FluentValidation

namespace MyRecipeBook.Application.UseCase.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        // Validação da requisição
        Validate(request);

        // Mapear a request em uma entidade
        // Exemplo: var userEntity = MapToEntity(request);

        // Criptografia da senha
        // Exemplo: userEntity.Password = EncryptPassword(request.Password);

        // Salvar no banco de dados
        // Exemplo: _repository.Save(userEntity);

        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
            Email = request.Email,
            
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}