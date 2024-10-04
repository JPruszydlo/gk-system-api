using gk_system_api.Entities;
using gk_system_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace gk_system_api.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class FilterUrlAttribute : AttributeBase
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var config = base.GetService<IConfiguration>(context);
            var acceptedClients = config.GetSection("AcceptedClients").Get<string[]>();
            var url = context.HttpContext.Request.Headers["Referer"].ToString().Replace("www.", "");
            if (acceptedClients.FirstOrDefault(x => x == url) == null)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class CheckApiKeyAttribute : AttributeBase
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var credentials = context.HttpContext.DecodeAuthHeader();
            if (credentials == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var config = base.GetService<IConfiguration>(context);
            var apiKey = config.GetValue<string>("ApiKey");
            var apiUser = config.GetValue<string>("ApiUser");

            if (credentials.Password == apiKey && credentials.Login == apiUser)
                return;
            context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class FilterUserAttribute : AttributeBase
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var credentials = context.HttpContext.DecodeAuthHeader();
            if (credentials == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var dbService = base.GetService<IDatabaseService>(context);

            if (dbService.GetUserByPassword(credentials.Login, credentials.Password) == null)
                context.Result = new UnauthorizedResult();
        }
    }

    class AttributeBase: ActionFilterAttribute
    {
        protected T GetService<T>(ActionExecutingContext context)
            => (T)context.HttpContext.RequestServices.GetService(typeof(T));
    }
}
