using System;
using AeroGear.Mobile.Security;
using Xamarin.Forms;

namespace Example.ViewModels.Security
{
    public class SecurityCheckResultReport
    {
        public SecurityCheckResult Result { get; }
        public bool Passed {
            get {
                return this.Result.Passed;
            }
        }
        public string Name {
            get {
                return this.Result.Name;
            }
        }
        public Color DisplayColor {
            get {
                return this.Passed ? Color.Green : Color.Red;
            }
        }

        public SecurityCheckResultReport(SecurityCheckResult result)
        {
            Result = result;
        }
    }
}

