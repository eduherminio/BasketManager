using System;
using Xunit;

using Configuration.Exceptions;
using Configuration.Helpers;

namespace Configuration.Test
{
    public class ExceptionHelpersTest
    {
        [Fact]
        public void GetInnerAspectInvocationException()
        {
            Exception aspectCoreException = new AspectCore.DynamicProxy.AspectInvocationException(
                null, new DatabaseException());

            Exception innerException = aspectCoreException.GetInnerAspectInvocationException();

            Assert.Equal(typeof(DatabaseException), innerException.GetType());
        }
    }
}
