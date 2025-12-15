using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CVRX.Wrappers
{
    public static class ClipboardUtils
    {
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(System.IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        private static extern System.IntPtr SetClipboardData(uint uFormat, System.IntPtr data);

        [DllImport("user32.dll")]
        private static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("user32.dll")]
        private static extern System.IntPtr GetClipboardData(uint uFormat);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern System.IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern System.IntPtr GlobalLock(System.IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GlobalUnlock(System.IntPtr hMem);

        const uint CF_UNICODETEXT = 13;
        const uint GMEM_MOVEABLE = 0x0002;

        public static void SetText(string text)
        {
            if (!OpenClipboard(System.IntPtr.Zero))
                return;

            EmptyClipboard();
            var bytes = Encoding.Unicode.GetBytes(text + "\0");
            var hGlobal = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)bytes.Length);
            var target = GlobalLock(hGlobal);
            Marshal.Copy(bytes, 0, target, bytes.Length);
            GlobalUnlock(hGlobal);
            SetClipboardData(CF_UNICODETEXT, hGlobal);
            CloseClipboard();
        }

        public static string GetText()
        {
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
                return string.Empty;
            if (!OpenClipboard(IntPtr.Zero))
                return string.Empty;
            string result = string.Empty;
            IntPtr handle = GetClipboardData(CF_UNICODETEXT);
            if (handle != IntPtr.Zero)
            {
                IntPtr pointer = GlobalLock(handle);
                if (pointer != IntPtr.Zero)
                {
                    result = Marshal.PtrToStringUni(pointer);
                    GlobalUnlock(handle);
                }
            }
            CloseClipboard();
            return result ?? string.Empty;
        }
    }
}
