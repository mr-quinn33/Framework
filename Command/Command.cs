using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Command
{
    public abstract class Command : ICommand
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

        public abstract void Execute();
    }
}