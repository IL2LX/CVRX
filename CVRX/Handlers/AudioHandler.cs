using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

namespace CVRX.Handlers
{
    internal class AudioHandler
    {
        private static AudioSource _source;
        private static AudioClip _clip;
        private static bool HasInit = false;
        private static bool HasWelcome = false;
        private static Dictionary<string, AudioClip> _embedCache = new Dictionary<string, AudioClip>();

        public static void Setup()
        {
            GameObject audioObj = new GameObject("CVRX_AudioHandler");
            UnityEngine.Object.DontDestroyOnLoad(audioObj);
            _source = audioObj.AddComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.clip = _clip;
            PlayFromREmbed(Properties.Resources.start);
            HasInit = true;
        }

        public static void Play()
        {
            if (_source != null && _clip != null)
                _source.Play();
        }

        public static void PlayFromREmbed(UnmanagedMemoryStream sound)
        {
            if (sound == null)
            {
                XConsole.Log("AudioHandler", "Embedded sound is null!");
                return;
            }
            byte[] wavData = new byte[sound.Length];
            sound.Read(wavData, 0, (int)sound.Length);
            string hash = ComputeHash(wavData);
            if (_embedCache.TryGetValue(hash, out AudioClip cachedClip))
            {
                _clip = cachedClip;
            }
            else
            {
                _clip = WavToAudioClip(wavData, "embed_clip_" + hash.Substring(0, 8));
                _embedCache[hash] = _clip;
            }
            _source.clip = _clip;
            _source.Play();
        }

        public static void PlayFromFile(string pathOrUrl)
        {
            MelonCoroutines.Start(LoadAudioCoroutine(pathOrUrl));
        }

        private static IEnumerator LoadAudioCoroutine(string pathOrUrl)
        {
            AudioType type = AudioType.WAV;
            if (pathOrUrl.StartsWith("http"))
            {
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(pathOrUrl, type))
                {
                    yield return www.SendWebRequest();
                    if (www.result != UnityWebRequest.Result.Success)
                        XConsole.Log("AudioHandler", "Failed to load audio from URL: " + www.error);
                    else
                    {
                        _clip = DownloadHandlerAudioClip.GetContent(www);
                        _source.clip = _clip;
                        _source.Play();
                        if (!HasWelcome)
                        {
                            HasWelcome = true;
                        }
                    }
                }
            }
            else
            {
                if (!File.Exists(pathOrUrl))
                {
                    XConsole.Log("AudioHandler", "Audio file not found: " + pathOrUrl);
                    yield break;
                }
                byte[] fileData = File.ReadAllBytes(pathOrUrl);
                _clip = WavToAudioClip(fileData, "file_clip");
                _source.clip = _clip;
                _source.Play();
            }
        }

        private static AudioClip WavToAudioClip(byte[] wavFile, string clipName)
        {
            using (MemoryStream ms = new MemoryStream(wavFile))
            using (BinaryReader br = new BinaryReader(ms))
            {
                br.ReadChars(4);
                br.ReadInt32();
                br.ReadChars(4);
                br.ReadChars(4);
                int fmtSize = br.ReadInt32();
                br.ReadInt16();
                int channels = br.ReadInt16();
                int sampleRate = br.ReadInt32();
                br.ReadInt32();
                br.ReadInt16();
                int bitsPerSample = br.ReadInt16();
                if (fmtSize > 16)
                    br.ReadBytes(fmtSize - 16);
                string chunkID = new string(br.ReadChars(4));
                while (chunkID != "data")
                {
                    int size = br.ReadInt32();
                    br.ReadBytes(size);
                    chunkID = new string(br.ReadChars(4));
                }
                int dataSize = br.ReadInt32();
                byte[] data = br.ReadBytes(dataSize);
                if (bitsPerSample != 16)
                {
                    Debug.LogError("Only 16-bit WAV supported!");
                    return null;
                }
                int samplesCount = dataSize / 2;
                float[] samples = new float[samplesCount];
                for (int i = 0; i < samplesCount; i++)
                    samples[i] = BitConverter.ToInt16(data, i * 2) / 32768f;
                AudioClip clip = AudioClip.Create(clipName, samplesCount, channels, sampleRate, false);
                clip.SetData(samples, 0);
                return clip;
            }
        }

        private static string ComputeHash(byte[] data)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(data);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
