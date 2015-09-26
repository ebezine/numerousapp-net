#region References

using System.Collections.Generic;

#endregion

namespace Numerous.Api
{
    internal interface IResultPage<TResult>
    {
        #region Properties

        IEnumerable<TResult> Values { get; set; }

        string NextUrl { get; set; }

        #endregion
    }
}