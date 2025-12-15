using HarmonyLib;
using MelonLoader;
using System.Linq;
using UnityEngine;

namespace CVRX.Hooks.PList
{
    internal class HWID : IPatch
    {
        private static readonly System.Random _rng = new System.Random();
        public static readonly string FakeHWID = GenerateHWID();

        public void Initialize()
        {
            MelonLogger.Msg($"[HWIDPatch]:Before ({UnityEngine.SystemInfo.deviceUniqueIdentifier})");
            HookManager.CreatePatch(AccessTools.Property(typeof(SystemInfo), "deviceUniqueIdentifier").GetMethod, null, HookManager.GetPatch(typeof(HWID), nameof(HWIDPostix)));
            MelonLogger.Msg($"[HWIDPatch]:After ({UnityEngine.SystemInfo.deviceUniqueIdentifier})");
        }

        private static void HWIDPostix(ref string __result)
        {
            __result = FakeHWID;
        }

        public static string GenerateHWID()
        {
            string originalHWID = UnityEngine.SystemInfo.deviceUniqueIdentifier;
            byte[] bytes = new byte[originalHWID.Length / 2];

            _rng.NextBytes(bytes);

            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }
    }
}
