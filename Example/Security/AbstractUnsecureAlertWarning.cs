using System;
using Example.Resources;

namespace Example.Security
{
    public abstract class AbstractUnsecureAlertWarning : IUnsecureAlertWarning
    {
        public AbstractUnsecureAlertWarning()
        {
        }

        protected abstract void Exit();

        private async void ShowAlert(int currentValue, int threshold)
        {
            if (await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                SecurityResources.warning_unsecure_title, 
                String.Format(SecurityResources.warning_unsecure_message, currentValue, threshold), 
                SecurityResources.button_exit, 
                SecurityResources.button_continue))
            {
                Exit();
            }
        }

        public void Show(int currentValue, int threshold)
        {
            ShowAlert(currentValue, threshold);
        }
    }
}
