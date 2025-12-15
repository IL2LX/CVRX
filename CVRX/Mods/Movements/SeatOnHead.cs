using ABI_RC.Core.Player;
using BTKUILib;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class SeatOnHead
    {
        public static Vector3 normalGravity = Physics.gravity;
        public static bool IsEnable = false;
        public static bool TempVal = false;
        public static Transform TargetPlayer;

        public static void State(bool s)
        {
            if (s)
            {
                TargetPlayer = QuickMenuAPI.SelectedPlayerEntity.PlayerGameObject.transform;
                IsEnable = true;
            }
            else
            {
                IsEnable = false;
                TargetPlayer = null;
            }
        }

        public static void Update()
        {
            try
            {
                if (IsEnable && PlayerSetup.Instance != null)
                {
                    if (TargetPlayer != null)
                    {
                        var animator = TargetPlayer.GetComponentInChildren<Animator>();
                        if (animator != null)
                        {
                            var head = animator.GetBoneTransform(HumanBodyBones.Head);
                            if (head != null)
                            {
                                Vector3 offset = new Vector3(0, 0.2f, 0);
                                PlayerSetup.Instance.transform.position = head.position + offset;
                                Physics.gravity = Vector3.zero;
                            }
                        }
                    }
                }
                else
                {
                    Physics.gravity = normalGravity;
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    IsEnable = false;
                    TargetPlayer = null;
                }
            }
            catch { }
        }
    }
}
