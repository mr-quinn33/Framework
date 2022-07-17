using Framework.Rules.Interfaces;

namespace Framework.Models.Interfaces
{
    public interface IModel : ISetGameMode, IGetUtility, IInvokeEvent
    {
        void Initialize();
    }
}