namespace TransfeloTask.Common.InterFaces
{
    public interface IPagedList
    {
        int TotalCount { get; }

        int Page { get; }

        int PageSize { get; }
    }
}
