using System;

using Example.Views;
using Xamarin.Forms;
using AeroGear.Mobile.Core;
using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using AeroGear.Mobile.Core.Configuration;
using Example.Views.Pages;
using Example.Resources;

namespace Example
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new RootPage();
        }

		protected override void OnStart ()
		{
            validateCert();
		}

        public async void validateCert()
        {
            try 
            {
                string url;

                MobileCoreConfiguration configuration = MobileCore.Instance.Configuration;

                IEnumerator<ServiceConfiguration> serviceConfigurations = configuration.ServiceConfigurations.GetEnumerator();

                if (serviceConfigurations.MoveNext())
                {
                    url = serviceConfigurations.Current.Url;
                }
                else
                {
                    url = configuration.ClusterName;
                }

                HttpWebRequest webRequest = WebRequest.CreateHttp(url);
                webRequest.ServerCertificateValidationCallback += ValidateCertificate;
                webRequest.Method = HttpMethod.Options.ToString();

                await webRequest.GetResponseAsync();
            }
            catch 
            {
                // ignore exceptions
            }

        }

        private bool ValidateCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                DisplayWarning();
            }
            return true;
        }

        private void DisplayWarning()
        {
            Device.BeginInvokeOnMainThread(async () => {
                if (await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    StringResources.dialog_certificate_error_title,
                    StringResources.dialog_certificate_error_message,
                    StringResources.button_showdocs_caption,
                    StringResources.button_close_caption))
                {
                    ((RootPage)(Current.MainPage)).ChangePage(new WebPage(StringResources.ssl_selfsigned_doc_link, StringResources.NavDocumentation));
                }
            });
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
