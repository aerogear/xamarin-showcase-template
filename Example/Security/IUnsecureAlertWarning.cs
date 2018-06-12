using System;
namespace Example.Security
{
    public interface IUnsecureAlertWarning
    {
        void Show(int currentValue, int threshold);
    }
}
