using CVRX.Mods.Misc;
using CVRX.UIs;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class JetPack
    {
        public static bool IsEnabled = false;
        public static void Render()
        {
            UIEx.Collapsible("JetPack", () =>
            {
                IsEnabled = UIEx.Checkbox(IsEnabled, IsEnabled ? "Enabled" : "Disabled");
            });
        }
        public static void Update()
        {
            if (!IsEnabled)
                return;
            bool jumpPressed;
            if (GeneralWrappers.IsInVr()) {
                jumpPressed = VRBinds.Button_Jump.GetState(0);
            }
            else {
                jumpPressed = Input.GetKey((KeyCode)32);
            }
            if (jumpPressed)
            {
                var lp = PlayerWrappers.GetLocalPlayer().GetBetterBetterCharacterController();
                Vector3 velocity = lp.GetVelocity();
                velocity.y = lp.jumpImpulse;
                lp.SetVelocity(velocity);
            }
        }
    }
}
