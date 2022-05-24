using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Command
{
    public abstract class Command : ICommand
    {
        private IArchitecture architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            this.architecture = architecture;
        }

        void ICommand.Execute()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}