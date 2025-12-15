using BTKUILib;
using BTKUILib.UIObjects;
using CVRX.Mods.Exploits;
using CVRX.Mods.Misc;
using CVRX.Mods.Movements;
using CVRX.Wrappers;

namespace CVRX.QMMenu
{
    internal class SelectPage
    {
        public static void Make()
        {
            Category category = QuickMenuAPI.PlayerSelectPage.AddCategory("CVRX Basic", "CVRX");
            category.AddButton("Copy UserID", null, "Copy the selected userid.").OnPress += () =>
            {
                ClipboardUtils.SetText(QuickMenuAPI.SelectedPlayerEntity.CVRPlayer.GetUserID());
            };
            category.AddButton("Copy AvatarID", null, "Copy the selected avatarid.").OnPress += () =>
            {
                ClipboardUtils.SetText(QuickMenuAPI.SelectedPlayerEntity.AvatarID);
            };
            category.AddButton("ForceClone", null, "ForceClone the selected player avatar.").OnPress += () =>
            {
                GeneralWrappers.SwitchAvatar(QuickMenuAPI.SelectedPlayerEntity.AvatarID);
            };
            category.AddButton("Teleport", null, "Teleport to the selected user.").OnPress += () =>
            {
                PlayerWrappers.GetLocalPlayer().transform.position = QuickMenuAPI.SelectedPlayerEntity.PlayerGameObject.transform.position;
            };
            category.AddButton("Dump Avatar", null, "Dump selected player avatar.").OnPress += () =>
            {
                BTKWrappers.ShowConfirm("AssetDumper", $"Would you like to dump {QuickMenuAPI.SelectedPlayerEntity.Username}'s current avatar,\nyou game may freeze while dumping avatar.",() => AssetDumper.DumpPlayerAvatar(QuickMenuAPI.SelectedPlayerEntity.CVRPlayer), () => BTKWrappers.ShowAlertToast("Dump canceled."));
            };
            Category category2 = QuickMenuAPI.PlayerSelectPage.AddCategory("CVRX Funnies", "CVRX");
            category2.AddToggle("SeatOnHead", "Allow you to seat on selected player head.", false).OnValueUpdated += b =>
            {
                SeatOnHead.State(b);
            };
            category2.AddToggle("SpyCam", "Allow you to spy on selected player.", false).OnValueUpdated += b =>
            {
                SpyCamera.Toggle(b);
            };
            category2.AddButton("Force Lewd", null, "i really want to kms for adding this.").OnPress += () =>
            {
                ForceLewd.LewdPlayer(QuickMenuAPI.SelectedPlayerEntity.CVRPlayer);
            };
            Category category3 = QuickMenuAPI.PlayerSelectPage.AddCategory("CVRX Props", "CVRX");
            category3.AddButton("Kaboom", null, "Place a landmine on selected player.").OnPress += () =>
            {
                ObjectWrappers.SpawnProp("00817e88-8445-4ce0-9bcb-d890819ef899", QuickMenuAPI.SelectedPlayerEntity.PlayerGameObject.transform.position);
            };
        }
    }
}
