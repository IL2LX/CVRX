using CVRX.UIs;
using CVRX.Wrappers;
using System.Collections.Generic;
using UnityEngine;

namespace CVRX.Mods.ESP
{
    internal class BoxESP
    {
        public static List<LineRenderer> BoxRenderers = new List<LineRenderer>();
        public static List<Transform> otherPlayers = new List<Transform>();
        public static Transform localPlayer;
        public static bool BoxpMyESP = false;
        public static bool temp = false;

        public static bool UseRankColor = true;
        public static Color CustomColor;

        public static void Render()
        {
            UIEx.Collapsible("BoxESP", () =>
            {
                temp = UIEx.Checkbox(temp, temp ? "Enabled" : "Disabled", (newState) =>
                {
                    BoxState(newState);
                });
                UseRankColor = UIEx.Checkbox(UseRankColor, UseRankColor ? "Use Rank Color?" : "Use Custom Color?", (newState) =>
                {
                    UseRankColor = newState;
                    UpdateBoxesColor();
                });
                if (!UseRankColor)
                {
                    UIEx.ColorPicker("Custom Box Color", ref CustomColor, (newColor) =>
                    {
                        CustomColor = newColor;
                        UpdateBoxesColor();
                    });
                }
            });
        }

        public static void BoxState(bool s)
        {
            if (s)
            {
                if (localPlayer == null)
                {
                    localPlayer = PlayerWrappers.GetLocalPlayer().transform;
                }
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
                    CreateBoxRenderer(player.PlayerObject.transform, UseRankColor ? player.GetRankColor() : CustomColor);
                }
            }

            BoxpMyESP = true;
        }

        public static void CreateBoxRenderer(Transform target, Color color)
        {
            GameObject boxObj = new GameObject("PlayerBox");
            LineRenderer boxRenderer = boxObj.AddComponent<LineRenderer>();
            boxRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            boxRenderer.startColor = color;
            boxRenderer.endColor = color;
            boxRenderer.startWidth = 0.01f;
            boxRenderer.endWidth = 0.01f;
            boxRenderer.loop = false;
            boxRenderer.positionCount = 16;
            BoxRenderers.Add(boxRenderer);
        }

        public static void UpdateBoxesColor()
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                if (i >= BoxRenderers.Count) continue;

                var player = PlayerWrappers.GetAllPlayers()[i];
                var renderer = BoxRenderers[i];
                if (renderer != null)
                {
                    Color color = UseRankColor ? player.GetRankColor() : CustomColor;
                    renderer.startColor = color;
                    renderer.endColor = color;
                }
            }
        }

        public static void UpdateBox()
        {
            if (!BoxpMyESP) return;
            if (localPlayer == null || otherPlayers.Count == 0) return;

            for (int i = 0; i < otherPlayers.Count; i++)
            {
                var target = otherPlayers[i];
                var renderer = BoxRenderers[i];
                if (target == null || renderer == null) continue;

                Vector3 center = target.position + Vector3.up * 1.0f;
                Vector3 size = new Vector3(0.4f, 1.6f, 0.4f);

                Vector3[] corners = new Vector3[16];

                Vector3 topFrontLeft = center + new Vector3(-size.x, size.y, size.z) * 0.5f;
                Vector3 topFrontRight = center + new Vector3(size.x, size.y, size.z) * 0.5f;
                Vector3 topBackLeft = center + new Vector3(-size.x, size.y, -size.z) * 0.5f;
                Vector3 topBackRight = center + new Vector3(size.x, size.y, -size.z) * 0.5f;
                Vector3 bottomFrontLeft = center + new Vector3(-size.x, -size.y, size.z) * 0.5f;
                Vector3 bottomFrontRight = center + new Vector3(size.x, -size.y, size.z) * 0.5f;
                Vector3 bottomBackLeft = center + new Vector3(-size.x, -size.y, -size.z) * 0.5f;
                Vector3 bottomBackRight = center + new Vector3(size.x, -size.y, -size.z) * 0.5f;

                corners[0] = topFrontLeft;
                corners[1] = topFrontRight;
                corners[2] = topBackRight;
                corners[3] = topBackLeft;
                corners[4] = topFrontLeft;

                corners[5] = bottomFrontLeft;
                corners[6] = bottomFrontRight;
                corners[7] = bottomBackRight;
                corners[8] = bottomBackLeft;
                corners[9] = bottomFrontLeft;

                corners[10] = bottomFrontRight;
                corners[11] = topFrontRight;
                corners[12] = topBackRight;
                corners[13] = bottomBackRight;
                corners[14] = bottomBackLeft;
                corners[15] = topBackLeft;

                renderer.positionCount = corners.Length;
                renderer.SetPositions(corners);
            }
        }

        public static void ClearLines()
        {
            foreach (var lr in BoxRenderers)
            {
                if (lr != null)
                    GameObject.Destroy(lr.gameObject);
            }
            BoxRenderers.Clear();
            BoxpMyESP = false;
        }
    }
}
