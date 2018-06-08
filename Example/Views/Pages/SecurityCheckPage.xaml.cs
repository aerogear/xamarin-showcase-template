using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using AeroGear.Mobile.Core;
using AeroGear.Mobile.Core.Utils;
using AeroGear.Mobile.Security;
using Example.Security;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecurityCheckPage : ContentPage, INotifyPropertyChanged
    {
        private readonly static int TRUST_SCORE_DECIMAL_PLACE_COUNT = 0;
        private readonly static decimal TRUST_SCORE_INITIAL_PERCENTAGE = 0;

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

            //var securityChecks = DependencyService.Get<ISecurityCheckProvider>().SecurityChecks;


            List<ISecurityCheck> securityChecks = new List<ISecurityCheck>();

            // Configure securitychecks list
            if (Device.RuntimePlatform == Device.iOS)
            {
                securityChecks = getIOSChecks();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                securityChecks = getAndroidChecks();
            }

            ReportCheckResults(securityChecks);
        }

        private List<ISecurityCheck> getIOSChecks()
        {
            ISecurityCheckFactory securityCheckFactory = ServiceFinder.Resolve<ISecurityCheckFactory>();
            var securityChecks = new List<ISecurityCheck>();

            // Configure securitychecks list
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("DeviceLockCheck"),
                    "Device Lock Enabled",
                    "No Device Lock Enabled"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NoDebuggerCheck"),
                    "No Debugger Detected",
                    "Debugger Detected"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NonJailbrokenCheck"),
                    "No Jailbreak Detected",
                    "Jailbreak Detected"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NotInEmulatorCheck"),
                    "No Emulator Access Detected",
                    "Emulator Access Detected"
                ));

            return securityChecks;
        }

        private List<ISecurityCheck> getAndroidChecks()
        {
            ISecurityCheckFactory securityCheckFactory = ServiceFinder.Resolve<ISecurityCheckFactory>();
            var securityChecks = new List<ISecurityCheck>();

            // Configure securitychecks list
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("BackupDisallowedCheck"),
                    "App Data Backup Recovery Disabled",
                    "App Data Backup Recovery Enabled"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("DeveloperModeDisabledCheck"),
                    "Developer Options Disabled",
                    "Developer Options Enabled"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("EncryptionCheck"),
                    "Device Encryption Active",
                    "Device Encryption Inactive"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NoDebuggerCheck"),
                    "No Debugger Detected",
                    "Debugger Detected"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NonRootedCheck"),
                    "No Root Access Detected",
                    "Root Access Detected"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("NotInEmulatorCheck"),
                    "No Emulator Access Detected",
                    "Emulator Access Detected"
                ));
            securityChecks.Add(
                new DeviceCheckSecurityDecorator(
                    securityCheckFactory.create("ScreenLockCheck"),
                    "Device Lock Enabled",
                    "No Device Lock Enabled"
                ));

            return securityChecks;
        }

        private void ReportCheckResults(ICollection<ISecurityCheck> securityChecks)
        {
            var securityService = MobileCore.Instance.GetService<ISecurityService>();
            foreach (var check in securityChecks)
            {
                var checkResult = securityService.Check(check);
                // Convert to a security check result report to include UI specific information.
                securityCheckResults.Add((SecurityCheckResultDecorator)checkResult);
            }

            this.trustScore = CalculateTrustScore(new List<SecurityCheckResultDecorator>(securityCheckResults));
            NotifyPropertyChanged(nameof(TrustScore));
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
    }
}