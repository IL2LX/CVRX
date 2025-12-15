using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class PosSaver
    {
        public static Vector3 targetPos;
        public static Quaternion targetRotation;
        public static bool Enabled = false;
        public static void Render()
        {
            UIEx.Collapsible("PosSaver", () =>
            {
                Enabled = UIEx.Checkbox(Enabled, Enabled ? "Enabled" : "Disabled", (newState) =>
                {
                    State(!Enabled);
                });
            });
        }
        public static void State(bool s)
        {
            var Localplayer = PlayerWrappers.GetLocalPlayer();
            if (s)
            {
                if (Localplayer == null)
                    return;
                targetPos = Localplayer.gameObject.transform.position;
                targetRotation = Localplayer.gameObject.transform.rotation;
            }
            else
            {
                if (Localplayer == null)
                    return;
                Localplayer.gameObject.transform.position = targetPos;
                Localplayer.gameObject.transform.rotation = targetRotation;
            }
        }
    }
}
