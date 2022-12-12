using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Rinboku.Infraestructure.TagHelpers
{
    public class PaginationTagHelper : TagHelper
    {
        private StringBuilder _htmlPagingBar = new StringBuilder();
        private string _pageQueryTag = "page";
        private int _pagingLength;
        private int _pagingStart;
        public int PageCount { get; set; }

        public string PageFirst { get; set; }

        public string PageLast { get; set; }

        public int PageNumber { get; set; }

        public int PageRange { get; set; }

        public int PageSize { get; set; }

        public string PageTarget { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("aria-label", "Page navigation");
            output.Content.SetHtmlContent(AddPageContent());
        }

        private string AddPageContent()
        {
            if (PageRange == 0)
            {
                PageRange = 1;
            }

            if (PageCount < PageRange)
            {
                PageRange = PageCount;
            }

            if (string.IsNullOrEmpty(PageFirst))
            {
                PageFirst = "First";
            }

            if (string.IsNullOrEmpty(PageLast))
            {
                PageLast = "Last";
            }

            AppendHtmlPagingBar();

            return _htmlPagingBar.ToString();
        }

        private void AppendButton(int num)
        {
            if (num < 1 || num > PageCount)
            {
                return;
            }

            var active = num == PageNumber ? "active" : "";
            _htmlPagingBar.Append($"<li class='page-item {active}'><a class='page-link'href='{PageTarget}?{_pageQueryTag}={num}'>{num}</a></li>");
        }

        private void AppendEveryButton()
        {
            if (PageNumber <= PageRange)
            {
                _pagingStart = 1;
                _pagingLength = 2 * PageRange + 1;
            }
            else if (PageNumber > PageRange && PageNumber < (PageCount - PageRange))
            {
                _pagingStart = PageNumber - PageRange;
                _pagingLength = PageNumber + PageRange;
            }
            else
            {
                _pagingStart = PageCount - (2 * PageRange);
                _pagingLength = PageCount + 1;
            }

            for (int i = _pagingStart; i < _pagingLength; i++)
            {
                AppendButton(i);
            }
        }

        private void AppendFirstButton()
        {
            if (PageNumber > 1)
            {
                _htmlPagingBar.Append($"<li class='page-item'><a class='page-link' href='{PageTarget}'>{PageFirst}</a></li>");
            }
        }

        private void AppendHtmlPagingBar()
        {
            _htmlPagingBar.Append(" <ul class='pagination'>");

            AppendFirstButton();
            AppendEveryButton();
            AppendLastButton();

            _htmlPagingBar.Append(" </ul");
        }

        private void AppendLastButton()
        {
            if (PageNumber < PageCount)
            {
                _htmlPagingBar.Append($"<li class='page-item'><a class='page-link' href='{PageTarget}?{_pageQueryTag}={PageCount}'>{PageLast}</a></li>");
            }
        }
    }
}
