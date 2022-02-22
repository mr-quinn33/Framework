using Framework.IOC.Attribute;

namespace Framework.IOC.Editor.Tests.Examples
{
    public class MyTestClass
    {
        [Inject] public MyTestDependency Dependency { get; set; } = null;
    }
}