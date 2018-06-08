using System;
using AeroGear.Mobile.Security;
using Xamarin.Forms;

namespace Example.Security
{
    internal class DeviceCheckSecurityDecorator : ISecurityCheck
    {
        private readonly ISecurityCheck securityCheck;
        private readonly bool secureWhenFailed;
        private readonly string secureMessage;
        private readonly string unsecureMessage;

        public DeviceCheckSecurityDecorator(ISecurityCheck securityCheck, string secureMessage, string unsecureMessage, bool secureWhenFailed = false)
        {
            this.securityCheck = securityCheck;
            this.secureWhenFailed = secureWhenFailed;
            this.secureMessage = secureMessage;
            this.unsecureMessage = unsecureMessage;
        }

        public string GetId() => securityCheck.GetId();

        public string GetName() => securityCheck.GetName();

        public SecurityCheckResult Check()
        {
            return new SecurityCheckResultDecorator(securityCheck.Check(), secureMessage, unsecureMessage, secureWhenFailed);
        }
    }

    public class SecurityCheckResultDecorator : SecurityCheckResult
    {
        public readonly bool IsSecure;
        private readonly string secureMessage;
        private readonly string unsecureMessage;
        private readonly SecurityCheckResult result;

        public SecurityCheckResultDecorator(SecurityCheckResult result, string secureMessage, string unsecureMessage, bool secureWhenFailed)
        {
            IsSecure = secureWhenFailed ? !result.Passed : result.Passed;
            this.secureMessage = secureMessage;
            this.unsecureMessage = unsecureMessage;
            this.result = result;
        }
        
        public string Message => IsSecure? secureMessage : unsecureMessage;

        public Color DisplayColor => IsSecure ? Color.Green : Color.Red;
    }   
}
