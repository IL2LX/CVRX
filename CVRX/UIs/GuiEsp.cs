using ABI_RC.Core.Player;
using CVRX.Wrappers;
using System;
using UnityEngine;
using Color = UnityEngine.Color;
using Texture2D = UnityEngine.Texture2D;

namespace CVRX.UIs
{
    internal class GuiEsp : MonoBehaviour
    {
        public static bool BoxESP = false;
        public static bool DimensionalESP = false;
        public static bool DrawTracingLines = false;
        public static bool DrawDistanceText = false;

        public void OnGUI()
        {
            if (BoxESP)
            {
                Draw3DBoundingBoxes();
                DrawNames();
            }

            if (DimensionalESP)
            {
                Draw2DBoundingBoxes();
                DrawNames();
            }

            if (DrawDistanceText)
                DrawDistance();

            if (DrawTracingLines)
            {
                DrawTracelines();
            }
        }

        public static void Render()
        {
            UIEx.Collapsible("CS:GO ESP", () => 
            {
                BoxESP = UIEx.Checkbox(BoxESP, BoxESP ? "BoxESP Enabled" : "BoxESP Disabled", (newState) =>
                {
                    BoxESP = newState;
                });
                if (BoxESP)
                    UIEx.ColorPicker("Box Color", ref ConfManager.GUI_BoxESPColor, (newColor) => { ConfManager.GUI_BoxESPColor = newColor; });
                DimensionalESP = UIEx.Checkbox(DimensionalESP, DimensionalESP ? "DimensionalESP Enabled" : "DimensionalESP Disabled", (newState) =>
                {
                    DimensionalESP = newState;
                });
                if (DimensionalESP)
                    UIEx.ColorPicker("2D Box Color", ref ConfManager.GUI_DimensionalESPColor, (newColor) => { ConfManager.GUI_DimensionalESPColor = newColor; });
                DrawDistanceText = UIEx.Checkbox(DrawDistanceText, DrawDistanceText ? "DistanceESP Enabled" : "DistanceESP Disabled", (newState) =>
                {
                    DrawDistanceText = newState;
                });
                DrawTracingLines = UIEx.Checkbox(DrawTracingLines, DrawTracingLines ? "TracerESP Enabled" : "TracerESP Disabled", (newState) =>
                {
                    DrawTracingLines = newState;
                });
                if (DrawTracingLines)
                    UIEx.ColorPicker("Tracer Color", ref ConfManager.GUI_TracerESPColor, (newColor) => { ConfManager.GUI_TracerESPColor = newColor; });
                GUILayout.Space(3);
                UIEx.ColorPicker("Name Color", ref ConfManager.GUI_NameESPColor, (newColor) => { ConfManager.GUI_NameESPColor = newColor; });
            });
        }

        public void DrawTracelines()
        {
            CVRPlayerEntity[] allPlayers = PlayerWrappers.GetAllPlayers();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                PuppetMaster putmaster = allPlayers[i].GetPuppetMaster();
                if (!putmaster.IsLocalPlayer)
                {
                    Vector3 vector = Camera.main.WorldToScreenPoint(allPlayers[i].PlayerObject.transform.position);
                    if (vector.z > 0.0)
                    {
                        Vector3 vector2 = GUIUtility.ScreenToGUIPoint(vector);
                        vector2.y = Screen.height - vector2.y;
                        UIEx.DrawLine(new Vector2((Screen.width / 2), (Screen.height / 2)), vector2, ConfManager.GUI_TracerESPColor);
                    }
                }
            }
        }

        public void Draw3DBoundingBoxes()
        {
            foreach (CVRPlayerEntity player in PlayerWrappers.GetAllPlayers())
            {
                PuppetMaster putmaster = player.GetPuppetMaster();
                if (!putmaster.IsLocalPlayer)
                {
                    Vector3 position = player.PlayerObject.transform.position;
                    Vector3 vector = position;
                    vector.y += 1.7f;
                    Vector3 min = new Vector3(position.x - 0.5f, position.y, position.z - 0.5f);
                    Vector3 max = new Vector3(position.x + 0.5f, vector.y, position.z + 0.5f);
                    this.Draw3DBox(position, min, max, Quaternion.Euler(player.PlayerObject.transform.rotation.eulerAngles), ConfManager.GUI_BoxESPColor);
                }
            }
        }

