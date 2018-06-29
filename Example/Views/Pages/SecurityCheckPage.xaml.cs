using System;
using System.Collections.Generic;
using System.ComponentModel;
using AeroGear.Mobile.Core;
using AeroGear.Mobile.Core.Metrics;
using AeroGear.Mobile.Security;
using AeroGear.Mobile.Security.Executors;
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

        private ICollection<SecurityCheckResult> securityCheckResults;
        private decimal trustScore = TRUST_SCORE_INITIAL_PERCENTAGE;

        private int passedChecksCount = 0;
        private int totalChecksCount = 0;

        private MetricsService metricsService;

        public string ResultMessage => String.Format("({0} out of {1} checks passing)", passedChecksCount, totalChecksCount);

        public ICollection<SecurityCheckResult> CheckResults => this.securityCheckResults;

        public decimal TrustScore => this.trustScore;

        public SecurityCheckPage()
        {
            MobileCore core = MobileCore.Instance;

            if (core.GetFirstServiceConfigurationByType("metrics") != null)
            {
                this.metricsService = core.GetService<MetricsService>();
            }

            InitializeComponent();
            BindingContext = this;

            ReportCheckResults(DependencyService.Get<ISecurityCheckProvider>().SecurityChecks);
        }

        private void ReportCheckResults(ICollection<ISecurityCheck> securityChecks)
        {
            securityCheckResults = SecurityCheckExecutor.newSyncExecutor().WithSecurityCheck(securityChecks).WithMetricsService(this.metricsService).Build().Execute().Values;

            this.trustScore = CalculateTrustScore(securityCheckResults);
            NotifyPropertyChanged(nameof(CheckResults));
            NotifyPropertyChanged(nameof(TrustScore));
            NotifyPropertyChanged(nameof(ResultMessage));

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

        private decimal CalculateTrustScore(ICollection<SecurityCheckResult> results)
        {
            int failedResultCount = 0;

            foreach(SecurityCheckResult result in results)
            {
                failedResultCount += ((SecurityCheckResultDecorator)result).IsSecure ? 0 : 1;
            }

            this.totalChecksCount = results.Count;
            this.passedChecksCount = totalChecksCount - failedResultCount;

            return 100 - Math.Round(Decimal.Divide(failedResultCount, this.totalChecksCount) * 100, TRUST_SCORE_DECIMAL_PLACE_COUNT);
        }

        void OnRefreshClicked(object sender, System.EventArgs e)
        {
            ReportCheckResults(DependencyService.Get<ISecurityCheckProvider>().SecurityChecks);
        }
    }
}