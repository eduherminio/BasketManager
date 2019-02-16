using System;
using Xunit.Sdk;

using Configuration.Helpers;

namespace Configuration.Test
{
    public static class XUnitHelpers
    {
        /// <summary>
        /// Verifies that the exact exception is thrown under AspectCore.AspectInvocationException
        /// </summary>
        /// <typeparam name="T">The type of the exception expected to be thrown</typeparam>
        /// <param name="testCode">A delegate to the code to be tested</param>
        /// <returns>The exception that was thrown, when successful</returns>
        /// <exceptions>
        /// T:Xunit.Sdk.ThrowsException
        /// Thrown when an exception was not thrown, or when an exception of the incorrect type is thrown
        /// </exceptions>
        public static T ThrowsAspectInvocationInnerException<T>(Action testCode)
            where T : Exception
        {
            try
            {
                testCode();
                return null;
            }
            catch (Exception e)
            {
                return (T)Throws(typeof(T), e.GetInnerAspectInvocationException());
            }
        }

        /// <summary>
        /// xunit/assert.xunit/ExceptionAsserts.cs
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Exception Throws(Type exceptionType, Exception exception)
        {
            GuardArgumentNotNull("exceptionType", exceptionType);

            if (exception == null)
            {
                throw new ThrowsException(exceptionType);
            }

            if (!exceptionType.Equals(exception.GetType()))
            {
                throw new ThrowsException(exceptionType, exception);
            }

            return exception;
        }

        /// <summary>
        /// xunit/assert.xunit/Guards.cs
        /// </summary>
        /// <param name="argName"></param>
        /// <param name="argValue"></param>
        private static void GuardArgumentNotNull(string argName, object argValue)
        {
            if (argValue == null)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}
