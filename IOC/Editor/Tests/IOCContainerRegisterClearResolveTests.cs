using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    public sealed class IOCContainerRegisterClearResolveTests
    {
        [Test]
        public void RegisterClearResolve_RegisterNewInstance_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>();
            iocContainer.Clear();
            var obj = iocContainer.Resolve<IOCContainer>();

            Assert.IsNull(obj);
        }

        [Test]
        public void RegisterClearResolve_RegisterInstance_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>(new IOCContainer());
            iocContainer.Clear();
            var obj = iocContainer.Resolve<IOCContainer>();

            Assert.IsNull(obj);
        }

        [Test]
        public void RegisterClearResolve_RegisterDependency_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IIOCContainer, IOCContainer>();
            iocContainer.Clear();
            var obj = iocContainer.Resolve<IIOCContainer>();

            Assert.IsNull(obj);
        }

        [Test]
        public void RegisterClearResolve_RegisterInstanceDependency_Null()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IIOCContainer>(iocContainer);
            iocContainer.Clear();
            var obj = iocContainer.Resolve<IIOCContainer>();

            Assert.IsNull(obj);
        }
    }
}