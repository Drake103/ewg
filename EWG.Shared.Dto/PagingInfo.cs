namespace EWG.Shared.Dto
{
    public class PagingInfo
    {
        public PagingInfo(int startIndex, int pageSize)
        {
            PageSize = pageSize;
            StartIndex = startIndex;
        }

        public const PagingInfo All = null;

        public int StartIndex { get; set; }
        public int PageSize { get; set; }
    }
}