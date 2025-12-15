using ABI_RC.Core.Player;
using CVRX.Wrappers;
using DarkRift;
using UnityEngine;

namespace CVRX.Mods
{
    internal class EventValidation
    {
        public static bool CheckPortalSpawn(Message Data)
        {
            using (DarkRiftReader reader = Data.GetReader())
            {
                string PortelOwner = reader.ReadString();
                string InstanceID = reader.ReadString();
                Vector3 Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                var OwnerPlayer = CVRPlayerManager.Instance.GetPlayer(PortelOwner);
                if (UnityWrappers.IsBadPosition(Position) || !InstanceID.StartsWith("i+") || InstanceID.Length != 41 || !InstanceID.Contains("-"))
                {
                    XConsole.Log("Protection", $"{(OwnerPlayer == null ? "LOCALPLAYER" : OwnerPlayer.Username)} spawned a Invalid Portal [{InstanceID}]");
                    return false;
                }
                XConsole.Log("Info", $"{(OwnerPlayer == null ? "LOCALPLAYER" : OwnerPlayer.Username)} spawned a Portal [{InstanceID}]");
            }
            return true;
        }
        public static bool CheckPropSpawn(Message Data)
        {
            using (DarkRiftReader reader = Data.GetReader())
            {
                string ObjectID = reader.ReadString();
                string InstanceID = reader.ReadString();
                string FileKey = reader.ReadString();
                Vector3 Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 Rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                int CustomFloats = reader.ReadInt32();
                float[] Floats = new float[CustomFloats];
                for (int i = 0; i < CustomFloats; i++)
                {
                    Floats[i] = reader.ReadSingle();
                }
                string PropOwner = reader.ReadString();
                var OwnerPlayer = CVRPlayerManager.Instance.GetPlayer(PropOwner);
                if (UnityWrappers.IsBadPosition(Position) || UnityWrappers.IsBadPosition(Rotation) || UnityWrappers.IsBadPosition(Scale) || !InstanceID.StartsWith("p+") || !InstanceID.Contains("-"))
                {
                    XConsole.Log("Protection", $"{(OwnerPlayer == null ? "LOCALPLAYER" : OwnerPlayer.Username)} spawned a Invalid Prop [{ObjectID} | {InstanceID}]");
                    return false;
                }
                XConsole.Log("Info", $"{(OwnerPlayer == null ? "LOCALPLAYER" : OwnerPlayer.Username)} spawned a Prop [{ObjectID} | {InstanceID}]");
            }
            return true;
        }
        public static bool CheckPropUpdate(Message Data)
        {
            using (DarkRiftReader reader = Data.GetReader())
            {
                string InstanceID = reader.ReadString();

                Vector3 Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 Rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                int CustomFloats = reader.ReadInt32();
                float[] Floats = new float[CustomFloats];
                for (int i = 0; i < CustomFloats; i++)
                {
                    Floats[i] = reader.ReadSingle();
                }
                string PropOwner = reader.ReadString();
                string SyncedBy = reader.ReadString();
                int SyncType = reader.ReadInt32();

                var OwnerPlayer = CVRPlayerManager.Instance.GetPlayer(PropOwner);
                var SyncPlayer = CVRPlayerManager.Instance.GetPlayer(SyncedBy);

                if (UnityWrappers.IsBadPosition(Position) || UnityWrappers.IsBadPosition(Rotation) || UnityWrappers.IsBadPosition(Scale) || !InstanceID.StartsWith("p+") || !InstanceID.Contains("-"))
                {
                    XConsole.Log("Protection", $"{(SyncPlayer == null ? "LOCALPLAYER" : SyncPlayer.Username)} updated a Invalid Prop from {(OwnerPlayer == null ? "LOCALPLAYER" : OwnerPlayer.Username)} [{InstanceID}]");
                    return false;
                }
            }
            return true;
        }
        public static bool CheckObjectSync(Message Data)
        {
            using (DarkRiftReader reader = Data.GetReader())
            {
                string key = reader.ReadString();
                Vector3 Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 Rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                bool Active = reader.ReadBoolean();
                string SyncedBy = reader.ReadString();

                var SyncPlayer = CVRPlayerManager.Instance.GetPlayer(SyncedBy);

                if (UnityWrappers.IsBadPosition(Position) || UnityWrappers.IsBadPosition(Rotation))
                {
                    XConsole.Log("Protection", $"{(SyncPlayer == null ? "LOCALPLAYER" : SyncPlayer.Username)} synced Invalid Objects");
                    return false;
                }
            }
            return true;
        }
    }
}
