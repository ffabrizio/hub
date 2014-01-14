namespace Renesis.Models.Utils
{
	public class Paging
	{
		public Paging(int page, int pagesize, int total)
		{
			Page = page;
			PreviousPage = page > 1 ? (page - 1) : 1;
			NextPage = page + 1;

			PageSize = pagesize;
			Total = total;

			ItemsLeft = Total - (Page * PageSize);
			PagesLeft = Total / PageSize - (Page + 1);
		}

		public int Page { get; private set; }
		public int NextPage { get; private set; }
		public int PreviousPage { get; private set; }
		public int PageSize { get; private set; }
		public int Total { get; private set; }
		public int ItemsLeft { get; private set; }
		public int PagesLeft { get; private set; }
	}
}