using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    [TestFixture]
    public static class IOCContainerRegisterResolveTests
    {
        [Test]
        public static void RegisterResolve_RegisterNewInstance_NotNullAndNotEqual()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>();
            var obj = iocContainer.Resolve<IOCContainer>();

            Assert.IsNotNull(obj);
            Assert.AreNotEqual(iocContainer, obj);
        }

        [Test]
        public static void RegisterResolve_RegisterInstance_Equal()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IOCContainer>(new IOCContainer());
            var instance1 = iocContainer.Resolve<IOCContainer>();
            var instance2 = iocContainer.Resolve<IOCContainer>();

            Assert.AreEqual(instance1, instance2);
        }

        [Test]
        public static void RegisterResolve_RegisterDependency_Equal()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IIOCContainer, IOCContainer>();
            var obj = iocContainer.Resolve<IIOCContainer>();

            Assert.AreEqual(typeof(IOCContainer), obj.GetType());
        }

        [Test]
        public static void RegisterResolve_RegisterInstanceDependency_Equal()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            iocContainer.Register<IIOCContainer>(iocContainer);
            var instance1 = iocContainer.Resolve<IIOCContainer>();
            var instance2 = iocContainer.Resolve<IIOCContainer>();

            Assert.AreEqual(instance1, instance2);
            Assert.AreEqual(iocContainer, instance1);
        }
    }
}