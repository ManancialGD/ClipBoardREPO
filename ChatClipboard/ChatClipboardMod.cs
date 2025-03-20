using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ChatClipBoard
{
    [BepInPlugin("com.manancialgd.chatclipboard", "ChatClipboard", "1.0.0")]
    public class PushToMuteMod : BaseUnityPlugin
    {
        private Harmony harmony;

        private void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            harmony = new Harmony("com.coddingcat.pushtomute");
            harmony.PatchAll();
            Logger.LogInfo($"ChatClipBoard loaded!");

        }
    }

}
