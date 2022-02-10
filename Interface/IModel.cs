using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface IModel : ISetArchitecture, IGetUtility, IInvokeEvent
    {
        void Initialize();
    }
}