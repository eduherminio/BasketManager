using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Configuration.Jwt
{
    /// <summary>
    /// Sets the permissions required to execute this method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationAttribute : AbstractInterceptorAttribute
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            ISession session = (ISession)context.ServiceProvider.GetService(typeof(ISession));
            if (session.IsAuthenticated())
            {
                await next(context).ConfigureAwait(false);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
