using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class RayCastTP
    {
        public static bool expanded = false;
        public static bool Enabled = false;
        public static void Render()
        {
            UIEx.Collapsible("RayCastTP", () =>
            {
                Enabled = UIEx.Checkbox(Enabled, Enabled ? "Enabled" : "Disabled");
            });
        }
        public static void Update()
        {
            if (Enabled)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit)) PlayerWrappers.GetLocalPlayer().gameObject.transform.position = raycastHit.point;
                }
            }
        }
    }
}
