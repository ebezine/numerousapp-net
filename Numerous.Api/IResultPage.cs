using System.Collections.Generic;

namespace Numerous.Api
{
    internal interface IResultPage<TResult>
    {
        IEnumerable<TResult> Values { get; set; }

        string NextUrl { get; set; }
    }
}