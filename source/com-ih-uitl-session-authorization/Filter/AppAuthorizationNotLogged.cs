using com.ih.session.authorization.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace com.ih.session.authorization.Filter;

public class AppAuthorizationNotLogged : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var appSessionService = context.HttpContext.RequestServices.GetService<IAppSessionService>();

        appSessionService.InitialValidation();

        base.OnActionExecuting(context);
    }
}