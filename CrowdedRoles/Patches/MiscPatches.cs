﻿using CrowdedRoles.Extensions;
using HarmonyLib;
using InnerNet;

namespace CrowdedRoles.Patches
{
    internal static class MiscPatches
    {
        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.Method_123))]
        public static class AmongUsClient_SpawnPlayer
        {
            public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ClientData data)
            {
                if (__instance.AmHost && !data.Character.AmOwner)
                {
                    PlayerControl.LocalPlayer.RpcSyncCustomSettings((int)data.Character.NetId);
                }
            }
        }
    }
}