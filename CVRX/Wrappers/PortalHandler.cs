using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Util;
using DarkRift;
using System;
using UnityEngine;

namespace CVRX.Wrappers
{
    internal class PortalHandler
    {
        public static void KillAllPortals()
        {
            foreach (CVRPortalManager Item in GameObject.FindObjectsOfType<CVRPortalManager>())
            {
                Item.Despawn();
            }
        }

        public static void PortalToRaycast(string InstanceID)
        {
            if (Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                CVRSyncHelper.SpawnPortal(InstanceID, hit.point.x, hit.point.y, hit.point.z);
            }
        }

        public static void SpawnPortal(string InstanceID, GameObject ObjectToSpawnAt)
        {
            SpawnPortal(InstanceID, ObjectToSpawnAt.transform.position);
        }

        public static void SpawnPortal(string InstanceID, Vector3 Position)
        {
            using (DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create())
            {
                darkRiftWriter.Write(InstanceID);
                darkRiftWriter.Write(Position.x);
                darkRiftWriter.Write(Position.y);
                darkRiftWriter.Write(Position.z);
                using (Message message = Message.Create(10000, darkRiftWriter))
                {
                    NetworkManager.Instance.GameNetwork.SendMessage(message, SendMode.Reliable);
                }
            }
        }
    }
}
