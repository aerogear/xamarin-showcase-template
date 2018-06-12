using System;
using Android.OS;
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
            Process.KillProcess(Process.MyPid());
        }
    }
}
