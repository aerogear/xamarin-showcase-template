using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AeroGear.Mobile.Core;
using AeroGear.Mobile.Security;
using Example.Security;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecurityCheckPage : ContentPage, INotifyPropertyChanged
    {
        private const int SCORE_THRESHOLD = 70;
        private const int TRUST_SCORE_DECIMAL_PLACE_COUNT = 0;
        private const decimal TRUST_SCORE_INITIAL_PERCENTAGE = 0;

        private ObservableCollection<SecurityCheckResultDecorator> securityCheckResults = new ObservableCollection<SecurityCheckResultDecorator>();
        private decimal trustScore = TRUST_SCORE_INITIAL_PERCENTAGE;


        public ObservableCollection<SecurityCheckResultDecorator> CheckResults
        {
            get { return this.securityCheckResults; }
        }
        public decimal TrustScore
        {
            get { return this.trustScore; }
        }

        public SecurityCheckPage()
        {
            InitializeComponent();
            BindingContext = this;

            ReportCheckResults(DependencyService.Get<ISecurityCheckProvider>().SecurityChecks);
        }

        private void ReportCheckResults(ICollection<ISecurityCheck> securityChecks)
        {
            securityCheckResults.Clear();
            var securityService = MobileCore.Instance.GetService<ISecurityService>();
            foreach (var check in securityChecks)
            {
                var checkResult = securityService.Check(check);
                // Convert to a security check result report to include UI specific information.
                securityCheckResults.Add((SecurityCheckResultDecorator)checkResult);
            }

            this.trustScore = CalculateTrustScore(new List<SecurityCheckResultDecorator>(securityCheckResults));
            NotifyPropertyChanged(nameof(TrustScore));

            if (this.trustScore < SCORE_THRESHOLD)
            {
                ShowDeviceInsecureAlert();    
            }
        }

        private void ShowDeviceInsecureAlert()
        {
            var unsecureWarning = DependencyService.Get<IUnsecureAlertWarning>();
            unsecureWarning.Show(Convert.ToInt32(this.trustScore), SCORE_THRESHOLD);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal CalculateTrustScore(List<SecurityCheckResultDecorator> results)
        {
            int failedResultCount = results.FindAll(result => !result.IsSecure).Count;
            return 100 - Math.Round(Decimal.Divide(failedResultCount, results.Count) * 100, TRUST_SCORE_DECIMAL_PLACE_COUNT);
        }

        void OnRefreshClicked(object sender, System.EventArgs e)
        {
            ReportCheckResults(DependencyService.Get<ISecurityCheckProvider>().SecurityChecks);
        }
    }
}