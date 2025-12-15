using BTKUILib.UIObjects;
using CVRX.Mods.Misc;

namespace CVRX.QMMenu.Pages_SubMenu
{
    internal class MiscSub
    {
        public static void Make()
        {
            MenuStruct.Miscsub = new Page("CVRX", "Misc", false, null)
            {
                MenuTitle = "Misc Menu",
                MenuSubtitle = $"Ramdom shit."
            };
            MenuStruct.MiscsubCat = MenuStruct.Miscsub.AddCategory("Misc Shit");
            MenuStruct.MiscsubCat.AddToggle("FlashLight", "I'm the light.", false).OnValueUpdated += b =>
            {
                Flashlight.State(b);
            };
            MenuStruct.MainPageCat.AddButton("Misc", "cvrx_misc", "Open Misc Menu.").OnPress += () => MenuStruct.Miscsub.OpenPage();
        }
    }
}
