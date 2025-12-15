using System;
using System.Reflection;
using UnityEngine;

namespace CVRX.Mods
{
    internal class ModsManager : MonoBehaviour
    {
        public static void CreateMods()
        {
            int a = 0;
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.IsAbstract) continue;
                if (!typeof(IMods).IsAssignableFrom(type)) continue;
                IMods instance = (IMods)Activator.CreateInstance(type);
                try
                {
                    instance.Initialize();
                    a++;
                }
                catch (Exception e)
                {
                    XConsole.Log("ModManager", $"Failed to Load {instance.GetType().Name}: {e}");
                }
            }
            XConsole.Log("ModManager", $"{a} Mods Loaded.");
        }

        void Update()
        {
            ESP.UpdateModule.OnUpdate();
            Movements.UpdateModule.Update();
        }
    }
}
