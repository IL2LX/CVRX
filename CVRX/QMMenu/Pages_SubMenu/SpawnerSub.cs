using BTKUILib.UIObjects;
using CVRX.Wrappers;

namespace CVRX.QMMenu.Pages_SubMenu
{
    internal class SpawnerSub
    {
        public static void Make()
        {
            MenuStruct.Spawnersub = new Page("CVRX", "Spawner", false, null)
            {
                MenuTitle = "Spawner Menu",
                MenuSubtitle = $"Yes, we are in gmod."
            };
            MenuStruct.SpawnersubCat = MenuStruct.Spawnersub.AddCategory("Prop");
            MenuStruct.SpawnersubCat.AddButton("Spawn from clipboard", null, "You guees ?").OnPress += () => ObjectWrappers.SpawnProp(ClipboardUtils.GetText());
            var PortalCategory = MenuStruct.Spawnersub.AddCategory("Portal");
            PortalCategory.AddButton("Spawn from clipboard", null, "You guees ?").OnPress += () => PortalHandler.SpawnPortal(ClipboardUtils.GetText(),PlayerWrappers.GetLocalPlayer());
            MenuStruct.MainPageCat.AddButton("Spawner", "cvrx_spawner", "Open Spawner Menu.").OnPress += () => MenuStruct.Spawnersub.OpenPage();
        }
    }
}
