using ABI_RC.Systems.Movement;
using CVRX.Wrappers;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class UtilsStuff
    {
        public static bool FlightForced = false;
        public static void SetWalkSpeed(float speed)
        {
            PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().maxWalkSpeed = speed;
            PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().maxWalkSpeedCrouched = speed;
        }
        public static void Teleport(GameObject a)
        {
            PlayerWrappers.GetLocalPlayer().gameObject.transform.position = a.transform.position;
        }
        public static void ForceFlightStat()
        {
            FlightForced = !FlightForced;
            if (FlightForced)
            {
                PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().FlightAllowedInWorld = true;
                HudWrappers.TriggerDropText("Utils Stuff", "Forced flight to be allowed in this world.");
            }
            else
            {
                PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().FlightAllowedInWorld = false;
                HudWrappers.TriggerDropText("Utils Stuff", "Forced flight to be unallowed in this world.");
            }
        }
    }
}
