using ABI_RC.Core.Player;
using CVRX.Handlers;

namespace CVRX.Hooks.PList
{
    internal class NamePlate : IPatch
    {
        public void Initialize()
        {
            HookManager.CreatePatch(typeof(PlayerNameplate).GetFromMethod(nameof(PlayerNameplate.UpdateNamePlate)), null, HookManager.GetPatch(typeof(NamePlate),nameof(NameplateUpdatePostix)));
        }
        private static void NameplateUpdatePostix(PlayerNameplate __instance)
        {
            __instance.gameObject.AddComponent<NameplateHandler>();
        }
    }
}
