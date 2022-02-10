using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.System
{
    public abstract class System : ISystem
    {
        private IArchitecture architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture iArchitecture)
        {
            architecture = iArchitecture;
        }

        void ISystem.Initialize()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }
}