#region References

using System.Threading.Tasks;

#endregion

namespace Numerous.Api.Sync
{
    /// <summary>
    /// Provides extension methods for using async methods in a synchronous mode.
    /// </summary>
    public static class SyncExtensionMethods
    {
        #region Methods

        /// <summary>
        /// Execute a task synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the task return value.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The return value of the task</returns>
        public static T AsSync<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Execute a task synchronously.
        /// </summary>
        /// <param name="task">The task.</param>
        public static void AsSync(this Task task)
        {
            task.Wait();
        }

        #endregion
    }
}