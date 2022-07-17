using Framework.Models.Interfaces;
using Framework.Rules.Interfaces;

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