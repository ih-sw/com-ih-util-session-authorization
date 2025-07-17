using com.ih.session.authorization.Domain;
using com.ih.session.authorization.Errors;
using com.ih.session.authorization.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace com.ih.session.authorization.Filter;

public class AppAuthorizationLoggedAndPermission : ActionFilterAttribute
{
    private readonly string[] _claims;
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var appSessionService = context.HttpContext.RequestServices.GetService<IAppSessionService>();
        var session = appSessionService.Get().Result;

        appSessionService.InitialValidation();

        if (!session.HasSession)
        {
            throw new AppAuthenticationInvalidTokenCustomErrorException(
                contentBodyRequest: JsonConvert.SerializeObject(session));
        }
        
        if (session.Data!.CustomSession.ExpirationAt is not null &&
            DateTime.Now.CompareTo(session.Data!.CustomSession.ExpirationAt) > 0)
        {
            throw new AppAuthenticationExpiredSessionCustomErrorException(
                contentBodyRequest: JsonConvert.SerializeObject(session));
        }
        
        if (!ValidatePermission(session))
        {
            throw new AppAuthenticationForbiddenCustomException(
                contentBodyRequest: JsonConvert.SerializeObject(session));
        }
        base.OnActionExecuting(context);
    }
    
    private bool ValidatePermission(AppSessionDto appSession)
    {
        if (appSession.Data!.User.IsSuperUser)
        {
            return true;
        }

        if (_claims.Length > 0)
        {
            if (appSession.Data.Claims.Count > 0)
            {
                var permissionsGranted = 0;

                appSession.Data.Claims.ForEach(permissionGranted =>
                {
                    if (_claims.Contains(permissionGranted))
                    {
                        permissionsGranted++;
                    }
                });

                return permissionsGranted > 0;
            }

            return false;
        }

        return true;
    }
}