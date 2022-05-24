using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class GetModelExtension
    {
        public static T GetModel<T>(this IGetModel self) where T : class, IModel
        {
            return self.GetArchitecture().GetModel<T>();
        }
    }
}