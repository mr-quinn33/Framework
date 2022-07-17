using Framework.IOC.Attributes;

namespace Framework.IOC.Editor.Tests.Examples
{
    public class MyTestClass
    {
        [Inject] public MyTestDependency Dependency { get; set; } = null;
    }
}