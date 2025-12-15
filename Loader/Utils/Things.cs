using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System.IO;
using System.Net;

namespace Loader.Utils
{
    internal class Things
    {
        public static void PlaySound(UnmanagedMemoryStream sound)
        {
            Audio audio = new Audio();
            audio.Play(sound, AudioPlayMode.Background);
        }

        public static string GetProductID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
            string ProductID = registryKey.GetValue("ProductID").ToString();
            registryKey.Close();
            return ProductID;
        }

        public static string GetMachineID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\SQMClient", false);
            string MachineID = registryKey.GetValue("MachineId").ToString();
            registryKey.Close();
            return MachineID;
        }

        public static string GetProfileGUID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", false);
            string ProfileGUID = registryKey.GetValue("HwProfileGUID").ToString();
            registryKey.Close();
            return ProfileGUID;
        }
    }
}
