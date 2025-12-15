using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.UI;

namespace CVRX.Wrappers
{
    internal class HudWrappers
    {
        public static void TriggerAlert(string Title, string Content)
        {
            ViewManager.Instance.TriggerAlert(Title, Content, 1, true);
        }
        public static void TriggerPushAlter(string content)
        {
            ViewManager.Instance.TriggerPushNotification(content, 5);
        }
        public static void TriggerConfirm(string HeadLineText, string content, string id, bool closeLoad = true, bool forceOpenMenu = true)
        {
            ViewManager.Instance.TriggerConfirm(HeadLineText, content, id, closeLoad, forceOpenMenu);
        }
        public static void TriggerDropText(string Title, string content)
        {
            CohtmlHud.Instance.ViewDropText("CVRX", Title, content, null, false);
        }
    }
}
