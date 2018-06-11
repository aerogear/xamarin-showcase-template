using System;
using Example.Security;

[assembly: Xamarin.Forms.Dependency(typeof(Example.Android.Security.UnsecureAlertWarning))]
namespace Example.Android.Security
{
    public class UnsecureAlertWarning : AbstractUnsecureAlertWarning
    {
        public UnsecureAlertWarning()
        {
        }

        protected override void Exit()
        {
            System.Threading.Thread.CurrentThread.Abort();;
        }
    }
}
