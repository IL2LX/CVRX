using BTKUILib.UIObjects;
using CVRX.Mods.Movements;

namespace CVRX.QMMenu.Pages_SubMenu
{
    internal class SelfSub
    {
        public static void Make()
        {
            MenuStruct.Selfsub = new Page("CVRX", "Self", false, null)
            {
                MenuTitle = "Self Menu",
                MenuSubtitle = $"Things to make your day better."
            };
            MenuStruct.SelfsubCat = MenuStruct.Selfsub.AddCategory("Flight");
            MenuStruct.SelfsubCat.AddToggle("Flight", "Allow you to fly like wee.", false).OnValueUpdated += b =>
            {
                Flight.Flyatoggle(!b);
            };
            var FlightSpeedSLD =  MenuStruct.SelfsubCat.AddSlider("Flight Speed:", "Set Speed.", 4.2f, 4.2f, 20);
            FlightSpeedSLD.OnValueUpdated += delegate {Flight.FlySpeed = FlightSpeedSLD.SliderValue; };
            var SpeedHackCategory = MenuStruct.Selfsub.AddCategory("SpeedHack");
            SpeedHackCategory.AddToggle("SpeedHack", "Allow you to go fast fast.", false).OnValueUpdated += b =>
            {
                SpeedHack.IsEnabled = b;
            };
            var SpeedHackWSLD = SpeedHackCategory.AddSlider("Walk Speed:", "Set Speed.", 10f, 10f, 50);
            SpeedHackWSLD.OnValueUpdated += delegate { SpeedHack.Speed = (int)SpeedHackWSLD.SliderValue; };
            var JetPackCategory = MenuStruct.Selfsub.AddCategory("JetPack");
            JetPackCategory.AddToggle("JetPack", "I discovered the jetpack.", false).OnValueUpdated += b =>
            {
                JetPack.IsEnabled = b;
            };
            var SpinBotCategory = MenuStruct.Selfsub.AddCategory("SpinBot");
            SpinBotCategory.AddToggle("SpinBot", "Allow you to spin.", false).OnValueUpdated += b =>
            {
                SpinBot.IsEnabled = b;
            };
            var SpinBotSpeedSLD = SpinBotCategory.AddSlider("Spin Speed:", "Set Speed.", 4.2f, 4.2f, 20);
            SpinBotSpeedSLD.OnValueUpdated += delegate { SpinBot.rotationSpeed = (int)SpinBotSpeedSLD.SliderValue; };
            MenuStruct.MainPageCat.AddButton("Self", "cvrx_physics", "Open Self Menu.").OnPress += () => MenuStruct.Selfsub.OpenPage();
        }
    }
}
