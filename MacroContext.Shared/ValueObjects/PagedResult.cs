using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.ValueObjects
{
    public sealed class PagedResult<T>
    {
        public PagingInformation Paging { get; private set; }
        public int PageCount { get; private set; }
        public int ItemCount { get; private set; }
        public T[] Result { get; private set; }
        public bool HasMore { get; private set; }

        public PagedResult(PagingInformation paging, int pageCount, int itemCount, T[] result)
        {
            this.Paging = paging;
            this.PageCount = pageCount;
            this.ItemCount = itemCount;
            this.Result = result;
            this.HasMore = paging.PageIndex != pageCount;
        }
    }
}
