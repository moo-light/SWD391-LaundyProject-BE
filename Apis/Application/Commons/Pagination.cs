using System.Collections;

namespace Application.Commons
{
    public class Pagination<T>
    {
        private int pageSize = 10;
        private ICollection<T> _items;

        public int TotalItemsCount { get; set; }
        public int PageSize
        {
            get => pageSize; set
            {
                if (value == 0) throw new InvalidDataException("PageSize equals 0");
                pageSize = value;
            }
        }
        public int TotalPagesCount
        {
            get
            {
                //if (PageIndex == TotalPagesCount) throw new InvalidDataException($"PageIndex must be smaller than {TotalPagesCount}");
                var count = TotalItemsCount / PageSize;
                if (TotalItemsCount % PageSize == 0)
                {
                    return count;
                }
                return count + 1;
            }
        }
        public int PageIndex { get; set; }

        /// <summary>
        /// page number start from 0
        /// </summary>
        public bool Next => PageIndex + 1 < TotalPagesCount;
        public bool Previous => PageIndex > 0;
        public ICollection<T> Items
        {
            get
            {
                return _items;
            }
            set => _items = value;
        }

    }
}
