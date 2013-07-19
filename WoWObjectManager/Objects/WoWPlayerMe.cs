/*
 * This file is part of the WoWObjectManager (C) 2013 Finn Grimpe
 * Copyright 2013 Finn Grimpe, All Rights Reserved
 * 
 * Github:  https://github.com/finndev/WoWObjectManager/
 * Website: http://finn.lu/
 * License: http://finn.lu/license
 *
 */

using Microsoft.Xna.Framework;
using System;

namespace WoWObjectManager.Objects
{
    /// <summary>
    /// Repesents the local player
    /// </summary>
    class WoWPlayerMe
    {
        internal static uint BaseAddr
        {
            get { return Manager.PlayerBaseAddr; }
        }

        /// <summary>
        /// The players name
        /// </summary>
        internal static string Name
        {
            get { return Manager.WoW.ReadASCIIString((uint) Manager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.Name, 128); }
        }

        /// <summary>
        /// The players GUID
        /// </summary>
        internal static ulong GUID
        {
            get { return Manager.PlayerGUID; }
        }

        /// <summary>
        /// The players GUID
        /// </summary>
        internal static ulong TargetGUID
        {
            get { return Manager.WoW.ReadUInt64((uint)Manager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.TargetGUID); }
        }

        /// <summary>
        /// Returns the postion as Vector3
        /// </summary>
        internal static Vector3 Position
        {
            get
            {
                return new Vector3(
                    Manager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.X),
                    Manager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.Y),
                    Manager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.Z)
                    );
            }
        }

        /// <summary>
        /// The continendid on which the player currently is
        /// </summary>
        internal static int ContinentId
        {
            get {return Manager.WoW.ReadInt((uint)Manager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.ContinentId); }
        }

        /// <summary>
        /// The areaid on which the player currently is
        /// </summary>
        internal static int AreaId
        {
            get { return Manager.WoW.ReadInt((uint)Manager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.AreaId); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal static string ZoneText
        {
            get { return Manager.WoW.ReadASCIIString(Manager.WoW.ReadUInt((uint)Manager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.ZoneText), 128); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal static string SubZoneText
        {
            get { return Manager.WoW.ReadASCIIString(Manager.WoW.ReadUInt((uint)Manager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.SubZoneText), 128); }
        }

        /// <summary>
        /// The players base health
        /// </summary>
        internal static float BaseHealth
        {
            get { return Manager.WoW.ReadInt(Manager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.BaseHealth); }
        }

        /// <summary>
        /// The players max health
        /// </summary>
        internal static float MaxHealth
        {
            get { return Manager.WoW.ReadInt(Manager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.MaxHealth); }
        }

        /// <summary>
        /// The players combopoints
        /// </summary>
        internal static int ComboPoints
        {
            get { return Manager.WoW.ReadInt((uint)Manager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.ComboPoints); }
        }

        /// <summary>
        /// The players class
        /// </summary>
        internal static WoWClass Class
        {
            get
            {
                return (WoWClass)Manager.WoW.ReadByte((uint)Manager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.PlayerClass);
            }
        }

        /// <summary>
        /// The players base power
        /// </summary>
        internal static float BasePower
        {
            get { return Manager.WoW.ReadInt(Manager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.BasePower); }
        }

        /// <summary>
        /// The players max power
        /// </summary>
        internal static float MaxPower
        {
            get { return Manager.WoW.ReadInt(Manager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.MaxPower); }
        }

        /// <summary>
        /// The players level
        /// </summary>
        internal static float Level
        {
            get { return Manager.WoW.ReadInt(Manager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.Level); }
        }

        /// <summary>
        /// Returns whether the player is alive or not.
        /// </summary>
        internal static bool IsAlive
        {
            get
            {
                if (BaseHealth > 0)
                    return true;
                return false;
            }
        }
    }
}
