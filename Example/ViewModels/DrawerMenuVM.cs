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
    public enum NavigationItemIds 
    { 
        HOME,
        IDMGT, 
        IDMGT_DOC, 
        AUTH, 
        SSO, 
        DEV_SEC, 
        DEV_SEC_DOC, 
        DEV_TRUST, 
        SEC_STORAGE, 
        CERT_PINNING, 
        PUSH, 
        PUSH_DOC, 
        PUSH_REG, 
        PUSH_MSG, 
        METRICS, 
        METRICS_DIC, 
        DEV_PROF_INFO, 
        TRUST_CHECK_INFO 
    };

    public class DrawerMenuVM : BaseVM
    {
        public Dictionary<NavigationItemIds, NavigationItem> Items = new Dictionary<NavigationItemIds, NavigationItem>();
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public DrawerMenuVM()
        {
            Items[NavigationItemIds.HOME] = NavigationItem.HOME_PAGE;
            Items[NavigationItemIds.IDMGT] = new DescriptionNavigationItem { Id= (int) NavigationItemIds.IDMGT, Title=StringResources.NavIdtMgmt,Icon=ResourceUtils.GetSvg("ic_account_circle"),TextContent=StringResources.IdtManagementDesc+StringResources.FurtherInformation};
            Items[NavigationItemIds.IDMGT_DOC] = new WebNavigationItem { Id = (int)NavigationItemIds.IDMGT_DOC, Title = StringResources.NavDocumentation, SubItem = true, Source = StringResources.idm_doc_link };
            Items[NavigationItemIds.AUTH] = new AuthNavigationItem { Id = (int)NavigationItemIds.AUTH, Title = StringResources.NavAuth, TargetType = typeof(AuthPage), SubItem = true };
            Items[NavigationItemIds.SSO] = new WebNavigationItem { Id = (int)NavigationItemIds.SSO, Title = StringResources.NavSSO, SubItem = true, Source = StringResources.idm_sso_doc_link };
            Items[NavigationItemIds.DEV_SEC] = new DescriptionNavigationItem { Id= (int)NavigationItemIds.DEV_SEC, Title=StringResources.NavDevSec,Icon=ResourceUtils.GetSvg("ic_security"),TextContent=StringResources.DeviceSecurityDesc+StringResources.FurtherInformation };
            Items[NavigationItemIds.DEV_SEC_DOC] = new WebNavigationItem { Id = (int)NavigationItemIds.DEV_SEC_DOC, Title = StringResources.NavDocumentation, SubItem = true, Source = StringResources.device_security_doc_link };
            Items[NavigationItemIds.DEV_TRUST] = new NavigationItem { Id = (int)NavigationItemIds.DEV_TRUST, Title = StringResources.NavDeviceTrust, TargetType = typeof(SecurityCheckPage), SubItem = true };
            Items[NavigationItemIds.SEC_STORAGE] = CreateUnderConstructionItem((int)NavigationItemIds.SEC_STORAGE, StringResources.NavSecStorage);
            Items[NavigationItemIds.CERT_PINNING] = CreateUnderConstructionItem((int) NavigationItemIds.CERT_PINNING, StringResources.NavCertPinning);
            Items[NavigationItemIds.PUSH] = new DescriptionNavigationItem { Id=(int)NavigationItemIds.PUSH, Title=StringResources.NavPush,Icon=ResourceUtils.GetSvg("ic_notifications_active"),TextContent=StringResources.PushNotificationsDesc+StringResources.FurtherInformation};
            Items[NavigationItemIds.PUSH_DOC] = new WebNavigationItem { Id = (int)NavigationItemIds.PUSH_DOC, Title = StringResources.NavDocumentation, SubItem = true, Source = StringResources.push_doc_link };
            Items[NavigationItemIds.PUSH_REG] = CreateUnderConstructionItem((int)NavigationItemIds.PUSH_REG, StringResources.NavDevReg);
            Items[NavigationItemIds.PUSH_MSG] = CreateUnderConstructionItem((int)NavigationItemIds.PUSH_MSG, StringResources.NavPushMsg);
            Items[NavigationItemIds.METRICS] = new DescriptionNavigationItem { Id=(int)NavigationItemIds.METRICS, Title=StringResources.NavMetrics,Icon=ResourceUtils.GetSvg("ic_insert_chart"),TextContent=StringResources.MobileMetricsDesc+StringResources.FurtherInformation };
            Items[NavigationItemIds.METRICS_DIC] = new WebNavigationItem { Id = (int)NavigationItemIds.METRICS_DIC, Title = StringResources.NavDocumentation, SubItem = true, Source = StringResources.metrics_doc_link };
            Items[NavigationItemIds.DEV_PROF_INFO] = CreateUnderConstructionItem((int) NavigationItemIds.DEV_PROF_INFO, StringResources.NavDeviceProfileInfo);
            Items[NavigationItemIds.TRUST_CHECK_INFO] = CreateUnderConstructionItem((int)NavigationItemIds.TRUST_CHECK_INFO, StringResources.NavTrustCheckInfo);

            NavigationItems = new ObservableCollection<NavigationItem>(Items.Values);
        }
        /// <summary>
        /// Creates under construction navigation item.
        /// </summary>
        /// <param name="id"></param> id of the neavigation item.
        /// <param name="title"></param> title of the navigation item
        /// <returns></returns>
        private NavigationItem CreateUnderConstructionItem(int id, string title) => new DescriptionNavigationItem { Id = id, Title = title, SubItem = true, Subtitle = StringResources.UnderConstructionTitle, TextContent = StringResources.UnderConstruction };
    }
}
