using System;
using System.IO;
using System.Linq;
using UnityEngine;
using ABI_RC.Core.Player;
using CVRX.Wrappers;
using CVRX.UIs;

namespace CVRX.Mods.Misc
{
    internal class AssetDumper
    {
        public static string input = "";

        public static void Render()
        {
            UIEx.Collapsible("AssetDumper", () =>
            {
                input = GUILayout.TextField(input);
                if (GUILayout.Button("Dump"))
                {
                    var go = GameObject.Find(input);
                    DumpGameObject(go);
                }
            });
        }

        public static void DumpPlayerAvatar(CVRPlayerEntity player)
        {
            try
            {
                var avatarRoot = player.GetPuppetMaster()?.AvatarObject;
                if (avatarRoot == null)
                {
                    XConsole.Log("AssetDumper", $"No avatar found for {player.GetUsername()}.");
                    return;
                }
                string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CVRX_AvatarDumper", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                Directory.CreateDirectory(root);
                string avatarFolder = Path.Combine(root, $"{player.GetUsername()} Avatar");
                Directory.CreateDirectory(avatarFolder);
                XConsole.Log("AssetDumper", $"Dumping {player.GetUsername()}'s avatar → {avatarFolder}.");
                DumpGameObjectAssets(avatarRoot, avatarFolder);
                DumpComponents(avatarRoot, avatarFolder);
                DumpParticles(avatarRoot, avatarFolder);
                DumpAnimations(avatarRoot, avatarFolder);
                CreateIndex(root);
                System.Diagnostics.Process.Start("explorer.exe", root);
                XConsole.Log("AssetDumper", $"Done. Exported {player.GetUsername()}'s avatar.");
            }
            catch (Exception ex)
            {
                XConsole.Log("AssetDumper", $"ERROR: {ex}");
            }
        }
        public static void DumpGameObject(GameObject avatarRoot)
        {
            try
            {
                if (avatarRoot == null)
                {
                    XConsole.Log("AssetDumper", $"No gameobject found.");
                    return;
                }
                string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AvatarDump", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                Directory.CreateDirectory(root);
                string avatarFolder = Path.Combine(root, $"{avatarRoot.name} prop");
                Directory.CreateDirectory(avatarFolder);
                XConsole.Log("AssetDumper", $"Dumping {avatarRoot.name}'s gameobject → {avatarFolder}.");
                DumpGameObjectAssets(avatarRoot, avatarFolder);
                DumpComponents(avatarRoot, avatarFolder);
                DumpParticles(avatarRoot, avatarFolder);
                DumpAnimations(avatarRoot, avatarFolder);
                CreateIndex(root);
                System.Diagnostics.Process.Start("explorer.exe", root);
                XConsole.Log("AssetDumper", $"Done. Exported {avatarRoot.name}'s gameobject.");
            }
            catch (Exception ex)
            {
                XConsole.Log("AssetDumper", $"ERROR: {ex}");
            }
        }

        private static void DumpGameObjectAssets(GameObject root, string folder)
        {
            string meshFolder = Path.Combine(folder, "Meshes");
            string textureFolder = Path.Combine(folder, "Textures");
            string audioFolder = Path.Combine(folder, "AudioClips");
            Directory.CreateDirectory(meshFolder);
            Directory.CreateDirectory(textureFolder);
            Directory.CreateDirectory(audioFolder);
            MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>(true);
            SkinnedMeshRenderer[] skinned = root.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            int meshCounter = 0;
            foreach (var mf in meshFilters)
            {
                if (mf.sharedMesh == null) continue;
                ExportMesh(mf.sharedMesh, mf.transform, Path.Combine(meshFolder, $"mesh_{meshCounter++}.obj"));
                DumpMaterials(mf.GetComponent<Renderer>(), textureFolder);
                DumpMaterialsJson(mf.GetComponent<Renderer>(), folder);
            }
            foreach (var smr in skinned)
            {
                if (smr.sharedMesh == null) continue;
                ExportMesh(smr.sharedMesh, smr.transform, Path.Combine(meshFolder, $"skinned_{meshCounter++}.obj"));
                DumpMaterials(smr, textureFolder);
                DumpMaterialsJson(smr, folder);
            }
            AudioSource[] audios = root.GetComponentsInChildren<AudioSource>(true);
            foreach (var audio in audios)
            {
                if (audio.clip == null) continue;
                SaveAudioClip(audio.clip, audioFolder);
            }
        }

