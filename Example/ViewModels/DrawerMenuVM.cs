using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Example.Models;
using Example.Resources;
using Example.Utils;
using Example.ViewModels.Base;
using Example.Views.Pages;

namespace Example.ViewModels
{
    public class DrawerMenuVM : BaseVM
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public DrawerMenuVM()
        {
            NavigationItems = new ObservableCollection<NavigationItem>(new[]
            {
                NavigationItem.HOME_PAGE,
                new NavigationItem { Id=1, Title=StringResources.NavIdtMgmt,Icon=ResourceUtils.GetSvg("ic_account_circle")},
                new WebNavigationItem { Id=10, Title=StringResources.NavDocumentation,SubItem=true, Source=StringResources.idm_doc_link},
                new NavigationItem { Id = 11, Title = StringResources.NavAuth, TargetType=typeof(AuthPage),SubItem=true},
                new WebNavigationItem { Id=12, Title=StringResources.NavSSO,SubItem=true, Source=StringResources.idm_sso_doc_link},
                new NavigationItem { Id=2, Title=StringResources.NavDevSec,Icon=ResourceUtils.GetSvg("ic_security") },
                new WebNavigationItem { Id=20, Title=StringResources.NavDocumentation,SubItem=true, Source=StringResources.device_security_doc_link},
                new NavigationItem {Id = 21, Title = StringResources.NavDeviceTrust, TargetType=typeof(SecurityCheckPage), SubItem=true },
                new NavigationItem { Id = 22, Title =StringResources.NavSecStorage, TargetType=typeof(UnderConstruction),SubItem=true},
                new NavigationItem { Id = 23, Title =StringResources.NavCertPinning, TargetType=typeof(UnderConstruction),SubItem=true},
                new NavigationItem { Id=3, Title=StringResources.NavPush,Icon=ResourceUtils.GetSvg("ic_notifications_active") },
                new WebNavigationItem { Id = 31, Title =StringResources.NavDocumentation, SubItem=true, Source=StringResources.push_doc_link},
                new NavigationItem { Id = 32, Title =StringResources.NavDevReg, TargetType=typeof(UnderConstruction),SubItem=true},
                new NavigationItem { Id = 33, Title =StringResources.NavPushMsg, TargetType=typeof(UnderConstruction),SubItem=true},
                new NavigationItem { Id=4, Title=StringResources.NavMetrics,Icon=ResourceUtils.GetSvg("ic_insert_chart") },
                new WebNavigationItem { Id = 41, Title =StringResources.NavDocumentation, SubItem=true, Source=StringResources.metrics_doc_link},
                new NavigationItem { Id = 42, Title =StringResources.NavDeviceProfileInfo, TargetType=typeof(UnderConstruction),SubItem=true},
                new NavigationItem { Id = 43, Title =StringResources.NavTrustCheckInfo, TargetType=typeof(UnderConstruction),SubItem=true},

            });
        }
    }
}
