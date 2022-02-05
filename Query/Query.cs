using Framework.Interface;

namespace Framework.Query
{
    public abstract class Query<T> : IQuery<T>
    {
        private IArchitecture _architecture;

        public void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        public IArchitecture GetArchitecture()
        {
            return _architecture;
        }

        T IQuery<T>.Execute()
        {
            return Execute();
        }

        protected abstract T Execute();
    }
}