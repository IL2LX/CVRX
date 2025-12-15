using ABI_RC.Core.Player;
using ABI_RC.Systems.Movement;
using System;
using UnityEngine;

namespace CVRX.Wrappers
{
    internal static class PlayerWrappers
    {
        public static GameObject GetPlayerObject(this CVRPlayerEntity player) { return player.PlayerObject; }
        public static string GetAvatarID(this CVRPlayerEntity player) { return player.PlayerDescriptor.avtrId; }
        public static PlayerNameplate GetNameplate(this CVRPlayerEntity player) { return player.PlayerNameplate; }
        public static string GetImageURL(this CVRPlayerEntity player) { return player.ApiProfileImageUrl; }
        public static string GetUsername(this CVRPlayerEntity player) { return player.Username; }
        public static string GetUserID(this CVRPlayerEntity player) { return player.Uuid; }
        public static PlayerDescriptor GetPlayerDescriptor(this CVRPlayerEntity player) { return player.PlayerDescriptor; }
        public static PuppetMaster GetPuppetMaster(this CVRPlayerEntity player) { return player.PuppetMaster; }   
        public static string GetStaffTag(this CVRPlayerEntity player) { return player.ApiUserStaffTag; }
        public static CVRPlayerEntity[] GetAllNetworkedPlayers(this CVRPlayerManager instance) { return instance.NetworkPlayers.ToArray(); }
        public static string GetAvatarID(this PlayerDescriptor player) { return player.avtrId; }
        public static bool IsAvatarBlocked(this PlayerDescriptor player) { return player.avatarBlocked; }
        public static string GetClanTag(this PlayerDescriptor player) { return player.userClanTag; }
        public static string GetStaffTag(this PlayerDescriptor player) { return player.userStaffTag; }
        public static string GetOwnerID(this PlayerDescriptor player) { return player.ownerId; }
        public static string GetImageURL(this PlayerDescriptor player) { return player.profileImageUrl; }
        public static string GetUsername(this PlayerDescriptor player) { return player.userName; }
        public static GameObject GetAvatarObject(this PuppetMaster player) { return player.AvatarDescriptor.gameObject; }
        public static GameObject GetAvatarObject(this CVRPlayerEntity player) { return player.AvatarHolder; }
        public static PlayerDescriptor GetLocalPlayerDescriptor() { return GetLocalPlayer().GetComponent<PlayerDescriptor>(); }
        public static PlayerSetup GetLocalPlayerSetup() { return GetLocalPlayer().GetComponent<PlayerSetup>(); }
        public static CVRPlayerEntity[] GetAllPlayers()
        {
            return CVRPlayerManager.Instance?.GetAllNetworkedPlayers();
        }
        private static GameObject LocalPlayer;
        public static GameObject GetLocalPlayer()
        {
            if (LocalPlayer == null) LocalPlayer = GameObject.Find("_PLAYERLOCAL");
            return LocalPlayer;
        }
        public static BetterBetterCharacterController GetBetterBetterCharacterController(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<BetterBetterCharacterController>();
        }
        public static CVRPlayerEntity GetPlayer(this CVRPlayerManager Instance, string UserID)
        {
            foreach (CVRPlayerEntity player in Instance.GetAllNetworkedPlayers())
            {
                if (player.Uuid == UserID) return player;
            }
            return null;
        }

        public enum PlayerRank
        {
            None,
            User,
            Legend,
            Guide,
            Mod,
            Dev,
        }

        public static PlayerRank GetRank(string Rank)
        {
            switch (Rank)
            {
                case "User":
                    return PlayerRank.User;

                case "Legend":
                    return PlayerRank.Legend;

                case "Community Guide":
                    return PlayerRank.Guide;

                case "Moderator":
                    return PlayerRank.Mod;

                case "Developer":
                    return PlayerRank.Dev;
            }
            return PlayerRank.None;
        }
        public static Color GetRankColor(string rank)
        {
            switch (rank)
            {
                case "User":
                    return Color.green;

                case "Legend":
                    return Color.white;

                case "Community Guide":
                    return Color.magenta;

                case "Moderator":
                    return Color.red;

                case "Developer":
                    return new Color(1f, 0.5f, 0f); // orange-like for dev

                default:
                    return Color.black;
            }
        }

        public static PlayerRank GetRank(this CVRPlayerEntity player)
        {
            return GetRank(player.ApiUserRank);
        }

        public static PlayerRank GetRank(this PlayerDescriptor player)
        {
            return GetRank(player.userRank);
        }
        public static Color GetRankColor(this CVRPlayerEntity player)
        {
            return GetRankColor(player.ApiUserRank);
        }

        public static bool GetIsBot(this CVRPlayerEntity player)
        {
            return player.GetPlayerObject().transform.position == Vector3.zero;
        }
    }
}