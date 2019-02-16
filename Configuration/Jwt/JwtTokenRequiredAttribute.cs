using Configuration.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net.Http;

namespace Configuration.Jwt
{
    /// <summary>
    /// Reads JWT token from HTTP Authorization header and stores permissions into ISession object
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtTokenRequiredAttribute : Attribute, IResourceFilter
    {
        private static readonly ExceptionToHttpCodeConverter _errorCodeConverter = new ExceptionToHttpCodeConverter();

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            try
            {
                ProcessAuthorization(context);
            }
            catch (Exception e) when ((e is UnauthorizedAccessException) || (e is InvalidTokenException))
            {
                ProcessError(context, e);
            }
            catch (Exception e)
            {
                ProcessError(context, new InvalidTokenException(e.Message, e));
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Nothing is required when returning from execution
        }

        private Session ProcessAuthorization(ResourceExecutingContext context)
        {
            return context.HttpContext.Request.Headers.TryGetValue(nameof(HttpClient.DefaultRequestHeaders.Authorization), out StringValues authHeader)
                    ? FillSession(context, authHeader)
                    : ProcessMissingAuthorization(context);
        }

        protected virtual Session ProcessMissingAuthorization(ResourceExecutingContext context)
        {
            throw new UnauthorizedAccessException("Authorization header is missing");
        }

        private static Session FillSession(ResourceExecutingContext context, StringValues authHeader)
        {
            IJwtManager jwtManager = GetService<IJwtManager>(context);
            Session session = (Session)GetService<ISession>(context);

            JwtTokenPayload payload = jwtManager.GetPayload(authHeader);
            CheckUserInformation(payload.Username);

            session.Username = payload.Username;
            session.Token = jwtManager.GetTokenFromAuthorizationHeader(authHeader);

            return session;
        }

        private static void ProcessError(ResourceExecutingContext context, Exception e)
        {
            ILogger<JwtTokenRequiredAttribute> logger = GetService<ILogger<JwtTokenRequiredAttribute>>(context);
            logger.LogError(e.Message);
            var errorCode = _errorCodeConverter.GetMessageAndHttpCode(e);
            context.HttpContext.Response.StatusCode = (int)errorCode.Status;
            context.Result = new JsonResult(errorCode.Message);
        }

        private static void CheckUserInformation(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidTokenException("Invalid JWT Token: username is missing");
            }
        }

        protected static T GetService<T>(ActionContext context)
        {
            return (T)context.HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}
