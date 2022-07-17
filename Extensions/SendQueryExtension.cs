using Framework.Queries.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class SendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ISendQuery self, IQuery<TResult> query)
        {
            return self.GetGameMode().SendQuery(query);
        }

        public static TResult SendQuery<T, TResult>(this ISendQuery self) where T : IQuery<TResult>, new()
        {
            return self.GetGameMode().SendQuery<T, TResult>();
        }
    }
}