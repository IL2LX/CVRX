using CVRX.UIs;
using CVRX.Wrappers;
using System.Collections.Generic;
using UnityEngine;

namespace CVRX.Mods.ESP
{
    internal class LineESP
    {
        public static List<LineRenderer> lineRenderers = new List<LineRenderer>();
        public static List<Transform> otherPlayers = new List<Transform>();
        public static Transform localPlayer;
        public static bool LinepMyESP = false;
        public static bool temp = false;

        public static bool UseRankColor = true;
        public static Color CustomColor = Color.green;

        public static void Render()
        {
            UIEx.Collapsible("LineESP", () =>
            {
                temp = UIEx.Checkbox(temp, temp ? "Enabled" : "Disabled", (newState) =>
                {
                    LineState(!newState);
                });
                UseRankColor = UIEx.Checkbox(UseRankColor, UseRankColor ? "Use Rank Color" : "Use Custom Color", (newState) =>
                {
                    UseRankColor = newState;
                    UpdateLinesColor();
                });
                if (!UseRankColor)
                {
                    UIEx.ColorPicker("Custom Line Color", ref CustomColor, (newColor) =>
                    {
                        CustomColor = newColor;
                        UpdateLinesColor();
                    });
                }
            });
        }

        public static void LineState(bool s)
        {
            if (s)
            {
                if (localPlayer == null)
                    localPlayer = PlayerWrappers.GetLocalPlayer().transform;
                FindOtherPlayers();
            }
            else
            {
                ClearLines();
            }
        }

        public static void FindOtherPlayers()
        {
            otherPlayers.Clear();
            ClearLines();
            foreach (var player in PlayerWrappers.GetAllPlayers())
            {
                if (player.PlayerObject.transform != localPlayer)
                {
                    otherPlayers.Add(player.PlayerObject.transform);
                    CreateLineRenderer(player.PlayerObject.transform, UseRankColor ? player.GetRankColor() : CustomColor);
                }
            }
            LinepMyESP = true;
        }

        public static void CreateLineRenderer(Transform target, Color color)
        {
            GameObject lineObj = new GameObject("PlayerLine");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.positionCount = 2;
            lineRenderers.Add(lineRenderer);
        }

        public static void UpdateLines()
        {
            if (!LinepMyESP) return;
            if (localPlayer == null || otherPlayers.Count == 0) return;
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                if (otherPlayers[i] == null || lineRenderers[i] == null) continue;

                lineRenderers[i].SetPosition(0, localPlayer.position);
                lineRenderers[i].SetPosition(1, otherPlayers[i].position);
            }
        }

        public static void UpdateLinesColor()
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                if (i >= lineRenderers.Count) continue;
                var player = PlayerWrappers.GetAllPlayers()[i];
                var renderer = lineRenderers[i];
                if (renderer != null)
                {
                    Color color = UseRankColor ? player.GetRankColor() : CustomColor;
                    renderer.startColor = color;
                    renderer.endColor = color;
                }
            }
        }

        public static void ClearLines()
        {
            foreach (var lr in lineRenderers)
            {
                if (lr != null)
                    GameObject.Destroy(lr.gameObject);
            }
            lineRenderers.Clear();
            LinepMyESP = false;
        }
    }
}
