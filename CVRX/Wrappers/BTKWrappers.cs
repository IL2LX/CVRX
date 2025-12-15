using System;

namespace CVRX.Wrappers
{
    internal class BTKWrappers
    {
        public static void ShowConfirm(string title, string content, Action onYes, Action onNo = null, string yesText = "Yes", string noText = "No")
        {
            BTKUILib.QuickMenuAPI.ShowConfirm(title, content, () =>
            {
                onYes.Invoke();
            }, () =>
            {
                onNo.Invoke();
            }, yesText, noText);
        }
        public static void ShowAlertToast(string message, int delay = 5)
        {
            BTKUILib.QuickMenuAPI.ShowAlertToast(message, delay);
        }
    }
}
