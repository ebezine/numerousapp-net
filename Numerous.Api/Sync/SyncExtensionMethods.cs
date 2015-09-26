using System.Threading.Tasks;

namespace Numerous.Api.Sync
{
    public static class SyncExtensionMethods
    {
        public static T AsSync<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        public static void AsSync(this Task task)
        {
            task.Wait();
        }
    }
}