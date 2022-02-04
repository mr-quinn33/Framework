using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Model
{
    public abstract class Model : IModel
    {
        private IArchitecture _architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return _architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        void IModel.Initialize()
        {
            Initialize();
        }

        private protected abstract void Initialize();
    }
}