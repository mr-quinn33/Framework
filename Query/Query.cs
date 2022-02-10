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

        void ISetArchitecture.SetArchitecture(IArchitecture iArchitecture)
        {
            architecture = iArchitecture;
        }

        T IQuery<T>.Execute()
        {
            return Execute();
        }

        protected abstract T Execute();
    }
}