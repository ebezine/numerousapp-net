#region References

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Numerous.Api.Sync;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Gets a partial view of a result set.
    /// </summary>
    /// <typeparam name="T">The result data type.</typeparam>
    public class ResultPage<T>
    {
        #region Properties

        /// <summary>
        /// Gets the values in the current page.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IEnumerable<T> Values { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has more results.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has more results; otherwise, <c>false</c>.
        /// </value>
        public bool HasMoreResults { get; internal set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next page of the result set.
        /// </summary>
        /// <returns>The next page of the result set.</returns>
        public async Task<ResultPage<T>> Next()
        {
            return await NextEvaluator();
        }

        #endregion

        #region Internal Helpers

        internal Func<Task<ResultPage<T>>> NextEvaluator { get; set; }

        internal static readonly ResultPage<T> Empty = new ResultPage<T>
        {
            Values = new T[0],
            HasMoreResults = false,
            NextEvaluator = () => new Task<ResultPage<T>>(() => Empty)
        };

        internal static ResultPage<T> SinglePage(IEnumerable<T> results)
        {
            return new ResultPage<T>
            {
                Values = results,
                HasMoreResults = false,
                NextEvaluator = () => new Task<ResultPage<T>>(() => Empty)
            };
        } 

        #endregion
    }
}