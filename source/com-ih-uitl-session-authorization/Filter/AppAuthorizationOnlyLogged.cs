using com.ih.session.authorization.Errors;
using com.ih.session.authorization.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace com.ih.session.authorization.Filter;

public class AppAuthorizationOnlyLogged : ActionFilterAttribute
{
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

        base.OnActionExecuting(context);
    }
}