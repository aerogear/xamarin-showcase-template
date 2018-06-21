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
            
        }


    }
}