using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRX.UIs
{
    internal class UIEx
    {
        private static Dictionary<string, bool> expandedStates = new Dictionary<string, bool>();

        public static void Collapsible(string label, Action renderContents,bool DefaultExpend = false)
        {
            if (!expandedStates.ContainsKey(label))
                expandedStates[label] = DefaultExpend;
            bool expanded = expandedStates[label];
            if (GUILayout.Button(expanded ? $"▼ {label}" : $"► {label}", GUILayout.ExpandWidth(true)))
            {
                expanded = !expanded;
                expandedStates[label] = expanded;
            }
            if (expanded && renderContents != null)
            {
                GUILayout.Space(6);
                GUILayout.BeginVertical("box");
                renderContents.Invoke();
                GUILayout.EndVertical();
            }
        }

        public static bool Checkbox(bool currentValue, string option, Action<bool> onChange = null)
        {
            bool newValue = GUILayout.Toggle(currentValue, option);
            if (newValue != currentValue)
                onChange?.Invoke(newValue);
            return newValue;
        }

        public static void InputInt(string label, ref int var)
        {
            int newvar = var;
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            if (int.TryParse(GUILayout.TextField(var.ToString()), out newvar))
                var = newvar;
            GUILayout.EndHorizontal();
        }

        public static void ColorPicker(string label, ref Color col, Action<Color> onChange)
        {
            Color oldColor = col;
            GUILayout.Label($"{label} (R:{Mathf.RoundToInt(col.r * 255f)}, G:{Mathf.RoundToInt(col.g * 255f)}, B:{Mathf.RoundToInt(col.b * 255f)})");
            GUILayout.BeginHorizontal();
            col.r = GUILayout.HorizontalSlider(col.r, 0f, 1f, GUILayout.Width(80));
            col.g = GUILayout.HorizontalSlider(col.g, 0f, 1f, GUILayout.Width(80));
            col.b = GUILayout.HorizontalSlider(col.b, 0f, 1f, GUILayout.Width(80));
            Color prev = GUI.color;
            GUI.color = col;
            GUILayout.Box("", GUILayout.Width(50), GUILayout.Height(20));
            GUI.color = prev;
            GUILayout.EndHorizontal();

            if (col != oldColor && onChange != null)
                onChange.Invoke(col);
        }

        protected static bool clipTest(float p, float q, ref float u1, ref float u2)
        {
            bool result = true;
            if ((double)p < 0.0)
            {
                float num = q / p;
                if ((double)num > (double)u2)
                {
                    result = false;
                }
                else if ((double)num > (double)u1)
                {
                    u1 = num;
                }
            }
            else if ((double)p > 0.0)
            {
                float num2 = q / p;
                if ((double)num2 < (double)u1)
                {
                    result = false;
                }
                else if ((double)num2 < (double)u2)
                {
                    u2 = num2;
                }
            }
            else if ((double)q < 0.0)
            {
                result = false;
            }
            return result;
        }

        protected static bool segmentRectIntersection(Rect bounds, ref Vector2 p1, ref Vector2 p2)
        {
            float num = 0f;
            float num2 = 1f;
            float num3 = p2.x - p1.x;
            if (clipTest(-num3, p1.x - bounds.xMin, ref num, ref num2) && clipTest(num3, bounds.xMax - p1.x, ref num, ref num2))
            {
                float num4 = p2.y - p1.y;
                if (clipTest(-num4, p1.y - bounds.yMin, ref num, ref num2) && clipTest(num4, bounds.yMax - p1.y, ref num, ref num2))
                {
                    if ((double)num2 < 1.0)
                    {
                        p2.x = p1.x + num2 * num3;
                        p2.y = p1.y + num2 * num4;
                    }
                    if ((double)num > 0.0)
                    {
                        p1.x += num * num3;
                        p1.y += num * num4;
                    }
                    return true;
                }
            }
            return false;
        }

        public static void BeginGroup(Rect position)
        {
            clippingEnabled = true;
            clippingBounds = new Rect(0f, 0f, position.width, position.height);
            GUI.BeginGroup(position);
        }

        public static void EndGroup()
        {
            GUI.EndGroup();
            clippingBounds = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
            clippingEnabled = false;
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
        {
            if (clippingEnabled && !segmentRectIntersection(clippingBounds, ref pointA, ref pointB))
            {
                return;
            }
            GL.Begin(1);
            GLMat.SetPass(0);
            GL.Color(color);
            GL.Vertex3(pointA.x, pointA.y, 0f);
            GL.Vertex3(pointB.x, pointB.y, 0f);
            GL.End();
        }

        public static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] array = new Color[width * height];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = col;
            }
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.SetPixels(array);
            texture2D.Apply();
            return texture2D;
        }

        private static Material GLMat = new Material(Shader.Find("Hidden/Internal-Colored"));

        protected static bool clippingEnabled;

        protected static Rect clippingBounds;

        protected static Material lineMaterial;
    }
}
