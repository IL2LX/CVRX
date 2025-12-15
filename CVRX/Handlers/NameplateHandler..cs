using ABI_RC.Core.Networking.IO.Social;
using ABI_RC.Core.Player;
using CVRX.Wrappers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CVRX.Handlers
{
    internal class NameplateHandler : MonoBehaviour
    {
        public TextMeshProUGUI Nametag;
        public Color BackgroundColor;
        public Color FontColor;

        public void Start()
        {
            var Player = CVRPlayerManager.Instance.GetPlayer(transform.parent.gameObject.name);
            if (Player == null) return;
            bool IsFriend = Friends.FriendsWith(Player.GetUserID());
            BackgroundColor = Player.GetRankColor();
            FontColor = IsFriend ? Color.yellow : BackgroundColor;

            GameObject SpecialTag = transform.Find("Canvas/Content/Image/Image").gameObject;
            SpecialTag.GetComponent<Image>().enabled = false;

            GameObject NametagObject = transform.Find("Canvas/Content/TMP:Username").gameObject;
            Nametag = NametagObject.GetComponent<TextMeshProUGUI>();
            Nametag.color = new Color(FontColor.r, FontColor.g, FontColor.b, 0.6f);
        }
    }
}
