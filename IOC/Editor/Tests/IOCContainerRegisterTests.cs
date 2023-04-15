using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    [TestFixture]
    public static class IOCContainerRegisterTests
    {
        [Test]
        public static void Register_RegisterSameType_True()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>();
            iocContainer.Register<IOCContainer>();

            Assert.IsTrue(true);
        }
    }
}