        private static void DumpComponents(GameObject go, string folder)
        {
            string compFolder = Path.Combine(folder, "Components");
            Directory.CreateDirectory(compFolder);
            foreach (var comp in go.GetComponentsInChildren<Component>(true))
            {
                if (comp == null) continue;
                string fileName = $"{Sanitize(comp.GetType().Name)}_{Guid.NewGuid():N}.json";
                string path = Path.Combine(compFolder, fileName);
                try
                {
                    string json = JsonUtility.ToJson(comp, true);
                    File.WriteAllText(path, json);
                }
                catch { }
            }
        }

        private static void DumpParticles(GameObject go, string folder)
        {
            ParticleSystem[] particles = go.GetComponentsInChildren<ParticleSystem>(true);
            if (particles.Length == 0) return;
            string particleFolder = Path.Combine(folder, "Particles");
            Directory.CreateDirectory(particleFolder);
            foreach (var ps in particles)
            {
                string json = JsonUtility.ToJson(ps, true);
                File.WriteAllText(Path.Combine(particleFolder, $"{Sanitize(ps.name)}.json"), json);
            }
        }

        private static void DumpAnimations(GameObject go, string folder)
        {
            Animator animator = go.GetComponentInChildren<Animator>(true);
            if (animator == null || animator.runtimeAnimatorController == null) return;
            string animFolder = Path.Combine(folder, "Animations");
            Directory.CreateDirectory(animFolder);
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                string path = Path.Combine(animFolder, $"{Sanitize(clip.name)}.json");
                string json = JsonUtility.ToJson(new { clip.name, clip.length, clip.frameRate }, true);
                File.WriteAllText(path, json);
            }
        }

        private static void DumpMaterials(Renderer r, string textureFolder)
        {
            if (r == null || r.sharedMaterials == null) return;

            string[] texProps =
            {
                "_MainTex",
                "_BaseMap",
                "_EmissionMap",
                "_BumpMap",
                "_MetallicGlossMap"
            };

            foreach (var mat in r.sharedMaterials)
            {
                if (mat == null) continue;

                foreach (var prop in texProps)
                {
                    if (!mat.HasProperty(prop)) continue;
                    Texture t = mat.GetTexture(prop);
                    if (t == null) continue;

                    SaveTexture(t, textureFolder);
                }
            }
        }

        private static void DumpMaterialsJson(Renderer r, string folder)
        {
            if (r == null) return;
            string matFolder = Path.Combine(folder, "MaterialsJson");
            Directory.CreateDirectory(matFolder);
            foreach (var mat in r.sharedMaterials)
            {
                if (mat == null) continue;
                string json = JsonUtility.ToJson(new SerializableMaterial(mat), true);
                File.WriteAllText(Path.Combine(matFolder, $"{Sanitize(mat.name)}.json"), json);
            }
        }

        [Serializable]
        private class SerializableMaterial
        {
            public string name;
            public Color color;
            public float metallic;
            public float smoothness;

            public SerializableMaterial(Material m)
            {
                name = m.name;
                if (m.HasProperty("_Color")) color = m.color;
                if (m.HasProperty("_Metallic")) metallic = m.GetFloat("_Metallic");
                if (m.HasProperty("_Glossiness")) smoothness = m.GetFloat("_Glossiness");
            }
        }

