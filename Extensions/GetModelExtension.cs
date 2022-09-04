using Framework.Models;
using Framework.Rules;

namespace Framework.Extensions
{
    public static class GetModelExtension
    {
        public static T GetModel<T>(this IGetModel self) where T : class, IModel
        {
            return self.GetGameMode().GetModel<T>();
        }
    }
}