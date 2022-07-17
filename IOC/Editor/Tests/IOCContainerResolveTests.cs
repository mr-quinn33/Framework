using Framework.IOC.Interfaces;
using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    public class IOCContainerResolveTests
    {
        [Test]
        public void Resolve_NoRegisterResolve_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            var obj = iocContainer.Resolve<IOCContainer>();

            Assert.IsNull(obj);
        }
    }
}