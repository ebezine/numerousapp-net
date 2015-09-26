#region References

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Numerous.Api.Sync;

#endregion

namespace Numerous.Api
{
    public static class ResultPageExtensionMethods
    {
        #region Methods

        /// <summary>
        /// Retrieves the specified number of results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultPageTask">The first result page.</param>
        /// <param name="count">The result count.</param>
        /// <returns>The results</returns>
        public static async Task<IEnumerable<T>> Take<T>(this Task<ResultPage<T>> resultPageTask, int count)
        {
            var results = new List<T>(count);

            var resultPage = await resultPageTask;
            results.AddRange(resultPage.Values.Take(count));

            while (resultPage.HasMoreResults && results.Count < count)
            {
                resultPage = await resultPage.Next();
                results.AddRange(resultPage.Values.Take(count - results.Count));
            }

            return results;
        }

        /// <summary>
        /// Retrieves all results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultPageTask">The first result page.</param>
        /// <returns>The results</returns>
        public static async Task<IEnumerable<T>> TakeAll<T>(this Task<ResultPage<T>> resultPageTask)
        {
            var results = new List<T>();

            var resultPage = await resultPageTask;
            results.AddRange(resultPage.Values);

            while (resultPage.HasMoreResults)
            {
                resultPage = await resultPage.Next();
                results.AddRange(resultPage.Values);
            }

            return results;
        }

        /// <summary>
        /// Retrieves the specified number of results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultPage">The first result page.</param>
        /// <param name="count">The result count.</param>
        /// <returns>The results</returns>
        public static IEnumerable<T> Take<T>(this ResultPage<T> resultPage, int count)
        {
            var results = new List<T>(count);

            results.AddRange(resultPage.Values.Take(count));

            while (resultPage.HasMoreResults && results.Count < count)
            {
                resultPage = resultPage.Next().AsSync();
                results.AddRange(resultPage.Values.Take(count - results.Count));
            }

            return results;
        }

        /// <summary>
        /// Retrieves all results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultPage">The first result page.</param>
        /// <returns>The results</returns>
        public static IEnumerable<T> TakeAll<T>(this ResultPage<T> resultPage)
        {
            var results = new List<T>();

            results.AddRange(resultPage.Values);

            while (resultPage.HasMoreResults)
            {
                resultPage = resultPage.Next().AsSync();
                results.AddRange(resultPage.Values);
            }

            return results;
        }

        #endregion
    }
}