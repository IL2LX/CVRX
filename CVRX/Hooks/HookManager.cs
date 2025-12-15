using HarmonyLib;
using MelonLoader;
using System;
using System.Reflection;

namespace CVRX.Hooks
{
    internal static class HookManager
    {
        public static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("LXClient");

        public static HarmonyMethod GetPatch(Type type, string methodName)
        {
            return new HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
        }

        #region GetFromMethod
        public static MethodInfo GetFromMethod(this Type Type, string name)
        {
            return Type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }
        #endregion

        #region CreatePatch
        public static void CreatePatch(MethodBase TargetMethod, HarmonyMethod Before = null, HarmonyMethod After = null)
        {
            try
            {
                Instance.Patch(TargetMethod, Before, After);
            }
            catch (Exception e)
            {
                XConsole.Log("HookManager", $"Failed to Patch {TargetMethod.Name}.\n{e}\n");
            }
        }
        #endregion

        public static void CreatePatch()
        {
            int a = 0;
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.IsAbstract) continue;
                if (!typeof(IPatch).IsAssignableFrom(type)) continue;
                IPatch instance = (IPatch)Activator.CreateInstance(type);
                try
                {
                    instance.Initialize();
                    a++;
                }
                catch (Exception e)
                {
                    MelonLogger.Msg($"[Patcher] Failed to Initialize {instance.GetType().Name}: {e}");
                }
            }
            MelonLogger.Msg($"[Patcher] {a} patches done.");
        }

        public static void CreateHooks()
        {
            int a = 0;
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.IsAbstract) continue;
                if (!typeof(IHook).IsAssignableFrom(type)) continue;
                IHook instance = (IHook)Activator.CreateInstance(type);
                try
                {
                    instance.Initialize();
                    a++;
                }
                catch (Exception e)
                {
                    XConsole.Log("Hooker", $"Failed to Hook {instance.GetType().Name}: {e}");
                }
            }
            XConsole.Log("Hooker", $"{a} Hooks Placed.");
        }
    }
}
