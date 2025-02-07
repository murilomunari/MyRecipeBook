using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Application.UseCase.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddPasswordEcrpter(services, configuration);
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
    
    private static void AddPasswordEcrpter(IServiceCollection services,IConfiguration configuration)
    {
        var additionalKeys = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        
        
        services.AddScoped(option => new PasswordEncrypter(additionalKeys!));
    }
}