        public void Draw3DBox(Vector3 center, Vector3 min, Vector3 max, Quaternion rot, Color c)
        {
            Vector3 point = new Vector3(min.x, max.y, min.z);
            Vector3 point2 = new Vector3(max.x, max.y, min.z);
            Vector3 point3 = new Vector3(min.x, max.y, max.z);
            Vector3 point4 = new Vector3(min.x, min.y, max.z);
            Vector3 point5 = new Vector3(max.x, min.y, min.z);
            Vector3 point6 = new Vector3(max.x, min.y, max.z);
            min = RotateAroundPoint(min, center, rot);
            max = RotateAroundPoint(max, center, rot);
            Vector3 position = RotateAroundPoint(point, center, rot);
            Vector3 position2 = RotateAroundPoint(point2, center, rot);
            Vector3 position3 = RotateAroundPoint(point3, center, rot);
            Vector3 position4 = RotateAroundPoint(point4, center, rot);
            Vector3 position5 = RotateAroundPoint(point5, center, rot);
            Vector3 position6 = RotateAroundPoint(point6, center, rot);
            Vector3 vector = Camera.main.WorldToScreenPoint(min);
            Vector3 vector2 = Camera.main.WorldToScreenPoint(max);
            Vector3 vector3 = Camera.main.WorldToScreenPoint(position);
            Vector3 vector4 = Camera.main.WorldToScreenPoint(position2);
            Vector3 vector5 = Camera.main.WorldToScreenPoint(position3);
            Vector3 vector6 = Camera.main.WorldToScreenPoint(position4);
            Vector3 vector7 = Camera.main.WorldToScreenPoint(position5);
            Vector3 vector8 = Camera.main.WorldToScreenPoint(position6);
            if (vector.z <= 0.0 || vector2.z <= 0.0 || vector3.z <= 0.0 || vector4.z <= 0.0 || vector5.z <= 0.0 || vector6.z <= 0.0 || vector7.z <= 0.0 || vector8.z <= 0.0)
            {
                return;
            }
            Vector3 vector9 = GUIUtility.ScreenToGUIPoint(vector);
            vector9.y = Screen.height - vector9.y;
            Vector3 vector10 = GUIUtility.ScreenToGUIPoint(vector2);
            vector10.y = Screen.height - vector10.y;
            Vector3 vector11 = GUIUtility.ScreenToGUIPoint(vector3);
            vector11.y = Screen.height - vector11.y;
            Vector3 vector12 = GUIUtility.ScreenToGUIPoint(vector4);
            vector12.y = Screen.height - vector12.y;
            Vector3 vector13 = GUIUtility.ScreenToGUIPoint(vector5);
            vector13.y = Screen.height - vector13.y;
            Vector3 vector14 = GUIUtility.ScreenToGUIPoint(vector6);
            vector14.y = Screen.height - vector14.y;
            Vector3 vector15 = GUIUtility.ScreenToGUIPoint(vector7);
            vector15.y = Screen.height - vector15.y;
            Vector3 vector16 = GUIUtility.ScreenToGUIPoint(vector8);
            vector16.y = Screen.height - vector16.y;
            UIEx.DrawLine(vector9, vector15, c);
            UIEx.DrawLine(vector9, vector14, c);
            UIEx.DrawLine(vector16, vector15, c);
            UIEx.DrawLine(vector16, vector14, c);
            UIEx.DrawLine(vector11, vector12, c);
            UIEx.DrawLine(vector11, vector13, c);
            UIEx.DrawLine(vector10, vector12, c);
            UIEx.DrawLine(vector10, vector13, c);
            UIEx.DrawLine(vector9, vector11, c);
            UIEx.DrawLine(vector14, vector13, c);
            UIEx.DrawLine(vector15, vector12, c);
            UIEx.DrawLine(vector16, vector10, c);
        }

