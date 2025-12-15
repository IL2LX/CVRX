using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class SpinBot
    {
        public static float rotationSpeed = 120f;
        public static bool IsEnabled;
        public static void Render()
        {
            UIEx.Collapsible("SpinBot", () =>
            {
                IsEnabled = UIEx.Checkbox(IsEnabled, IsEnabled ? "Enabled" : "Disabled");
                if (IsEnabled)
                {
                    GUILayout.Label($"Spibot Speed:{rotationSpeed}");
                    rotationSpeed = (int)GUILayout.HorizontalSlider(rotationSpeed, 120, 290);
                }
            });
        }
        public static void Update()
        {
            if (IsEnabled)
            {
                PlayerWrappers.GetLocalPlayer().transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
