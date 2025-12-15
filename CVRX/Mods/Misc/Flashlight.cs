using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Misc
{
    internal class Flashlight
    {
        public static Light light;
        public static int intensity = 1;
        public static int range = 60;
        public static bool Enabled = false;

        public static void State(bool s)
        {
            var LocalPlayer = PlayerWrappers.GetLocalPlayer();
            if (s)
            {
                if (LocalPlayer == null)
                    return;
                light = LocalPlayer.gameObject.AddComponent<Light>();
                light.type = LightType.Directional;
                light.intensity = intensity;
                light.range = range;
            }
            else
            {
                if (LocalPlayer == null)
                    return;
                if (light != null)
                {
                    GameObject.Destroy(light);
                }
            }
        }

        public static void Render()
        {
            UIEx.Collapsible("Flashlight", () =>
            {
                Enabled = UIEx.Checkbox(Enabled, Enabled ? "Enabled" : "Disabled", (newState) =>
                {
                    State(!Enabled);
                });
            });
        }
    }
}
