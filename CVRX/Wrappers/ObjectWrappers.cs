using ABI.CCK.Components;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Player;
using DarkRift;
using HighlightPlus;
using System;
using UnityEngine;

namespace CVRX.Wrappers
{
    internal static class ObjectWrappers
    {
        public static CVRPickupObject GetCVRPickupObject(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<CVRPickupObject>();
        }
        public static CVRObjectSync GetCVRObjectSync(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<CVRObjectSync>();
        }
        public static PhysicsInfluencer GetPhysicsInfluencer(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<PhysicsInfluencer>();
        }
        public static CVRSharedPhysicsController GetCVRSharedPhysicsController(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<CVRSharedPhysicsController>();
        }
        public static HighlightEffect GetHighlightEffect(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<HighlightEffect>();
        }
        public static CVRSpawnable GetCVRSpawnable(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<CVRSpawnable>();
        }
        public static CVRAssetInfo GetCVRAssetInfo(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<CVRAssetInfo>();
        }
        public static RCC_CarControllerV3 GetRCCCarControllerV3(this GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetComponent<RCC_CarControllerV3>();
        }
        public static void SpawnProp(string propGuid, Vector3? pos = null, Quaternion? rot = null, bool useTargetLocationGravity = false)
        {
            if (!Utils.PropsAllowed()) return;
            if (pos == null)
                pos = PlayerSetup.Instance.gameObject.transform.position;
            if (rot == null)
                rot = Quaternion.LookRotation(Vector3.ProjectOnPlane((PlayerSetup.Instance.CharacterController.RotationPivot.position - pos.Value).normalized,-pos.Value),-pos.Value);
            XConsole.Log("Prop",$"Trying to spawn prop {propGuid} at {pos} with rotation {rot}");
            using (DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create())
            {
                darkRiftWriter.Write(propGuid);
                darkRiftWriter.Write(pos.Value.x);
                darkRiftWriter.Write(pos.Value.y);
                darkRiftWriter.Write(pos.Value.z);
                darkRiftWriter.Write(rot.Value.x);
                darkRiftWriter.Write(rot.Value.y);
                darkRiftWriter.Write(rot.Value.z);
                darkRiftWriter.Write(1f);
                darkRiftWriter.Write(1f);
                darkRiftWriter.Write(1f);
                darkRiftWriter.Write(0f);
                using (Message message = Message.Create(10050, darkRiftWriter))
                {
                    NetworkManager.Instance.GameNetwork.SendMessage(message, SendMode.Reliable);
                }
            }
        }
    }
}
