using System;
using System.Collections.Generic;
using AeroGear.Mobile.Auth;
using AeroGear.Mobile.Core;
using Example.Auth;
using Example.Models;
using Example.Resources;
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

        public async void OnAuthenticateClicked(object sender, EventArgs args)
        {
            IAuthService service = MobileCore.Instance.GetService<IAuthService>();
            var authOptions = DependencyService.Get<IAuthenticateOptionsProvider>().GetOptions();

            try 
            {
                var user = await service.Authenticate(authOptions);
                Device.BeginInvokeOnMainThread(() => ((RootPage)(App.Current.MainPage)).ChangePage(new UserDetails()));
            }
            catch 
            {
                App.Current.MainPage.DisplayAlert(StringResources.dialog_error_title, StringResources.service_auth_authentication_failed, StringResources.button_close_caption);
            }
        }
    }
}
