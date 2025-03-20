using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;

namespace ChatClipboard
{
    [HarmonyPatch(typeof(ChatManager), "Update")]
    public class ChatManagerPatch
    {
        private static readonly FieldInfo chatActiveInfo = typeof(ChatManager).GetField("chatActive",
                                                                                        BindingFlags.NonPublic
                                                                                        | BindingFlags.Instance);
        private static readonly FieldInfo chatMessageInfo = typeof(ChatManager).GetField("chatMessage",
                                                                                        BindingFlags.NonPublic
                                                                                        | BindingFlags.Instance);

        [HarmonyPostfix]
        private static void Postfix(ChatManager __instance)
        {
            if (chatActiveInfo == null || chatMessageInfo == null || !Input.anyKeyDown)
                return;

            bool chatActive = (bool)chatActiveInfo.GetValue(__instance);
            

            if (Input.GetKey(KeyCode.LeftControl) && chatActive)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    GUIUtility.systemCopyBuffer = (string)chatMessageInfo.GetValue(__instance);
                }
                else if (Input.GetKeyDown(KeyCode.V))
                {
                    string pastedText = GUIUtility.systemCopyBuffer;
                    if (pastedText != null)
                    {
                        string newText = (string)chatMessageInfo.GetValue(__instance) + pastedText;
                        newText.Replace("<b>|</b>", "");
                        chatMessageInfo.SetValue(__instance, newText);
                    }

                }
            }
        }
    }
}
