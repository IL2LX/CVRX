using System.IO;
using System.Collections.Generic;
using UnityEngine;
using CVRX.Mods.ESP;
using CVRX.UIs;

namespace CVRX
{
    internal static class ConfManager
    {
        private static readonly string ConfigPath = "CVRX/config.txt";

        public static bool AntiPortal = false;
        public static bool LogReceiveEvents = false;
        public static bool LogRaiseEvents = false;
        public static bool Ex_Serialize_RestorePos = false;

        public static Color GUI_BackgroundColor = new Color(0.8f, 0.1f, 0.1f, 1f);
        public static Color GUI_BoxESPColor = new Color(1f, 1f, 1f, 1f);
        public static Color GUI_DimensionalESPColor = new Color(1f, 1f, 1f, 1f);
        public static Color GUI_TracerESPColor = new Color(1f, 1f, 1f, 1f);
        public static Color GUI_NameESPColor = new Color(1f, 1f, 1f, 1f);

        static ConfManager()
        {
            string folder = Path.GetDirectoryName(ConfigPath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (File.Exists(ConfigPath))
                LoadConfig();
            else
                SaveConfig();
        }

        public static void Render()
        {
            GUILayout.Label("Seetings:");
            UIEx.Collapsible("GUI Settings", () =>
            {
                UIEx.ColorPicker("GUI Background Color", ref GUI_BackgroundColor, (newColor) =>
                {
                    GUI_BackgroundColor = newColor;
                });
            });
            UIEx.Collapsible("Network Settings", () =>
            {
                LogReceiveEvents = UIEx.Checkbox(LogReceiveEvents, "Log Recive Events", (newState) =>
                {
                    LogReceiveEvents = newState;
                });
                LogRaiseEvents = UIEx.Checkbox(LogRaiseEvents, "Log Raise Events", (newState) =>
                {
                    LogRaiseEvents = newState;
                });
            });
            UIEx.Collapsible("Exploits Settings", () =>
            {
                Ex_Serialize_RestorePos = UIEx.Checkbox(Ex_Serialize_RestorePos, "Serialize Restore Pos", (newState) =>
                {
                    Ex_Serialize_RestorePos = newState;
                });
            });
            GUILayout.Space(5);
            if (GUILayout.Button("Save Config"))
            {
                SaveConfig();
            }
        }

        public static void LoadConfig()
        {
            var lines = File.ReadAllLines(ConfigPath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                    continue;

                var split = line.Split('=');
                if (split.Length != 2)
                    continue;

                string key = split[0].Trim();
                string value = split[1].Trim();

                bool boolVal = value.ToLower() == "true" || value == "1";

                switch (key)
                {
                    case "AntiPortal":
                        AntiPortal = boolVal;
                        break;
                    case "LogReceiveEvents":
                        LogReceiveEvents = boolVal;
                        break;
                    case "LogRaiseEvents":
                        LogRaiseEvents = boolVal;
                        break;
                    case "Ex_Serialize_RestorePos":
                        Ex_Serialize_RestorePos = boolVal;
                        break;
                    case "GUI_BackgroundColor":
                        GUI_BackgroundColor = ParseColor(value);
                        break;
                    case "GUI_BoxESPColor":
                        GUI_BoxESPColor = ParseColor(value);
                        break;
                    case "GUI_DimensionalESPColor":
                        GUI_BoxESPColor = ParseColor(value);
                        break;
                    case "GUI_TracerESPColor":
                        GUI_TracerESPColor = ParseColor(value);
                        break;
                    case "GUI_NameESPColor":
                        GUI_NameESPColor = ParseColor(value);
                        break;
                    case "BoxESP_CustomColor":
                        BoxESP.CustomColor = ParseColor(value);
                        break;
                }
            }
        }

        public static void SaveConfig()
        {
            List<string> lines = new List<string>
            {
                $"AntiPortal={AntiPortal}",
                $"LogReceiveEvents={LogReceiveEvents}",
                $"LogRaiseEvents={LogRaiseEvents}",
                $"Ex_Serialize_RestorePos={Ex_Serialize_RestorePos}",
                $"GUI_BackgroundColor={GUI_BackgroundColor.r},{GUI_BackgroundColor.g},{GUI_BackgroundColor.b},{GUI_BackgroundColor.a}",
                $"GUI_BoxESPColor={GUI_BoxESPColor.r},{GUI_BoxESPColor.g},{GUI_BoxESPColor.b},{GUI_BoxESPColor.a}",
                $"GUI_DimensionalESPColor={GUI_DimensionalESPColor.r},{GUI_DimensionalESPColor.g},{GUI_DimensionalESPColor.b},{GUI_DimensionalESPColor.a}",
                $"GUI_TracerESPColor={GUI_TracerESPColor.r},{GUI_TracerESPColor.g},{GUI_TracerESPColor.b},{GUI_TracerESPColor.a}",
                $"GUI_NameESPColor={GUI_NameESPColor.r},{GUI_NameESPColor.g},{GUI_NameESPColor.b},{GUI_NameESPColor.a}",
                $"BoxESP_CustomColor={BoxESP.CustomColor.r},{BoxESP.CustomColor.g},{BoxESP.CustomColor.b},{BoxESP.CustomColor.a}",
            };
            File.WriteAllLines(ConfigPath, lines);
            XConsole.Log("Config", "Config saved.");
        }

        private static Color ParseColor(string value)
        {
            var parts = value.Split(',');
            if (parts.Length != 4)
                return Color.white;

            if (float.TryParse(parts[0], out float r) &&
                float.TryParse(parts[1], out float g) &&
                float.TryParse(parts[2], out float b) &&
                float.TryParse(parts[3], out float a))
            {
                return new Color(r, g, b, a);
            }
            return Color.white;
        }
    }
}