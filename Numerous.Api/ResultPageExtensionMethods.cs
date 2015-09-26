using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Numerous.Api.Sync;

namespace Numerous.Api
{
    public static class ResultPageExtensionMethods
    {
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
    }
}