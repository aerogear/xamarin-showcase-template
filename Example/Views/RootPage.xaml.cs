using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models;
using Example.Resources;
using Example.ViewModels;
using Example.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        private NavigationItem oldSelectedItem;

        public RootPage()
        {
            InitializeComponent();
            DrawerMenuPage.ListView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item != null)
                {
                    var item = e.Item as NavigationItem;
                    if (!item.Selectable) DrawerMenuPage.ListView.SelectedItem = null;
                }
            };
           DrawerMenuPage.ListView.ItemSelected += ListView_ItemSelected;
            DrawerMenuPage.ListView.SelectedItem=NavigationItem.HOME_PAGE;
        }

        /// <summary>
        /// Changes the current page to the new based by menu item.
        /// </summary>
        /// <param name="item">menu item</param>
        private void ChangePage(NavigationItem item) {
            if (item == null || !item.Selectable || item==oldSelectedItem)
                return;

            var page = item.Page;

            if (page == null)
            {
                IsPresented = false;
                DrawerMenuPage.ListView.SelectedItem = oldSelectedItem;
                return;
            }
            if (String.IsNullOrEmpty(page.Title)) page.Title = item.PageTitle;
            Detail = new NavigationPage(page);
            IsPresented = false;
            item.Selected = true;
            if (oldSelectedItem!=null) oldSelectedItem.Selected = false;
            oldSelectedItem = item;
        }

        public void MoveTo(NavigationItemIds navItemId)
        {
            DrawerMenuPage.ListView.SelectedItem = DrawerMenuPage.GetItem(navItemId);
        }

        internal void ChangePage(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;
        }

        /// <summary>
        /// Replaces page when item is selected in the drawer menu.
        /// </summary>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ChangePage(e.SelectedItem as NavigationItem);
        }
    }
}