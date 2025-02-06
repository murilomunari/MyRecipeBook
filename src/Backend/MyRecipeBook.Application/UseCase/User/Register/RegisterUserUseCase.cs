using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using FluentValidation;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions.ExceptionsBase;
using MyRecipeBook.Domain.Repositories.User; // Certifique-se de que está usando FluentValidation

namespace MyRecipeBook.Application.UseCase.User.Register;

public class RegisterUserUseCase : IRegisterUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;
    
    private readonly PasswordEncrypter _passwordEncripter;

    public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        PasswordEncrypter passwordEncripter)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
    }
        

    public async Task<ResponseRegisteredUserJson>  Execute(RequestRegisterUserJson request)
    {
        
        // Validação da requisição
        Validate(request);
        
        var user = _mapper.Map<Domain.Entities.User>(request);
        
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _userWriteOnlyRepository.Add(user);
        
        await _unitOfWork.Commit();

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