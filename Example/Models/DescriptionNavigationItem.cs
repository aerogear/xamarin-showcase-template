using System;
using Example.Views.Pages;
using Xamarin.Forms;

namespace Example.Models
{
    public class DescriptionNavigationItem : NavigationItem
    {
        public string Subtitle { get; set; }
        public string TextContent { get; set; }

        public override Page Page => new LandingPage(Subtitle,TextContent);

        public DescriptionNavigationItem()
        {
            this.TargetType = typeof(LandingPage);
        }
    }
}
