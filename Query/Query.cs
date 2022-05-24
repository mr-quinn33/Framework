using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Query
{
    public abstract class Query<T> : IQuery<T>
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

        T IQuery<T>.Execute()
        {
            return Execute();
        }

        protected abstract T Execute();
    }
}