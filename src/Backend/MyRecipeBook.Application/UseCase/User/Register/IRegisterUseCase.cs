using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCase.User.Register;

public interface IRegisterUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    
    
}