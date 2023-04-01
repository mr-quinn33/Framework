using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    public sealed class IOCContainerRegisterTests
    {
        [Test]
        public void Register_RegisterSameType_True()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>();
            iocContainer.Register<IOCContainer>();

            Assert.IsTrue(true);
        }
    }
}