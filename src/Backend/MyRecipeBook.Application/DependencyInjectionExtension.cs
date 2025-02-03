using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Application.UseCase.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddPasswordEcrpter(services);
        AddUseCase(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration
        (options => 
            options.AddProfile(new AutoMapping())).CreateMapper());
    }


    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterUseCase, RegisterUserUseCase>();
    }
    
    private static void AddPasswordEcrpter(IServiceCollection services)
    {
        services.AddScoped(option => new PasswordEncrypter());
    }
}