using System.Runtime.InteropServices;
using System;

internal class DiscordRpc
{
    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
    protected internal static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
    protected internal static extern void Shutdown();

    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
    protected internal static extern void RunCallbacks();

    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
    protected internal static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClearPresence")]
    protected internal static extern void ClearPresence();

    [DllImport("discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
    protected internal static extern void Respond(string userId, Reply reply);

    internal struct EventHandlers
    {
        internal ReadyCallback readyCallback;
        internal DisconnectedCallback disconnectedCallback;
        internal ErrorCallback errorCallback;
        internal JoinCallback joinCallback;
        internal SpectateCallback spectateCallback;
        internal RequestCallback requestCallback;
    }

    [Serializable]
    internal struct RichPresenceButton
    {
        public string label; // Button text (max 32 characters)
        public string url;   // URL to open when clicked
    }

    [Serializable]
    internal struct RichPresence
    {
        internal string state;
        internal string details;
        internal long startTimestamp;
        internal long endTimestamp;
        internal string largeImageKey;
        internal string largeImageText;
        internal string smallImageKey;
        internal string smallImageText;
        internal string partyId;
        internal int partySize;
        internal int partyMax;
        internal string matchSecret;
        internal string joinSecret;
        internal string spectateSecret;
        internal bool instance;

        internal RichPresenceButton[] buttons;
    }

    internal static RichPresenceButton CreateButton(string label, string url)
    {
        return new RichPresenceButton
        {
            label = label,
            url = url
        };
    }

    [Serializable]
    internal struct JoinRequest
    {
        public string userId;
        public string username;
        public string discriminator;
        public string avatar;
    }
    public enum Reply
    {
        No,
        Yes,
        Ignore
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void ReadyCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void DisconnectedCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void ErrorCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void JoinCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void SpectateCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void RequestCallback(ref JoinRequest request);
}