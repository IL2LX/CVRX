using ABI_RC.Core.Networking;
using ABI_RC.Core.Player;
using CVRX.Mods;
using CVRX.Mods.Exploits;
using CVRX.Mods.Misc;
using CVRX.Wrappers;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System.Linq;

namespace CVRX.Hooks.List
{
    internal class NetworkHook : IHook
    {
        public void Initialize()
        {
            NetworkManager.Instance.GameNetwork.MessageReceived += (sender, args) => Hook(sender, args);
            HookManager.Instance.Patch(typeof(UnityClient).GetMethods().Where(x => x.Name == "SendMessage").ToArray()[0], HookManager.GetPatch(typeof(NetworkHook), nameof(SendGameMessagePrefix)));
        }
        public static bool Hook(object __0, MessageReceivedEventArgs __1)
        {
            Tags tag = (Tags)__1.Tag;
            using (DarkRift.Message message = __1.GetMessage())
            {
                if (ConfManager.LogReceiveEvents)
                {
                    using (DarkRiftReader reader = message.GetReader())
                    {
                        byte[] Data = reader.ReadRaw(reader.Length);
                        XConsole.LogGameEvent(tag, Data, false);
                    }
                }
                switch (tag)
                {
                    case Tags.DropPortalBroadcast:
                        if (ConfManager.AntiPortal) return false;
                        return EventValidation.CheckPortalSpawn(message);
                    case Tags.CreateSpawnableObject:
                        return EventValidation.CheckPropSpawn(message);
                    case Tags.UpdateSpawnableObject:
                        return EventValidation.CheckPropUpdate(message);
                    case Tags.ObjectSync:
                        return EventValidation.CheckObjectSync(message);
                    case Tags.ObjectSyncOnJoin:
                        return EventValidation.CheckObjectSync(message);
                    case Tags.PlayerDisconnection:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                var player = CVRPlayerManager.Instance.GetPlayer(UserID);
                                XConsole.Log("Network", $"[-] {player.Username} [{UserID}]");
                            }
                        }
                        break;
                    case Tags.UserAccountingData:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                string Username = reader.ReadString();
                                string StaffTag = reader.ReadString();
                                string ImageURL = reader.ReadString();
                                string Rank = reader.ReadString();
                                string AvatarID = reader.ReadString();
                                XConsole.Log("Network", $"[+] {Username} [{UserID}]");
                            }
                        }
                        break;
                    case Tags.SwitchIntoAvatar:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                string AvatarID = reader.ReadString();
                                var player = CVRPlayerManager.Instance.GetPlayer(UserID);
                                XConsole.Log("Network", $"[Avatar] {player.GetUsername()} changed Avatar [{AvatarID}]");
                                AviLogger.LogAvatar(player.GetUsername(), AvatarID);
                            }
                            break;
                        }
                }
            }
            return true;
        }
        private static bool SendGameMessagePrefix(ref DarkRift.Message __0, ref SendMode __1)
        {
            if (ConfManager.LogRaiseEvents)
            {
                using (DarkRiftReader reader = __0.GetReader())
                {
                    byte[] Data = reader.ReadRaw(reader.Length);
                    XConsole.LogGameEvent((Tags)__0.Tag, Data, true);
                }
            }
            switch ((Tags)__0.Tag)
            {
                case Tags.NetworkedRootData:
                    return !FakeSerialize.NoSerialize;
            }
            return true;
        }
    }
}
