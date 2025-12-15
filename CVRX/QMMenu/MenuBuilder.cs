using BTKUILib;
using BTKUILib.UIObjects;
using CVRX.QMMenu.Pages_SubMenu;
using System.Reflection;

namespace CVRX.QMMenu
{
    internal class MenuBuilder
    {
        public static void Make()
        {
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_logo", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_logo.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_exploits", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_exploits.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_options", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_options.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_visuals", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_visuals.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_physics", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_physics.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_spawner", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_spawner.png"));
            QuickMenuAPI.PrepareIcon("CVRX", "cvrx_misc", Assembly.GetExecutingAssembly().GetManifestResourceStream("CVRX.Res.cvrx_misc.png"));
            MenuStruct.MainPage = new Page("CVRX", "CVRX_MainPage", true, "cvrx_logo")
            {
                MenuTitle = "CVRX",
                MenuSubtitle = $"{Entry.ver}"
            };
            MenuStruct.MainPageCat = MenuStruct.MainPage.AddCategory("Main");
            SelfSub.Make();
            ESPSub.Make();
            Exploitsub.Make();
            SpawnerSub.Make();
            MiscSub.Make();
            SettingsSub.Make();
            SelectPage.Make();
        }
    }
}
