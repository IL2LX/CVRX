using System;
using System.IO;

namespace CVRX.Wrappers
{
    internal static class KeyThing
    {
        private static readonly string AppFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CVRX");

        private static readonly string FilePath =
            Path.Combine(AppFolder, "femboy.data");

        private const int RequiredLength = 23;

        private static void EnsureFolder()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }

        public static string Read()
        {
            if (!File.Exists(FilePath))
                return string.Empty;
            return File.ReadAllText(FilePath);
        }

        public static bool Exists()
        {
            if (!File.Exists(FilePath))
                return false;
            string key = File.ReadAllText(FilePath).Trim();
            return key.Length == RequiredLength;
        }

        public static bool IsValidFormat()
        {
            if (!File.Exists(FilePath))
                return false;
            string key = File.ReadAllText(FilePath).Trim();
            return key.Length == RequiredLength;
        }
    }
}
