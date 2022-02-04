using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Extension
{
    public static class SendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ISendQuery self, IQuery<TResult> query)
        {
            return self.GetArchitecture().SendQuery(query);
        }
    }
}