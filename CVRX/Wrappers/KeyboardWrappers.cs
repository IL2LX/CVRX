using ABI_RC.Core.UI.UIRework.Managers;
using System;

namespace CVRX.Wrappers
{
    internal class KeyboardWrappers
    {
        public static void OpenKeyboard(string currentValue, Action<string> callback)
        {
            KeyboardManager.Instance.ShowKeyboard(currentValue, callback, null, "Success", 0, false, false, null, KeyboardManager.OpenSource.Other);
        }
        public static void OpenKeyboard(string currentText, Action<string> callback, string placeholder, int maxCharacterCount, bool multiLine, string title)
        {
            KeyboardManager.Instance.ShowKeyboard(currentText, callback, placeholder, "Success", maxCharacterCount, false, multiLine, title, KeyboardManager.OpenSource.Other);
        }
    }
}
