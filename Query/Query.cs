using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Query
{
    public abstract class Query<T> : IQuery<T>
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

        T IQuery<T>.Execute()
        {
            return Execute();
        }

        protected abstract T Execute();
    }
}