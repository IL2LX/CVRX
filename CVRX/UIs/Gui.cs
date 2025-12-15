using CVRX.Mods.ESP;
using CVRX.Mods.Exploits;
using CVRX.Mods.Misc;
using CVRX.Mods.Movements;
using UnityEngine;

namespace CVRX.UIs
{
    internal class Gui : MonoBehaviour
    {
        private Rect windowRect = new Rect(10, 10, 900, 465);
        private int selectedTab = 0;
        private Vector2 scrollPos;

        public static bool menuOpen = false;

        private readonly string[] tabs =
        {
            "Self", "ESP", "Exploits", "Spawner", "Misc", "Settings"
        };

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
                menuOpen = !menuOpen;
            if (menuOpen)
            {
                if (Cursor.lockState != CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.None;
                if (!Cursor.visible)
                    Cursor.visible = true;
            }
        }

        void OnGUI()
        {
            if (!menuOpen)
                return;
            GUI.backgroundColor = ConfManager.GUI_BackgroundColor;
            GUI.color = Color.white;
            GUI.contentColor = Color.white;
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
                Event.current.Use();
            windowRect = GUI.Window(0, windowRect, DrawWindow, "CVRX");
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(120));
            for (int i = 0; i < tabs.Length; i++)
            {
                GUI.backgroundColor = (i == selectedTab) ? new Color(1f, 0.3f, 0.3f) : new Color(0.8f, 0.1f, 0.1f);
                if (GUILayout.Button(tabs[i], GUILayout.Height(40)))
                {
                    selectedTab = i;
                }
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            GUI.backgroundColor = new Color(0.7f, 0.05f, 0.05f);
            GUILayout.BeginVertical("box");
            GUI.backgroundColor = new Color(0.6f, 0.05f, 0.05f);
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(420));
            GUI.backgroundColor = new Color(0.8f, 0.1f, 0.1f);
            switch (tabs[selectedTab])
            {
                case "Self":
                    GUILayout.Label("Self Options:");
                    Flight.Render();
                    JetPack.Render();
                    SpeedHack.Render();
                    RayCastTP.Render();
                    PosSaver.Render();
                    SpinBot.Render();
                    break;

                case "ESP":
                    GUILayout.Label("ESP Options:");
                    GuiEsp.Render();
                    GUIRadar.Render();
                    GUILayout.Label("InGame Options:");
                    BoxESP.Render();
                    LineESP.Render();
                    LegacyESP.Render();
                    break;

                case "Exploits":
                    GUILayout.Label("Exploits Options:");
                    FakeSerialize.Render();
                    GUILayout.Label("Pickups Options:");
                    BeyBladePickup.Render();
                    ItemMesser.Render();
                    break;

                case "Spawner":
                    Spawner.Render();
                    break;

                case "Misc":
                    GUILayout.Label("Misc Options:");
                    Flashlight.Render();
                    AssetDumper.Render();
                    AviLogger.Render();
                    break;

                case "Settings":
                    ConfManager.Render();
                    break;

                default:
                    GUILayout.Label("Coming soon...");
                    break;
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUI.DragWindow(new Rect(0, 0, 900, 25));
        }
    }
}
