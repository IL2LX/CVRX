using CVRX.Handlers;
using CVRX.Hooks;
using CVRX.Mods;
using CVRX.Mods.Misc;
using CVRX.UIs;
using CVRX.Wrappers;
using MelonLoader;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace CVRX
{
    public class Entry : MelonMod
    {
        public static string ver = "v0.2.4 - Lolipop";
        static GameObject Handler;

        public override void OnInitializeMelon()
        {
            XConsole.Alloc();
            HookManager.CreatePatch();
        }

        public override void OnLateInitializeMelon()
        {
            MelonLogger.Msg("Preparing BTK shit...");
            QMMenu.MenuBuilder.Make();
            ABI_RC.Core.Savior.IntroManager.Skip();
            MelonCoroutines.Start(WaitForPlayerAndInit()); 
        }

        private static IEnumerator WaitForPlayerAndInit()
        {
            while (GameObject.Find("_PLAYERLOCAL") == null)
            {
                yield return null;
            }
            MelonLogger.Msg("Starting CVRX...");
            ConfManager.LoadConfig();
            AudioHandler.Setup();
            Handler = new GameObject();
            if (!GeneralWrappers.IsInVr())
            {
                Handler.AddComponent<Gui>();
                Handler.AddComponent<GUIRadar>();
                Handler.AddComponent<GuiEsp>(); 
                Handler.AddComponent<Thirdperson>();
            }
            Handler.AddComponent<Watermark>();
            Handler.AddComponent<ModsManager>();
            GameObject.DontDestroyOnLoad(Handler);
            HookManager.CreateHooks();
            ModsManager.CreateMods();
            XConsole.Log("Console", $"Welcome to CVRX {ver}");
            yield break; 
        }

        public override void OnApplicationQuit()
        {
            ConfManager.SaveConfig();
            Thread.Sleep(1000);
            XConsole.Log("Quit", $"Bye bya.");
        }
    }
}
