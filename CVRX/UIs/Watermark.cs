using UnityEngine;

namespace CVRX.UIs
{
    internal class Watermark : MonoBehaviour
    {
        private static float rainbowHue = 0.0f;
        private static GUIStyle labelStyle;
        private static GUIStyle textStyle;
        private static readonly string staticText = "CVRX";

        public void Init()
        {
            labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = Color.white }
            };

            textStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };
        }
        void OnGUI()
        {
            if (labelStyle == null || textStyle == null)
                Init();
            const float barHeight = 30f;
            float posX = 10f;
            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect(0, 0, Screen.width, barHeight), GUIContent.none);
            float hue = rainbowHue;
            float posY = (barHeight - labelStyle.lineHeight) / 2f;
            for (int i = 0; i < staticText.Length; i++)
            {
                string ch = staticText[i].ToString();
                Color color = Color.HSVToRGB(hue, 1f, 1f);
                labelStyle.normal.textColor = color;
                Vector2 charSize = labelStyle.CalcSize(new GUIContent(ch));
                GUI.Label(new Rect(posX, posY, charSize.x, charSize.y), ch, labelStyle);
                posX += charSize.x;
                hue += 0.08f;
                if (hue > 1f) hue -= 1f;
            }
            rainbowHue += 0.0015f;
            if (rainbowHue > 1f) rainbowHue -= 1f;
            float fps = 1.0f / Time.deltaTime;
            string infoText = $" | FPS: {fps:F2} | CHack.cc";
            GUI.Label(new Rect(posX + 10, 7f, Screen.width, barHeight), infoText, textStyle);
        }
    }
}
