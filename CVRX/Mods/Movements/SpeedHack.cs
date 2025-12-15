using CVRX.UIs;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class SpeedHack
    {
        public static bool IsEnabled = false;
        public static int Speed = 10;
        public static void Render()
        {
            UIEx.Collapsible("SpeedHack", () =>
            {
                IsEnabled = UIEx.Checkbox(IsEnabled, IsEnabled ? "Enabled" : "Disabled", (newState) => 
                {
                    IsEnabled = newState;
                });
                if (IsEnabled)
                {
                    GUILayout.Label($"Speed:{Speed}");
                    Speed = (int)GUILayout.HorizontalSlider(Speed, 10, 50);
                }
            });
        }

        public static void Update()
        {
            if (IsEnabled)
            {
                UtilsStuff.SetWalkSpeed(Speed);
            }
        }
    }
}
