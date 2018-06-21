using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Example.Models;
using Example.Resources;
using Example.Utils;
using Example.ViewModels.Base;
using Example.Views.Pages;
using static Example.Views.Pages.LandingPage;

namespace Example.ViewModels
{
    public class DrawerMenuVM : BaseVM
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public DrawerMenuVM() => NavigationItems = new ObservableCollection<NavigationItem>(new[]
            {
                NavigationItem.HOME_PAGE,
                new DescriptionNavigationItem { Id=1, Title=StringResources.NavIdtMgmt,Icon=ResourceUtils.GetSvg("ic_account_circle"),Subtitle=StringResources.IdtMangement,TextContent=StringResources.IdtManagementDesc},
                new WebNavigationItem { Id=10, Title=StringResources.NavDocumentation,SubItem=true, Source=StringResources.idm_doc_link},
                new NavigationItem { Id = 11, Title = StringResources.NavAuth, TargetType=typeof(AuthPage),SubItem=true},
                new WebNavigationItem { Id=12, Title=StringResources.NavSSO,SubItem=true, Source=StringResources.idm_sso_doc_link},
                new DescriptionNavigationItem { Id=2, Title=StringResources.NavDevSec,Icon=ResourceUtils.GetSvg("ic_security"),Subtitle=StringResources.DeviceSecurity,TextContent=StringResources.DeviceSecurityDesc },
                new WebNavigationItem { Id=20, Title=StringResources.NavDocumentation,SubItem=true, Source=StringResources.device_security_doc_link},
                new NavigationItem {Id = 21, Title = StringResources.NavDeviceTrust, TargetType=typeof(SecurityCheckPage), SubItem=true },
                new NavigationItem { Id = 22, Title =StringResources.NavSecStorage, TargetType=typeof(LandingPage),SubItem=true},
                new NavigationItem { Id = 23, Title =StringResources.NavCertPinning, TargetType=typeof(LandingPage),SubItem=true},
                new DescriptionNavigationItem { Id=3, Title=StringResources.NavPush,Icon=ResourceUtils.GetSvg("ic_notifications_active"),Subtitle=StringResources.PushNotifications,TextContent=StringResources.PushNotificationsDesc },
                new WebNavigationItem { Id = 31, Title =StringResources.NavDocumentation, SubItem=true, Source=StringResources.push_doc_link},
                CreateUnderConstructionItem (32,StringResources.NavDevReg),
                CreateUnderConstructionItem (33, StringResources.NavPushMsg),
                new DescriptionNavigationItem { Id=4, Title=StringResources.NavMetrics,Icon=ResourceUtils.GetSvg("ic_insert_chart"),Subtitle=StringResources.MobileMetrics,TextContent=StringResources.MobileMetricsDesc },
                new WebNavigationItem { Id = 41, Title =StringResources.NavDocumentation, SubItem=true, Source=StringResources.metrics_doc_link},
                CreateUnderConstructionItem (42, StringResources.NavDeviceProfileInfo),
                CreateUnderConstructionItem(43, StringResources.NavTrustCheckInfo)

            });

        /// <summary>
        /// Creates under construction navigation item.
        /// </summary>
        /// <param name="id"></param> id of the neavigation item.
        /// <param name="title"></param> title of the navigation item
        /// <returns></returns>
        private NavigationItem CreateUnderConstructionItem(int id, string title) => new DescriptionNavigationItem { Id = id, Title = title, SubItem = true, Subtitle = StringResources.UnderConstructionTitle, TextContent = StringResources.UnderConstruction };
    }
}