        private static void SaveTexture(Texture tex, string folder)
        {
            try
            {
                int w = tex.width;
                int h = tex.height;
                RenderTexture rt = RenderTexture.GetTemporary(w, h, 0);
                Graphics.Blit(tex, rt);
                Texture2D readable = new Texture2D(w, h, TextureFormat.RGBA32, false);
                RenderTexture.active = rt;
                readable.ReadPixels(new Rect(0, 0, w, h), 0, 0);
                readable.Apply();
                RenderTexture.active = null;
                byte[] png = readable.EncodeToPNG();
                string name = $"{Sanitize(tex.name)}_{Guid.NewGuid():N}.png";
                File.WriteAllBytes(Path.Combine(folder, name), png);
                UnityEngine.Object.Destroy(readable);
                RenderTexture.ReleaseTemporary(rt);
            }
            catch { }
        }

        private static void SaveAudioClip(AudioClip clip, string folder)
        {
            try
            {
                string path = Path.Combine(folder, $"{Sanitize(clip.name)}.wav");
                var samples = new float[clip.samples * clip.channels];
                clip.GetData(samples, 0);
                using (var fileStream = new FileStream(path, FileMode.Create))
                using (var writer = new BinaryWriter(fileStream))
                {
                    int hz = clip.frequency;
                    int channels = clip.channels;
                    int samplesLength = samples.Length * 2;
                    int byteRate = hz * channels * 2;
                    writer.Write(System.Text.Encoding.UTF8.GetBytes("RIFF"));
                    writer.Write(36 + samplesLength);
                    writer.Write(System.Text.Encoding.UTF8.GetBytes("WAVE"));
                    writer.Write(System.Text.Encoding.UTF8.GetBytes("fmt "));
                    writer.Write(16);
                    writer.Write((short)1);
                    writer.Write((short)channels);
                    writer.Write(hz);
                    writer.Write(byteRate);
                    writer.Write((short)(channels * 2));
                    writer.Write((short)16);
                    writer.Write(System.Text.Encoding.UTF8.GetBytes("data"));
                    writer.Write(samplesLength);
                    foreach (var sample in samples)
                    {
                        short s = (short)(Mathf.Clamp(sample, -1f, 1f) * short.MaxValue);
                        writer.Write(s);
                    }
                }
            }
            catch (Exception ex)
            {
                XConsole.Log("AssetDumper", $"Audio export failed:{ex}");
            }
        }

        private static void ExportMesh(Mesh mesh, Transform t, string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("# OBJ Export");
                    foreach (var v in mesh.vertices)
                    {
                        Vector3 w = t.TransformPoint(v);
                        sw.WriteLine($"v {-w.x} {w.y} {w.z}");
                    }
                    foreach (var uv in mesh.uv)
                        sw.WriteLine($"vt {uv.x} {uv.y}");
                    foreach (var n in mesh.normals)
                    {
                        Vector3 wn = t.TransformDirection(n);
                        sw.WriteLine($"vn {-wn.x} {wn.y} {wn.z}");
                    }
                    int[] tri = mesh.triangles;
                    for (int i = 0; i < tri.Length; i += 3)
                    {
                        int a = tri[i] + 1;
                        int b = tri[i + 1] + 1;
                        int c = tri[i + 2] + 1;
                        sw.WriteLine($"f {a}/{a}/{a} {b}/{b}/{b} {c}/{c}/{c}");
                    }
                }
            }
            catch (Exception ex)
            {
                XConsole.Log("AssetDumper", $"Mesh Export failed:{ex}");
            }
        }

        private static string Sanitize(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }

        private class AssetIndex
        {
            public string Name;
            public string Type;
            public string Path;
        }

        private static void CreateIndex(string folder)
        {
            var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Select(f => new AssetIndex { Name = Path.GetFileName(f), Type = Path.GetExtension(f), Path = f }).ToArray();
            File.WriteAllText(Path.Combine(folder, "index.json"), JsonUtility.ToJson(new { files }, true));
        }
    }
}
