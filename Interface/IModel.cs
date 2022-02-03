using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface IModel : ISetArchitecture, IGetUtility, IInvokeEvent
    {
        void Initialization();
    }
}