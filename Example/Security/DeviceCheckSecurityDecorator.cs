using System;
using AeroGear.Mobile.Security;
using Xamarin.Forms;

namespace Example.Security
{
    public class DeviceCheckSecurityDecorator : IDeviceCheck
    {
        private readonly IDeviceCheck securityCheck;
        private readonly bool secureWhenFailed;
        private readonly string secureMessage;
        private readonly string unsecureMessage;

        public DeviceCheckSecurityDecorator(IDeviceCheck securityCheck, string secureMessage, string unsecureMessage, bool secureWhenFailed = false)
        {
            this.securityCheck = securityCheck;
            this.secureWhenFailed = secureWhenFailed;
            this.secureMessage = secureMessage;
            this.unsecureMessage = unsecureMessage;
        }

        public string GetId() => securityCheck.GetId();

        public string GetName() => securityCheck.GetName();

        public DeviceCheckResult Check()
        {
            return new DeviceCheckResultDecorator(this, securityCheck.Check(), secureMessage, unsecureMessage, secureWhenFailed);
        }
    }

    public class DeviceCheckResultDecorator : DeviceCheckResult
    {
        public readonly bool IsSecure;
        private readonly string secureMessage;
        private readonly string unsecureMessage;
        private readonly DeviceCheckResult result;

        public DeviceCheckResultDecorator(IDeviceCheck check, DeviceCheckResult result, string secureMessage, string unsecureMessage, bool secureWhenFailed) : base(check, result.Passed)
        {
            IsSecure = secureWhenFailed ? !result.Passed : result.Passed;
            this.secureMessage = secureMessage;
            this.unsecureMessage = unsecureMessage;
            this.result = result;
        }
        
        public string Message => IsSecure? secureMessage : unsecureMessage;

        public Color DisplayColor => IsSecure ? Color.Green : Color.Red;

        public string Icon => IsSecure ? "ic_passed" : "ic_warning";
    }   
}
