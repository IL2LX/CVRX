using ABI_RC.Systems.GameEventSystem;
using ABI_RC.Systems.Movement;
using CVRX.Mods.Misc;
using CVRX.Wrappers;

namespace CVRX.Hooks.List
{
    internal class WorldHook : IHook
    {
        public void Initialize()
        {
            CVRGameEventSystem.World.OnLoad.AddListener((worldName) => Hook(worldName));
            CVRGameEventSystem.Instance.OnDisconnected.AddListener((disconnectedMsg) => DumbHook());
        }
        public static void DumbHook()
        {
            ForceGarbageCollection.RamClear();
        }
        public static void Hook(string WName)
        {
            if (!PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().FlightAllowedInWorld)
            {
                PlayerWrappers.GetLocalPlayer().GetComponent<BetterBetterCharacterController>().FlightAllowedInWorld = true;
                XConsole.Log("World", "Force allowed flight in-world.",true);
            }
        }
    }
}
