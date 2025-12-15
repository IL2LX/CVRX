using ABI_RC.Systems.GameEventSystem;
using CVRX.Handlers;

namespace CVRX.Hooks.List
{
    internal class QuickMenuHook : IHook
    {
        public void Initialize()
        {
            CVRGameEventSystem.QuickMenu.OnOpen.AddListener(() => Hook(true));
            CVRGameEventSystem.QuickMenu.OnClose.AddListener(() => Hook(false));
        }
        public static void Hook(bool s)
        {
            if (s)
            {
                AudioHandler.PlayFromREmbed(Properties.Resources.qmopen);
            }
            else
            {
                AudioHandler.PlayFromREmbed(Properties.Resources.qmclose);
            }
        }
    }
}
