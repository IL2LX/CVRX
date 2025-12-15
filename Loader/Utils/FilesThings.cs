using System;
using System.IO;

namespace Loader.Utils
{
    internal static class FilesThings
    {
        private static readonly string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"CVRX");

        private static readonly string FilePath = Path.Combine(AppFolder, "femboy.data");

        private static void EnsureFolder()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }

        public static void Write(string text)
        {
            EnsureFolder();
            File.WriteAllText(FilePath, text);
        }

        public static void Append(string text)
        {
            EnsureFolder();
            File.AppendAllText(FilePath, text + Environment.NewLine);
        }

        public static string Read()
        {
            if (!File.Exists(FilePath))
                return string.Empty;

            return File.ReadAllText(FilePath);
        }

        public static bool Exists()
        {
            return File.Exists(FilePath);
        }

        public static void Delete()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        public static string GetPath()
        {
            return FilePath;
        }
    }
}
