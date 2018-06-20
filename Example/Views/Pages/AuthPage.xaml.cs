using System;
using System.Collections.Generic;
using AeroGear.Mobile.Auth;
using AeroGear.Mobile.Core;
using Example.Auth;
using Example.Models;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
namespace Example.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            if (MobileCore.Instance.GetService<IAuthService>().CurrentUser() != null)
            {
                Device.BeginInvokeOnMainThread(() => ((RootPage)(App.Current.MainPage)).ChangePage(new UserDetails()));
                return;
            }
            InitializeComponent();
        }

        public void OnAuthenticateClicked(object sender, EventArgs args)
        {
            IAuthService service = MobileCore.Instance.GetService<IAuthService>();
            var authOptions = DependencyService.Get<IAuthenticateOptionsProvider>().GetOptions();
            service.Authenticate(authOptions).ContinueWith(result =>
            {
                Device.BeginInvokeOnMainThread(() => ((RootPage)(App.Current.MainPage)).ChangePage(new UserDetails()));
            });
        }
    }
}
