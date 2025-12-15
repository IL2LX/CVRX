/*using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRX.UIs
{
    internal class GUIConsole
    {
        public static readonly List<ConsoleEntry> entries = new List<ConsoleEntry>();
        private static GUIStyle style;
        private static string input = ""; // Current input text
        public const int maxLines = 50;

        // Dictionary to hold commands
        private static readonly Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public static void Awake()
        {
            style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.richText = true;

            // Register default commands
            RegisterCommand("help", HelpCommand, "Shows all available commands.");
            RegisterCommand("clear", ClearCommand, "Clears the console.");

            LogInfo("Console", "Gadon. Type /help for commands.");
        }

        public static void Render()
        {
            GUILayout.Label("Console:");
            GUILayout.Space(5);

            for (int i = 0; i < entries.Count; i++)
            {
                GUILayout.Label(entries[i].Formatted, style);
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            input = GUILayout.TextField(input, GUILayout.MinWidth(200));

            if (GUILayout.Button("Enter", GUILayout.Width(60)) || Event.current.isKey && Event.current.keyCode == KeyCode.Return)
            {
                HandleInput(input);
                input = "";
            }
            GUILayout.EndHorizontal();

            // Display tooltip if hovering over a command
            if (!string.IsNullOrWhiteSpace(input) && input.StartsWith("/"))
            {
                string cmdName = input.Substring(1).Split(' ')[0].ToLower();
                if (commands.TryGetValue(cmdName, out Command cmd) && !string.IsNullOrEmpty(cmd.Tooltip))
                {
                    GUI.Label(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 300, 40), cmd.Tooltip, style);
                }
            }
        }

        private static void HandleInput(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
                return;

            LogInfo("Input", inputText);

            if (inputText.StartsWith("/"))
            {
                string[] split = inputText.Substring(1).Split(' ');
                string cmd = split[0].ToLower();
                string[] args = new string[split.Length - 1];
                Array.Copy(split, 1, args, 0, args.Length);

                if (commands.TryGetValue(cmd, out Command command))
                {
                    try
                    {
                        command.Action.Invoke(args);
                    }
                    catch (Exception ex)
                    {
                        LogError("Console", $"Error executing command {cmd}: {ex.Message}");
                    }
                }
                else
                {
                    LogWarn("Console", $"Unknown command: {cmd}");
                }
            }
        }

        public static void RegisterCommand(string name, Action<string[]> action, string tooltip = "")
        {
            if (!commands.ContainsKey(name))
            {
                commands.Add(name, new Command { Action = action, Tooltip = tooltip });
            }
        }

        // Logging methods
        public static void LogInfo(string module, string message) => Log(module, message, "#00FF00");
        public static void LogWarn(string module, string message) => Log(module, message, "#FFFF00");
        public static void LogError(string module, string message) => Log(module, message, "#FF0000");

        private static void Log(string module, string message, string color)
        {
            DateTime now = DateTime.Now;
            string formatted = $"[<color={color}>{now:HH:mm:ss}</color>] " +
                               $"[<color=red>CVRX</color>] " +
                               $"[<color={color}>{module}</color>] {message}";

            entries.Add(new ConsoleEntry { Formatted = formatted });

            if (entries.Count > maxLines)
                entries.RemoveAt(0);
        }

        // Default commands
        private static void HelpCommand(string[] args)
        {
            LogInfo("Console", "Available commands:");
            foreach (var cmd in commands)
            {
                LogInfo("Console", $"/{cmd.Key} - {cmd.Value.Tooltip}");
            }
        }

        private static void ClearCommand(string[] args)
        {
            entries.Clear();
            LogInfo("Console", "Console cleared.");
        }

        public struct ConsoleEntry
        {
            public string Formatted;
        }

        private struct Command
        {
            public Action<string[]> Action;
            public string Tooltip;
        }
    }
}
*/