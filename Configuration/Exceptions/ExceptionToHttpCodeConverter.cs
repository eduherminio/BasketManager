using System;
using System.Collections.Generic;
using System.Net;

namespace Configuration.Exceptions
{
    public class ExceptionToHttpCodeConverter
    {
        private readonly Dictionary<Type, HttpExceptionResponseInfo> _exceptionInfo = new Dictionary<Type, HttpExceptionResponseInfo>();

        protected void AddValues(Type type, HttpStatusCode code, string msg)
        {
            _exceptionInfo.Add(type, new HttpExceptionResponseInfo(code, msg));
        }

        private HttpExceptionResponseInfo GetValue(Type type)
        {
            if (!_exceptionInfo.TryGetValue(type, out HttpExceptionResponseInfo info))
            {
                info = new HttpExceptionResponseInfo(HttpStatusCode.InternalServerError, null);
            }

            return info;
        }

        public ExceptionToHttpCodeConverter()
        {
            AddValues(typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized, "UnauthorizedAccessException");
            AddValues(typeof(InvalidDataException), HttpStatusCode.BadRequest, "InvalidDataException");
            AddValues(typeof(InvalidOperationException), HttpStatusCode.BadRequest,
                "InvalidOperationException");
        }


        public HttpExceptionResponseInfo GetMessageAndHttpCode(Exception exc)
        {
            var values = GetValue(exc.GetType());

            if (values.Message == null)
            {
                values.Message = exc.ToString();
            }

            return values;
        }
    }
}
