using ABI_RC.Core.EventSystem;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Savior;
using DarkRift;

namespace CVRX.Wrappers
{
    internal class GeneralWrappers
    {
        public static void SwitchAvatar(string ID) { AssetManagement.Instance.LoadLocalAvatar(ID); }
        public static bool IsInVr()
        {
            return MetaPort.Instance.isUsingVr;
        }
        public static bool IsConnected()
        {
            return NetworkManager.Instance.GameNetwork.ConnectionState == ConnectionState.Connected;
        }
    }
}
