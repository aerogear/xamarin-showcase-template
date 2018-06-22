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
    public partial class LandingPage : ContentPage
    {

        public String Subtitle
        {
            get => subtitle;
            private set
            {
                subtitle = value;
                LabelSubtitle.IsVisible = !String.IsNullOrEmpty(value);
                OnPropertyChanged("Subtitle");
            }
        }

        private string subtitle;

        public String TextContent
        {
            get => textContent;
            private set
            {
                textContent = value;
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = @"<html>
                <style>
                    body { 
                        font-family: sans-serif;
                        background-color:#FAFAFA;
                    }
                    a {
                        color:#2e6980;
                    }
                    ul {
                        padding-left:20;
                    }
                </style>
                <body>"+value+@"</body></html>";

                WebView.Source = htmlSource;
                OnPropertyChanged("TextContent");
            }
        }

        private string textContent;

        public LandingPage(String subtitle,String textContent)
        {
            InitializeComponent();
            BindingContext = this;
            Subtitle = subtitle;
            TextContent = textContent;

            WebView.Navigating += (s, e) =>
            {
                if (e.Url.StartsWith("http") || e.Url.StartsWith("https"))
                {
                    try
                    {
                        var uri = new Uri(e.Url);
                        Device.OpenUri(uri);
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
            };
        }
    }
}