        public void Draw2DBoundingBoxes()
        {
            CVRPlayerEntity[] allPlayers = PlayerWrappers.GetAllPlayers();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                PuppetMaster putmaster = allPlayers[i].GetPuppetMaster();
                if (!putmaster.IsLocalPlayer)
                {
                    Vector3 position = allPlayers[i].PlayerObject.transform.position;
                    Vector3 position2 = position;
                    position2.y += 1.7f;
                    Vector3 vector = Camera.main.WorldToScreenPoint(position);
                    Vector3 vector2 = Camera.main.WorldToScreenPoint(position2);
                    if (vector.z > 0.0 && vector2.z > 0.0)
                    {
                        Vector3 vector3 = GUIUtility.ScreenToGUIPoint(vector);
                        vector3.y = Screen.height - vector3.y;
                        Vector3 vector4 = GUIUtility.ScreenToGUIPoint(vector2);
                        vector4.y = Screen.height - vector4.y;
                        float num = (float)((double)Math.Abs(vector3.y - vector4.y) / 2.20000004768372 / 2.0);
                        Vector3 v = new Vector3(vector4.x - num, vector4.y);
                        Vector3 v2 = new Vector3(vector4.x + num, vector4.y);
                        Vector3 v3 = new Vector3(vector4.x - num, vector3.y);
                        Vector3 v4 = new Vector3(vector4.x + num, vector3.y);
                        UIEx.DrawLine(v, v2, ConfManager.GUI_DimensionalESPColor);
                        UIEx.DrawLine(v, v3, ConfManager.GUI_DimensionalESPColor);
                        UIEx.DrawLine(v3, v4, ConfManager.GUI_DimensionalESPColor);
                        UIEx.DrawLine(v2, v4, ConfManager.GUI_DimensionalESPColor);
                    }
                }
            }
        }

        public void DrawNames()
        {
            foreach (CVRPlayerEntity player in PlayerWrappers.GetAllPlayers())
            {
                PuppetMaster putmaster = player.GetPuppetMaster();
                if (!putmaster.IsLocalPlayer)
                {
                    Vector3 position = player.PlayerObject.transform.position;
                    position.y += 2.5f;
                    Vector3 vector = Camera.main.WorldToScreenPoint(position);
                    if (vector.z > 0.0)
                    {
                        Vector3 vector2 = GUIUtility.ScreenToGUIPoint(vector);
                        vector2.y = Screen.height - vector2.y;
                        GUI.Label(new Rect(vector2.x, vector2.y, 105f, 25f), $"<size=10><b><color=#{ColorUtility.ToHtmlStringRGBA(ConfManager.GUI_NameESPColor)}>" + player.GetUsername() + "</color></b></size>");
                    }
                }
            }
        }

        public void DrawDistance()
        {
            foreach (CVRPlayerEntity player in PlayerWrappers.GetAllPlayers())
            {
                PuppetMaster pm = player.GetPuppetMaster();
                if (pm.IsLocalPlayer) continue;

                Vector3 pos = player.PlayerObject.transform.position;
                Vector3 screen = Camera.main.WorldToScreenPoint(pos + new Vector3(0, 2.0f, 0));

                if (screen.z > 0)
                {
                    Vector3 gui = GUIUtility.ScreenToGUIPoint(screen);
                    gui.y = Screen.height - gui.y;

                    float dist = Vector3.Distance(Camera.main.transform.position, pos);
                    GUI.Label(new Rect(gui.x, gui.y + 15f, 125, 20),
                        $"<size=9><color=white>{dist:F1}m</color></size>");
                }
            }
        }

        public static Texture2D CreatePlainTexture(float r, float g, float b, float a)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.SetPixel(0, 0, new Color(r, g, b, a));
            texture2D.Apply();
            return texture2D;
        }

        public static Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion angle)
        {
            return angle * (point - pivot) + pivot;
        }
    }
}
