using System;
using System.Collections.Generic;
using AeroGear.Mobile.Auth.Authenticator;
using Example.Security;
using AeroGear.Mobile.Security;

[assembly: Xamarin.Forms.Dependency(typeof(Example.Android.Security.SecurityCheckProvider))]
namespace Example.Android.Security
{
    public class SecurityCheckProvider : ISecurityCheckProvider
    {
        public SecurityCheckProvider()
        {
        }

        public ICollection<ISecurityCheckType> SecurityChecks
        {
            get
            {
                return (ICollection<ISecurityCheckType>)AeroGear.Mobile.Security.SecurityChecks.GetAllChecks();
            }
        }
    }
}
