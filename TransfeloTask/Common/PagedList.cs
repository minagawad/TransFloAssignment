using TransfeloTask.Common.InterFaces;

namespace TransfeloTask.Common
{
    public class PagedList<T> : IPagedList
    {
        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }   
        public List<T> List { get; set; }
    }
}
