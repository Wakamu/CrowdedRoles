﻿using UnityEngine;
using System;

namespace CrowdedRoles.Api.Roles
{
    public class CustomRole
    {
        internal byte Id;
        private static bool DefaultKillFilter(PlayerControl me, GameData.PlayerInfo target)
        {
            return 
                !target.Disconnected &&
                me.PlayerId != target.PlayerId &&
                !target.IsDead &&
                !target.IsImpostor;
        }
        private bool createdAmountOption = false;

        public readonly string Name;
        public readonly Color Color;
        public Func<PlayerControl, GameData.PlayerInfo, bool> KillFilter = new Func<PlayerControl, GameData.PlayerInfo, bool>(DefaultKillFilter);
        public bool AbleToKill = false;
        public string NameFormat = "{0}";
        public string StartTip = "Do nothing but [00FF00FF]kiss";
        public Side Side = Side.Crewmate;
        public Visibility Visibility = Visibility.Myself;

        public CustomRole(string name, Color color = new Color())
        {
            Name = name;
            Color = color;
        }

        public void CreateAmountOption()
        {
            if(createdAmountOption)
            {
                MainPlugin.Logger.LogWarning($"Role {Name} already has an amount option, ignoring");
                return;
            }
            createdAmountOption = true;
            // create option
            RoleManager.Limits[Id] = 0;
        }
    }
}