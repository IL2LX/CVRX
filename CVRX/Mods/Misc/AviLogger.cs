using CVRX.UIs;
using CVRX.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CVRX.Mods.Misc
{
    internal class AviLogger : IMods
    {
        private static Dictionary<string, (string username, DateTime timestamp)> loggedAvatars= new Dictionary<string, (string, DateTime)>();

        private static readonly string logDirectory = Path.Combine(Environment.CurrentDirectory, "CVRX");
        private static readonly string logFile = Path.Combine(logDirectory, "AvatarLog.txt");
        static string searchQuery = "";

        public void Initialize()
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
            try
            {
                if (!File.Exists(logFile))
                    return;
                var lines = File.ReadAllLines(logFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    string[] parts = line.Split('|');
                    if (parts.Length < 3)
                        continue;
                    if (!DateTime.TryParse(parts[0], out DateTime timestamp))
                        timestamp = DateTime.MinValue;
                    string username = parts[1];
                    string avatarId = parts[2];
                    if (!loggedAvatars.ContainsKey(avatarId))
                        loggedAvatars.Add(avatarId, (username, timestamp));
                }
                XConsole.Log("AviLogger", $"Loaded {loggedAvatars.Count} logged avatars.");
            }
            catch (Exception ex)
            {
                XConsole.Log("AviLogger", $"Load error: {ex}", true);
            }
        }

        public static void Render()
        {
            UIEx.Collapsible("AviLogger", () =>
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Search avatars:");
                searchQuery = GUILayout.TextField(searchQuery, 200);
                var filtered = string.IsNullOrWhiteSpace(searchQuery)
                    ? loggedAvatars
                    : loggedAvatars
                        .Where(kvp => kvp.Value.username.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0
                                   || kvp.Key.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                if (filtered.Count == 0)
                {
                    GUILayout.Label("No avatars found.");
                }
                else
                {
                    foreach (var kvp in filtered)
                    {
                        string avatarId = kvp.Key;
                        string username = kvp.Value.username;
                        DateTime timestamp = kvp.Value.timestamp;

                        if (GUILayout.Button($"[{timestamp:yyyy-MM-dd HH:mm}] {username} ({avatarId})"))
                        {
                            GeneralWrappers.SwitchAvatar(avatarId);
                        }
                    }
                }
                GUILayout.EndVertical();
            });
        }

        public static bool LogAvatar(string username, string avatarId)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(avatarId))
                return false;

            if (loggedAvatars.ContainsKey(avatarId))
                return false;

            DateTime timestamp = DateTime.Now;
            loggedAvatars.Add(avatarId, (username, timestamp));
            Save();
            return true;
        }

        private static void Save()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var kvp in loggedAvatars)
                {
                    string avatarId = kvp.Key;
                    string username = kvp.Value.username;
                    string timestamp = kvp.Value.timestamp.ToString("yyyy-MM-dd HH:mm");
                    lines.Add($"{timestamp}|{username}|{avatarId}");
                }
                File.WriteAllLines(logFile, lines);
            }
            catch (Exception ex)
            {
                XConsole.Log("AviLogger", $"Save error: {ex}", true);
            }
        }
    }
}
