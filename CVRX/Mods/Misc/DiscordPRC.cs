using CVRX.Wrappers;
using System;
using System.IO;
using System.Net;

namespace CVRX.Mods.Misc
{
    internal class DiscordPRC : IMods
    {
        private const string DllUrl = "https://github.com/IL2LX/FileHost/raw/refs/heads/main/discord-rpc.dll";
        public void Initialize()
        {
            try
            {
                string targetDir = AppDomain.CurrentDomain.BaseDirectory;
                string dllPath = Path.Combine(targetDir, "discord-rpc.dll");
                if (!File.Exists(dllPath))
                {
                    DownloadDll(dllPath, DllUrl);
                }
                Start();
            }
            catch (Exception ex)
            {
                XConsole.Log("Mods:DiscordRPC", $"Failed to download or start discord-rpc: {ex}",true);
            }
        }

        private void DownloadDll(string path, string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(url, path);
            }
        }

        public void Start()
        {
            eventHandlers = default(DiscordRpc.EventHandlers);
            eventHandlers.errorCallback = delegate (int code, string message) { };

            presence.state = $"ChillloutVR";
            if (GeneralWrappers.IsInVr())
            {
                presence.details = "ChilloutVR in VR.";
            }
            else
            {
                presence.details = "ChilloutVR in DSK.";
            }
            presence.largeImageKey = "https://files.catbox.moe/kbetvv.png";
            presence.largeImageText = $"CVRX - Best ChilloutVR Tool.";
            presence.smallImageKey = "https://files.catbox.moe/zcipk5.png";
            presence.smallImageText = "Chiness Sucker.";
            presence.partySize = 6;
            presence.partyMax = 9;
            presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            presence.partyId = Guid.NewGuid().ToString();

            try
            {
                DiscordRpc.Initialize("1441752809337782292", ref eventHandlers, true, "");
                DiscordRpc.UpdatePresence(ref presence);
            }
            catch
            {
            }
        }

        public static void ChangeDetails(string a)
        {
            presence.details = a;
            DiscordRpc.UpdatePresence(ref presence);
        }

        internal static DiscordRpc.RichPresence presence;
        internal static DiscordRpc.EventHandlers eventHandlers;
    }
}
