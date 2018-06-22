using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Example.Resources;
using Example.Utils;
using Example.Views.Pages;
using Xamarin.Forms;

namespace Example.Models
{
    public class NavigationItem : INotifyPropertyChanged
    {

        /// <summary>
        /// Home page default. (first page displayed)
        /// </summary>
        public static NavigationItem HOME_PAGE
        {
            get
            {
                return _HOME_PAGE.Value;
            }
        }

        private static Lazy<NavigationItem> _HOME_PAGE = new Lazy<NavigationItem>(() =>
        {
            var item = new DescriptionNavigationItem();
            item.Id = 0;
            item.Title = StringResources.NavHome;
            item.PageTitle = StringResources.AppName;
            item.Icon = ResourceUtils.GetSvg("ic_home");
            item.Selected = true;
            item.TargetType = typeof(DescriptionNavigationItem);
            item.Subtitle = "";
            item.TextContent = StringResources.HomeWelcome+StringResources.FurtherInformation;
            return item;
        });


        public NavigationItem()
        {

        }
        /// <summary>
        /// Numerical identifier of the navigation item.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the navigation item as it appears in navigation drawer. If a <see cref="PageTitle"/> is not specified, it's used as the page title as well.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title of the navigation item as it appears in the window's toolbar.
        /// </summary>
        public string PageTitle
        {
            get => _pageTitle != null ? _pageTitle : Title;
            set => _pageTitle = value;
        }

        /// <summary>
        /// Icon of the navigation item.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Determines if the navigation item is a sub-item (part of a larger group of items). true= sub-item, false= group-item
        /// </summary>
        public bool SubItem { get; set; }

        /// <summary>
        /// Determines if item is selectable (clickable). Read-only.
        /// </summary>
        public bool Selectable
        {
            get => TargetType != null;
        }

        /// <summary>
        /// Gets/sets if item is selected.
        /// </summary>
        public bool Selected
        {
            get => selected;
            set
            {
                if (selected != value && Selectable)
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                }
                OnPropertyChanged("SelectedColor");
            }
        }
        private bool selected;
        private string _pageTitle;

        /// <summary>
        /// Links to the <see cref="Type"/> of the page that is represented by the item.
        /// </summary>
        public Type TargetType { protected get; set; }


        public virtual Page Page => (Page)Activator.CreateInstance(this.TargetType);

        /// <summary>
        /// Returns color that is used for rentering the item in the navigation drawer.
        /// </summary>
        public Color SelectedColor
        {
            get=>(Color)Application.Current.Resources[selected ? "Accent" : "PrimaryTextColor"];
        }

        /// <summary>
        /// Returns <see cref="Xamarin.Forms.FontAttributes"/> that are used for rendereing  the item in the navigation drawer.
        /// </summary>
        public FontAttributes FontAttributes {
            get => SubItem ? FontAttributes.None : FontAttributes.Bold;
        }

        
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}