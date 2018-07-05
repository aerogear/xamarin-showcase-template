using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage(string source)
        {
            InitializeComponent();
            this.PageWebView.Source = source;
        }

        public WebPage(string source, string title)
        {
            InitializeComponent();
            this.PageWebView.Source = source;
            this.Title = title;
        }

    }
}