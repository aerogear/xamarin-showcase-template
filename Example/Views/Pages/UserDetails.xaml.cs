using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AeroGear.Mobile.Auth;
using AeroGear.Mobile.Core;
using Xamarin.Forms;

namespace Example.Views.Pages
{
    public partial class UserDetails : ContentPage, INotifyPropertyChanged
    {
        private User user;
        private ObservableCollection<UserRole> userRoles = new ObservableCollection<UserRole>();

        public UserDetails()
        {
            this.user = MobileCore.Instance.GetService<IAuthService>().CurrentUser();
            userRoles = new ObservableCollection<UserRole>(this.user.Roles);
            //NotifyPropertyChanged(nameof(AssignedRoles));
            InitializeComponent();
            BindingContext = this;
            //NotifyPropertyChanged(nameof(AssignedRoles));
            //InitListView();
        }

        //private void InitListView()
        //{
        //    var user = MobileCore.Instance.GetService<IAuthService>().CurrentUser();
        //    var roleItems = new List<string> { };
        //    foreach (var role in user.getRoles())
        //    {
        //        roleItems.Add(role.ToString());
        //    }

        //    lstRoles.ItemsSource = roleItems;
        //    //username.Text = user.Username;
        //    //email.Text = user.Email;
        //}

        public User CurrentUser => user;

        public ICollection<UserRole> AssignedRoles 
        {
            get
            {
                return userRoles;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void onLogoutClicked(object sender, System.EventArgs e)
        {
            var authService = MobileCore.Instance.GetService<IAuthService>();
            authService.Logout(authService.CurrentUser()).ContinueWith(result =>
            {
                Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AuthPage()));
            });
        }
    }
}
