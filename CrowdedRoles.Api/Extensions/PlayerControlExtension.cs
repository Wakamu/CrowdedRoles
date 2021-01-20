﻿using System.Collections.Generic;
using CrowdedRoles.Api.Roles;

namespace CrowdedRoles.Api.Extensions
{
    public static class PlayerControlExtension
    {
        internal static readonly Dictionary<byte, CustomRole> PlayerRoles = new Dictionary<byte, CustomRole>();

        public static void InitRole(this PlayerControl player, CustomRole? role = null)
        {
            if (role != null)
            {
                PlayerRoles.Add(player.PlayerId, role);
            }
        }

        public static CustomRole? GetRole(this PlayerControl player)
        {
            return PlayerRoles.GetValueOrDefault(player.PlayerId);
        }

        public static bool IsTeamedWith(this PlayerControl me, byte other) 
            => me.IsTeamedWith(GameData.Instance.GetPlayerById(other));

        public static bool IsTeamedWith(this PlayerControl me, GameData.PlayerInfo other)
        {
            CustomRole? myRole = me.GetRole();
            if(myRole == null)
            {
                return me.Data.IsImpostor == other.IsImpostor;
            }
            return myRole.Equals(other.Object.GetRole());
        }
        
        public static List<PlayerControl> GetTeam(this PlayerControl player)
        {
            var result = new List<PlayerControl>();
            foreach(var p in PlayerControl.AllPlayerControls)
            {
                if(p != null && player.IsTeamedWith(p.PlayerId))
                {
                    result.Add(p);
                }
            }

            return result;
        }

        public static bool CanSee(this PlayerControl me, PlayerControl whom)
        {
            Visibility? state = whom.GetRole()?.Visibility;
            return state switch
            {
                Visibility.Myself => me.PlayerId == whom.PlayerId,
                Visibility.Team => whom.IsTeamedWith(me.PlayerId),
                Visibility.Everyone => true,
                _ => false
            };
        }
    }
}