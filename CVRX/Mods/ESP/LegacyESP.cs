using ABI.CCK.Components;
using ABI_RC.Core.Player;
using CVRX.UIs;
using HighlightPlus;
using UnityEngine;

namespace CVRX.Mods.ESP
{
    internal class LegacyESP
    {
        public static bool Enabled = false;
        public static bool Enabled2 = false;
        public static void Render()
        {
            UIEx.Collapsible("LegacyESP", () =>
            {
                GUILayout.Label("This one may gonna make you lag if too many ppls.");
                Enabled = UIEx.Checkbox(Enabled, Enabled ? "Item ESP Enabled" : "Item ESP Disabled", (newState) =>
                {
                    ObjectState(newState);
                });
                Enabled2 = UIEx.Checkbox(Enabled2, Enabled2 ? "Player ESP Enabled" : "Player ESP Disabled", (newState) =>
                {
                    PBState(newState);
                });
            });
        }
        public static void ObjectState(bool s)
        {
            var pickupObjects = GameObject.FindObjectsOfType<CVRPickupObject>();
            foreach (var obj in pickupObjects)
            {
                var go = obj.gameObject;
                if (go.name.Contains("QuickMenu"))
                {
                    continue;
                }
                var highlight = go.GetComponent<HighlightEffect>();
                if (!s)
                {
                    if (highlight != null)
                    {
                        highlight.SetHighlighted(false);
                        GameObject.Destroy(highlight);
                    }
                }
                else
                {
                    if (highlight != null)
                    {
                        GameObject.Destroy(highlight);
                    }
                    var newHighlight = go.AddComponent<HighlightEffect>();
                    newHighlight.glowWidth = 0;
                    newHighlight.SetHighlighted(true);
                }
            }
        }
        public static void PBState(bool s)
        {
            var pickupObjects = GameObject.FindObjectsOfType<PlayerDescriptor>();
            foreach (var obj in pickupObjects)
            {
                if (obj.name.Contains("_PLAYERLOCAL"))
                {
                    continue;
                }
                var go = obj.gameObject;
                var highlight = go.GetComponent<HighlightEffect>();
                if (!s)
                {
                    if (highlight != null)
                    {
                        highlight.SetHighlighted(false);
                        Object.Destroy(highlight);
                    }
                }
                else
                {
                    if (highlight != null)
                    {
                        Object.Destroy(highlight);
                    }
                    var newHighlight = go.AddComponent<HighlightEffect>();
                    newHighlight.glowWidth = 0;
                    newHighlight.SetHighlighted(true);
                }
            }
        }
    }
}
