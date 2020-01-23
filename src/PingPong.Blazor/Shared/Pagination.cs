using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PingPong.Blazor.Shared
{
    public partial class Pagination
    {
        [Parameter] public int                Page          { get; set; }
        [Parameter] public EventCallback<int> OnPageChanged { get; set; }

        [Parameter] public int PageSize { get; set; } = 10;

        [Parameter] public int TotalItems { get; set; }

        //[Parameter] public int ShowPagesRange { get; set; } = 3;

        [Parameter] public bool ShowWhenOnlyOnePage { get; set; } = false;


        public int TotalPages
        {
            get
            {
                var pages = 1;
                if (TotalItems > 0)
                    pages = TotalItems / PageSize + (TotalItems % PageSize > 0 ? 1 : 0);

                return pages;
            }
        }

        public bool HasPrevPage => Page > 1;
        public int  PrevPage    => Math.Max(Page - 1, 1);

        public bool HasNextPage => Page < TotalPages;
        public int  NextPage    => Math.Min(Page + 1, TotalPages);


        // Implement later:

        // ------ [   Pages to Show    ] ------
        // ShowPagesRange == 3
        // 1 ... 17 18 19 [20] 21 22 23 ... 42

        // public int EarliestPageToShow => Math.Max(Page - ShowPagesRange, 1);
        // public int LatestPageToShow   => Math.Min(Page + ShowPagesRange, TotalPages);

        // public bool HasEllipsisBefore => EarliestPageToShow > 2;
        // public bool HasEllipsisAfter  => LatestPageToShow < TotalPages - 1;


        private async Task OnPageButtonClicked(int page)
        {
            await OnPageChanged.InvokeAsync(page);
        }
    }
}