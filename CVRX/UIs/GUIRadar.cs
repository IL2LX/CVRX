using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.UIs
{
    internal class GUIRadar : MonoBehaviour
    {
        public static float radarSize = 150f;
        public static float radarRange = 50f;

        public static Texture2D dotTexture = Texture2D.whiteTexture;

        public static bool Enabled = false;
        public static bool UseRankColor = true;
        public static Color CustomColor = Color.red;

        public static void Render()
        {
            UIEx.Collapsible("Radar", () =>
            {
                Enabled = UIEx.Checkbox(Enabled, Enabled ? "Enabled" : "Disabled");
                UseRankColor = UIEx.Checkbox(UseRankColor, UseRankColor ? "Use Rank Color" : "Use Custom Color");
                if (!UseRankColor)
                {
                    UIEx.ColorPicker("Custom Dot Color", ref CustomColor, (newColor) =>
                    {
                        CustomColor = newColor;
                    });
                }
            });
        }

        void OnGUI()
        {
            if (Enabled)
            {
                GUI.backgroundColor = ConfManager.GUI_BackgroundColor;
                var localPlayerWrapper = PlayerWrappers.GetLocalPlayer();
                if (localPlayerWrapper == null) return;
                Transform localPlayerTransform = localPlayerWrapper.transform;
                float radarX = 2f;
                float radarY = 30f;
                GUI.Box(new Rect(radarX, radarY, radarSize, radarSize), "Radar");
                foreach (var player in PlayerWrappers.GetAllPlayers())
                {
                    if (player == null || player.PlayerObject == null || player.PlayerObject.transform == null)
                        continue;
                    if (player.PlayerObject.transform == localPlayerTransform)
                        continue;
                    Vector3 offset = player.PlayerObject.transform.position - localPlayerTransform.position;
                    float angle = -localPlayerTransform.eulerAngles.y * Mathf.Deg2Rad;
                    float rotatedX = offset.x * Mathf.Cos(angle) - offset.z * Mathf.Sin(angle);
                    float rotatedZ = offset.x * Mathf.Sin(angle) + offset.z * Mathf.Cos(angle);
                    float normalizedX = Mathf.Clamp(rotatedX / radarRange, -0.5f, 0.5f);
                    float normalizedZ = Mathf.Clamp(rotatedZ / radarRange, -0.5f, 0.5f);
                    float dotX = radarX + radarSize / 2 + normalizedX * radarSize;
                    float dotY = radarY + radarSize / 2 + normalizedZ * radarSize;
                    GUI.color = UseRankColor ? player.GetRankColor() : CustomColor;
                    GUI.DrawTexture(new Rect(dotX - 2, dotY - 2, 4, 4), dotTexture);
                }
                GUI.color = Color.green;
                GUI.DrawTexture(new Rect(radarX + radarSize / 2 - 2, radarY + radarSize / 2 - 2, 4, 4), dotTexture);
                GUI.color = Color.white;
            }
        }
    }
}
