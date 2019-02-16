using Xunit;

using Configuration.Exceptions;

namespace Configuration.Test
{
    public class XUnitHelpersTest
    {
        [Fact]
        public void ThrowsAspectInvocationInnerException()
        {
            // AspectCore exception
            Assert.Throws<AspectCore.DynamicProxy.AspectInvocationException>(() => ThrowAspectCoreException());
            XUnitHelpers.ThrowsAspectInvocationInnerException<DatabaseException>(
                () => ThrowAspectCoreException());

            // Simple exception
            XUnitHelpers.ThrowsAspectInvocationInnerException<EntityDoesNotExistException>(
                () => ThrowSimpleException());
        }

        static private void ThrowAspectCoreException()
        {
            throw new AspectCore.DynamicProxy.AspectInvocationException(null, new DatabaseException());
        }

        static private void ThrowSimpleException()
        {
            throw new EntityDoesNotExistException();
        }
    }
}
