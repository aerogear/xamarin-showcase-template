using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using AeroGear.Mobile.Core;
using AeroGear.Mobile.Security;
using Example.Security;
using Example.ViewModels.Security;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecurityCheckPage : ContentPage, INotifyPropertyChanged
    {
        private readonly static int TRUST_SCORE_DECIMAL_PLACE_COUNT = 2;
        private readonly static decimal TRUST_SCORE_INITIAL_PERCENTAGE = 0;

        private ObservableCollection<SecurityCheckResultReport> securityCheckResults = new ObservableCollection<SecurityCheckResultReport>();
        private decimal trustScore = TRUST_SCORE_INITIAL_PERCENTAGE;

        public ObservableCollection<SecurityCheckResultReport> CheckResults
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

            var securityChecks = DependencyService.Get<ISecurityCheckProvider>().SecurityChecks;
            ReportCheckResults(securityChecks);
        }

        private void ReportCheckResults(IList<ISecurityCheckType> securityChecks)
        {
            var securityService = MobileCore.Instance.GetService<ISecurityService>();
            foreach (var check in securityChecks)
            {
                var checkResult = securityService.Check(check);
                // Convert to a security check result report to include UI specific information.
                securityCheckResults.Add(new SecurityCheckResultReport(checkResult));
            }

            this.trustScore = CalculateTrustScore(new List<SecurityCheckResultReport>(securityCheckResults));
            NotifyPropertyChanged(nameof(TrustScore));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal CalculateTrustScore(List<SecurityCheckResultReport> results)
        {
            int failedResultCount = results.FindAll(result => !result.Passed).Count;
            return 100 - Math.Round(Decimal.Divide(failedResultCount, results.Count) * 100, TRUST_SCORE_DECIMAL_PLACE_COUNT);
        }
    }
}