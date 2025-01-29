using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using FluentValidation;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Exceptions.ExceptionsBase; // Certifique-se de que está usando FluentValidation

namespace MyRecipeBook.Application.UseCase.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        var criptografiaDeSenha = new PasswordEncrypter();
        
        var autoMapper = new AutoMapper.MapperConfiguration(options => 
            options.AddProfile(new AutoMapping())).CreateMapper();
        
        // Validação da requisição
        Validate(request);
        
        var user = autoMapper.Map<Domain.Entities.User>(request);
        
        user.Password = criptografiaDeSenha.Encrypt(request.Password);
        
        
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