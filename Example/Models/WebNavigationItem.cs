using System;
using Example.Views.Pages;
using Xamarin.Forms;

namespace Example.Models
{
    public class WebNavigationItem : NavigationItem
    {
        public string Source { get; set; }

        public override Page Page => new WebPage(Source);

        public WebNavigationItem()
        {
            this.TargetType = typeof(WebPage);
        }
    }
}
