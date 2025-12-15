using BTKUILib.UIObjects;
using CVRX.Mods.ESP;

namespace CVRX.QMMenu.Pages_SubMenu
{
    internal class ESPSub
    {
        public static void Make()
        {
            MenuStruct.ESPsub = new Page("CVRX", "ESP", false, null)
            {
                MenuTitle = "ESP Menu",
                MenuSubtitle = $"WallHack Stuff."
            };
            MenuStruct.ESPsubCat = MenuStruct.ESPsub.AddCategory("CS:GO");
            MenuStruct.ESPsubCat.AddToggle("Box ESP", "Allow you to see ppls with 3D Box.", false).OnValueUpdated += b =>
            {
                BoxESP.BoxState(b);
            };
            MenuStruct.ESPsubCat.AddToggle("Line ESP", "Allow you to have a line to ppls.", false).OnValueUpdated += b =>
            {
                LineESP.LineState(b);
            };
            var LegacyCategory = MenuStruct.ESPsub.AddCategory("Legacy ESP");
            LegacyCategory.AddToggle("Item ESP", "Allow you to see item.", LegacyESP.Enabled).OnValueUpdated += b =>
            {
                LegacyESP.ObjectState(b);
            };
            LegacyCategory.AddToggle("Player ESP", "Allow you to see ppls.", LegacyESP.Enabled2).OnValueUpdated += b =>
            {
                LegacyESP.PBState(b);
            };
            MenuStruct.MainPageCat.AddButton("ESP", "cvrx_visuals", "Open ESP Menu.").OnPress += () => MenuStruct.ESPsub.OpenPage();
        }
    }
}
