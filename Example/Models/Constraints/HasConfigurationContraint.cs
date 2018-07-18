using System;
using AeroGear.Mobile.Core;
using Example.Resources;
using Example.ViewModels;
using Example.Views;

namespace Example.Models.Constraints
{
    /// <summary>
    /// This constraint checks if a specific configuration is present.
    /// If it is not, a message is visualised and the user is proposed to look at the service configuration page.
    /// </summary>
    public class HasConfigurationContraint : IConstraint
    {
        private readonly string serviceName;
        private readonly string serviceType;
        private readonly NavigationItemIds serviceDocPage;

        private const string msg = "The service {0} does not have a configuration in mobile-services.json. Refer to the documentation for instructions on how to configure this service.";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Example.Models.Constraints.HasConfigurationContraint"/> class.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <param name="serviceType">Service type.</param>
        /// <param name="serviceDocPage">Service documentation page item.</param>
        public HasConfigurationContraint(string serviceName, string serviceType, NavigationItemIds serviceDocPage)
        {
            this.serviceName = serviceName;
            this.serviceType = serviceType;
            this.serviceDocPage = serviceDocPage;
        }

        /// <summary>
        /// If the configuration is present, returns true, otherwise it visualises the popup asking if documentation is desired.
        /// </summary>
        /// <returns>true if the configuration is present</returns>
        public bool Check()
        {
            if (MobileCore.Instance.GetServiceConfigurationsByType(serviceType).Length == 0)
            {
                DisplayAlert();
                return false;
            }

            return true;
        }

        private async void DisplayAlert()
        {
            RootPage Root = (RootPage)(Xamarin.Forms.Application.Current.MainPage);
            if (await Root.DisplayAlert(
                StringResources.dialog_notconfigured_title,
                String.Format(StringResources.dialog_notconfigured_message, serviceName),
                StringResources.button_showdocs_caption,
                StringResources.button_close_caption))
            {
                Root.MoveTo(serviceDocPage);
            }
        }
    }
}
