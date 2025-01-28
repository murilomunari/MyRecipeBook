using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var requestedCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();
        
        try
        {
            var cultureInfo = !string.IsNullOrEmpty(requestedCulture) 
                ? new CultureInfo(requestedCulture) 
                : CultureInfo.InvariantCulture; 

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
        catch (CultureNotFoundException)
        {
            
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        
        await _next(context);
    }
}