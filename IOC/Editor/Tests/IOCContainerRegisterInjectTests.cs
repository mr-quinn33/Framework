using Framework.IOC.Editor.Tests.Examples;
using NUnit.Framework;

namespace Framework.IOC.Editor.Tests
{
    public sealed class IOCContainerRegisterInjectTests
    {
        [Test]
        public void RegisterInject_RegisterNewInstance_NotNullAndEqual()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            var testInstance = new MyTestClass();
            iocContainer.Register<MyTestDependency>();
            iocContainer.Inject<MyTestDependency>(testInstance);

            Assert.IsNotNull(testInstance.Dependency);
            Assert.AreEqual(typeof(MyTestDependency), testInstance.Dependency.GetType());
        }

        [Test]
        public void RegisterInject_RegisterInstance_NotNullAndEqual()
        {
            var iocContainer = new IOCContainer() as IIOCContainer;
            var testInstance = new MyTestClass();
            iocContainer.Register<MyTestDependency>(new MyTestDependency());
            iocContainer.Inject<MyTestDependency>(testInstance);

            Assert.IsNotNull(testInstance.Dependency);
            Assert.AreEqual(typeof(MyTestDependency), testInstance.Dependency.GetType());
        }
    }
}