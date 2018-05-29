using System;
using System.Collections.Generic;
using AeroGear.Mobile.Security;
using Example.Security;

[assembly: Xamarin.Forms.Dependency(typeof(Example.iOS.Security.SecurityCheckProvider))]
namespace Example.iOS.Security
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
