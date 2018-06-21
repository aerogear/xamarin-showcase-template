using System;
using Example.Models.Constraints;
using Example.Resources;
using Example.ViewModels;
using Example.Views.Pages;
using Xamarin.Forms;

namespace Example.Models
{
    /// <summary>
    /// Navigation item for the Authentication service.
    /// </summary>
    public class AuthNavigationItem : ConstrainedNavigationItem
    {
        public String RequiredConfiguration;

        public AuthNavigationItem() : base(new HasConfigurationContraint(StringResources.service_auth_name, "keycloak", NavigationItemIds.IDMGT_DOC))
        {
            this.TargetType = typeof(AuthPage);
        }

        protected override Page BuildPage()
        {
            return new AuthPage();
        }
    }
}
