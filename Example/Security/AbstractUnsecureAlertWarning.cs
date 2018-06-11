using System;
namespace Example.Security
{
    public abstract class AbstractUnsecureAlertWarning : IUnsecureAlertWarning
    {
        public AbstractUnsecureAlertWarning()
        {
        }

        protected abstract void Exit();

        private async void showAlert()
        {
            var exit = await App.Current.MainPage.DisplayAlert("Warning", "Your device is not secure, do you want to continue?", "EXIT", "CONTINUE");
            if (exit)
            {
                Exit();
            }
        }

        public void Show()
        {
            showAlert();
        }
    }
}
