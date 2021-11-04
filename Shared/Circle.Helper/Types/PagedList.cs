using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Helper.Utilities.Types
{
    public class MappedPagedList<TSource, TDestination> : PagedList<TDestination>
    {

        public MappedPagedList(IQueryable<TSource> source, int pageNumber, int pageSize, Func<IEnumerable<TSource>, IEnumerable<TDestination>> map)
            : base(pageNumber, pageSize, source.Count())
        {
            this.AddRange(map(source.Skip(this.SkipSize).Take(this.PageSize).ToList()));
        }

        public MappedPagedList(List<TSource> source, int pageNumber, int pageSize, Func<IEnumerable<TSource>, IEnumerable<TDestination>> map)
            : base(pageNumber, pageSize, source.Count())
        {
            this.AddRange(map(source.Skip(this.SkipSize).Take(this.PageSize).ToList()));
        }

        public MappedPagedList(IQueryable<TSource> source, int pageNumber, int pageSize, List<SearchFilter> filters, Func<IEnumerable<TSource>, IEnumerable<TDestination>> map)
    : base(pageNumber, pageSize, source.Count(), filters)
        {
            this.AddRange(map(source.Skip(this.SkipSize).Take(this.PageSize).ToList()));
        }

        public MappedPagedList(List<TSource> source, int pageNumber, int pageSize, List<SearchFilter> filters, Func<IEnumerable<TSource>, IEnumerable<TDestination>> map)
            : base(pageNumber, pageSize, source.Count(), filters)
        {
            this.AddRange(map(source.Skip(this.SkipSize).Take(this.PageSize).ToList()));
        }
    }


    public class PagedList<T> : List<T>
    {
        public int RecordCount { get; private set; }
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int SkipSize { get { return (PageNumber - 1) * PageSize; } }
        public List<SearchFilter> Filters { get; set; }

        public PagedList()
        {

        }
        public PagedList(IEnumerable<T> source)
        {
            RecordCount = source.Count();
            PageCount = 1;
            PageNumber = 1;
            PageSize = RecordCount;
            Filters = null;
            AddRange(source);
        }

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize, List<SearchFilter> filters)
        {
            RecordCount = source.Count();
            PageCount = GetPageCount(pageSize, RecordCount);
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            Filters = filters;

            AddRange(source.Skip(SkipSize).Take(PageSize).ToList());
        }

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            RecordCount = source.Count();
            PageCount = GetPageCount(pageSize, RecordCount);
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;

            AddRange(source.Skip(SkipSize).Take(PageSize).ToList());
        }


        protected PagedList(int pageNumber, int pageSize, int recordCount)
        {
            RecordCount = recordCount;
            PageCount = GetPageCount(pageSize, RecordCount);
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
        }

        protected PagedList(int pageNumber, int pageSize, int recordCount, List<SearchFilter> filters)
        {
            RecordCount = recordCount;
            PageCount = GetPageCount(pageSize, RecordCount);
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            Filters = filters;
        }

        private int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            var remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }
    }
}
