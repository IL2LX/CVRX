using BTKUILib.UIObjects;

namespace CVRX.QMMenu.Pages_SubMenu
{
    internal class SettingsSub
    {
        public static void Make()
        {
            MenuStruct.Settingsub = new Page("CVRX", "Settings", false, null)
            {
                MenuTitle = "Settings Menu",
                MenuSubtitle = $"Settings for the cheat."
            };
            MenuStruct.SettingsubCat = MenuStruct.Settingsub.AddCategory("Log Settings");
            MenuStruct.SettingsubCat.AddToggle("Log Raise Events", "Allow you to Log Send Events.", false).OnValueUpdated += b =>
            {
                ConfManager.LogRaiseEvents = b;
            };
            MenuStruct.SettingsubCat.AddToggle("Log Recive Events", "Allow you to Log Recived Events.", false).OnValueUpdated += b =>
            {
                ConfManager.LogReceiveEvents = b;
            };
            var ExploitSetCategory = MenuStruct.Settingsub.AddCategory("Exploits Settings");
            ExploitSetCategory.AddToggle("Serialize pos restore", "stfu i'm lazy.", false).OnValueUpdated += b =>
            {
                ConfManager.Ex_Serialize_RestorePos = b;
            };
            MenuStruct.MainPageCat.AddButton("Settings", "cvrx_options", "Open Settings Menu.").OnPress += () => MenuStruct.Settingsub.OpenPage();
        }
    }
}
