using ABI_RC.Core.Savior;
using ABI_RC.Core.Util;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace CVRX.Wrappers
{
    public static class Utils
    {

        public static bool PropsAllowed()
        {
            if (!CVRSyncHelper.IsConnectedToGameNetwork())
            {
                XConsole.Log("Prop", "Not connected to an online Instance.", true);
                HudWrappers.TriggerDropText("Prop - Cannot spawn prop", "Not connected to an online Instance.");
                return false;
            }
            else if (!MetaPort.Instance.worldAllowProps)
            {
                XConsole.Log("Prop", "Props are not allowed in this world.", true);
                HudWrappers.TriggerDropText("Prop - Cannot spawn prop", "Props are not allowed in this world.");
                return false;
            }
            else if (!MetaPort.Instance.settings.GetSettingsBool("ContentFilterPropsEnabled", false))
            {
                XConsole.Log("Prop", "Props are disabled in content filter.", true);
                HudWrappers.TriggerDropText("Prop - Cannot spawn prop", "Props are disabled in content filter.");
                return false;
            }
            return true;
        }

        public static Image LoadImageFromResource(this Image Image, string Name, int pixels = 200, Vector4 border = new Vector4())
        {
            var resourcePath = $"CVRX.Res.{Name}.png";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (stream == null)
            {
                XConsole.Log("Utils:ImageLoader",$"Failed to find texture {Name}");
                return null;
            }
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Texture2D Texture = new Texture2D(1, 1);
            ImageConversion.LoadImage(Texture, ms.ToArray());
            Image.sprite = Sprite.Create(Texture, new Rect(0, 0, Texture.width, Texture.height), new Vector2(0, 0), pixels, 1000u, SpriteMeshType.FullRect, border, false);
            return Image;
        }
    }
}
