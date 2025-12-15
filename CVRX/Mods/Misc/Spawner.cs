using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Misc
{
    internal class Spawner
    {
        public static void Render()
        {
            GUILayout.Label("Spawner Options:");
            UIEx.Collapsible("Prop Spawner", () =>
            {
                if (GUILayout.Button("Spawn from Clipboard"))
                {
                    ObjectWrappers.SpawnProp(ClipboardUtils.GetText());
                }
            });
            UIEx.Collapsible("Portal Spawner", () => 
            {
                if (GUILayout.Button("Spawn from Clipboard"))
                {
                    PortalHandler.SpawnPortal(ClipboardUtils.GetText(), PlayerWrappers.GetLocalPlayer().gameObject);
                }
            });
        }
    }
}
