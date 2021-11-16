using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Web.Helper.Types
{
    public class PageInfo
    {
        private int recordCount;
        private int pageCount;
        private int pageSize;
        private int pageNumber;

        public PageInfo()
        {
            pageCount = 0;
            recordCount = 0;
            pageNumber = 1;
            pageSize = 5;
            IsAsc = false;
        }

        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                calculatePageInfo();
                //pageCount = (int)Math.Round(recordCount / (double)PageSize, 0);
            }
        }

        public int PageCount { get { return pageCount; } }
        public int PageNumber { get { return pageNumber; } set { pageNumber = value; } }
        public int PageSize { get { return pageSize; } set { pageSize = value; calculatePageInfo(); } }
        public string SortColumn { get; set; }
        public bool IsAsc { get; set; }


        public int SkipSize
        {
            get
            {
                return pageSize * (pageNumber < 1 ? 0 : pageNumber - 1);
            }
        }

        private void calculatePageInfo()
        {
            if (recordCount == 0 || pageSize == 0)
            {
                pageCount = 1;
            }
            else
            {
                pageCount = recordCount / pageSize;
                if (recordCount % pageSize > 0)
                    pageCount++;
            }

            if (pageNumber > pageCount)
                pageNumber = pageCount;
            else if (pageNumber < 1)
                pageNumber = 1;
        }

    }
}
