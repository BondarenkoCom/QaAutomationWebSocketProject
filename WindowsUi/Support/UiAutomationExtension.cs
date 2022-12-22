using System;
using Constants.Windows;
using FlaUI.Core.Tools;
using NUnit.Framework;

namespace AWindowsUi.Support
{
    public class UiAutomationExtension
    {
        public static T WaitForElement<T>(Func<T> getter)
        {
            var retry = Retry.WhileNull<T>(getter, Ui.BigWaitTimeout);

            if (!retry.Success)
            {
                Assert.Fail("Failed to get an element within a wait timeout");
            }

            return retry.Result;
        }
    }
}