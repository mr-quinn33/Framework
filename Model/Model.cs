using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Model
{
    public abstract class Model : IModel
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

        void IModel.Initialize()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }
}