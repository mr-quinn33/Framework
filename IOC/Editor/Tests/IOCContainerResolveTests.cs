using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    [TestFixture]
    public static class IOCContainerResolveTests
    {
        [Test]
        public static void Resolve_NoRegisterResolve_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            var obj = iocContainer.Resolve<IOCContainer>();

            Assert.IsNull(obj);
        }
    }
}