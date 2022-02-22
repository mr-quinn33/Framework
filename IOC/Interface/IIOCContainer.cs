namespace Framework.IOC.Interface
{
    public interface IIOCContainer
    {
        /// <summary>
        ///     Register a new instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>();

        /// <summary>
        ///     Register an instance
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        void Register<T>(object instance);

        void Register<TParent, TChild>() where TChild : TParent;

        T Resolve<T>();

        void Inject<T>(object obj);

        void Clear();
    }
}