using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.System
{
    public abstract class System : ISystem
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

        public abstract void Initialization();
    }
}