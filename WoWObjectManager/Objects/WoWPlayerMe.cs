/*
 * This file is part of the WoWObjectManager (C) 2013 Finn Grimpe
 * Copyright 2013 Finn Grimpe, All Rights Reserved
 * 
 * Github:  https://github.com/finndev/WoWObjectManager/
 * Website: http://finn.lu/
 * License: http://finn.lu/license
 *
 */

using System;

namespace WoWObjectManager
{
    /// <summary>
    /// Repesents the local player
    /// </summary>
    class WoWPlayerMe
    {
        public WoWPlayerMe(uint baseAddr)
        {
            if (BaseAddr == baseAddr)
                return;

            BaseAddr = baseAddr;
        }

        /// <summary>
        /// The players base address
        /// </summary>
        internal uint BaseAddr { get; set; }

        /// <summary>
        /// The players name
        /// </summary>
        internal string Name
        {
            get { return ObjectManager.WoW.ReadASCIIString((uint) ObjectManager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.Name, 128); }
        }

        /// <summary>
        /// The players GUID
        /// </summary>
        internal ulong GUID
        {
            get { return ObjectManager.PlayerGUID; }
        }

        /// <summary>
        /// The players GUID
        /// </summary>
        internal ulong TargetGUID
        {
            get { return ObjectManager.WoW.ReadUInt64((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.TargetGUID); }
        }

        /// <summary>
        /// Returns the postion as Vector3
        /// </summary>
        internal Vector3 Position
        {
            get
            {
                return new Vector3(
                    ObjectManager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.X),
                    ObjectManager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.Y),
                    ObjectManager.WoW.ReadFloat(BaseAddr + (Int32)Offsets.WoWUnit.Z)
                    );
            }
        }

        /// <summary>
        /// The continendid on which the player currently is
        /// </summary>
        internal int ContinentId
        {
            get {return ObjectManager.WoW.ReadInt((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.ContinentId); }
        }

        /// <summary>
        /// The areaid on which the player currently is
        /// </summary>
        internal int AreaId
        {
            get { return ObjectManager.WoW.ReadInt((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.AreaId); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal string ZoneText
        {
            get { return ObjectManager.WoW.ReadASCIIString(ObjectManager.WoW.ReadUInt((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.ZoneText), 128); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal string SubZoneText
        {
            get { return ObjectManager.WoW.ReadASCIIString(ObjectManager.WoW.ReadUInt((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.SubZoneText), 128); }
        }

        /// <summary>
        /// The players base health
        /// </summary>
        internal float BaseHealth
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.BaseHealth); }
        }

        /// <summary>
        /// The players max health
        /// </summary>
        internal float MaxHealth
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.MaxHealth); }
        }

        /// <summary>
        /// The players combopoints
        /// </summary>
        internal int ComboPoints
        {
            get { return ObjectManager.WoW.ReadInt((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32)Offsets.WoWPlayerMe.ComboPoints); }
        }

        /// <summary>
        /// The players class
        /// </summary>
        internal WoWClass Class
        {
            get
            {
                return (WoWClass)ObjectManager.WoW.ReadByte((uint)ObjectManager.WoW.MainModule.BaseAddress + (Int32) Offsets.WoWPlayerMe.PlayerClass);
            }
        }

        /// <summary>
        /// The players base power
        /// </summary>
        internal float BasePower
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.BasePower); }
        }

        /// <summary>
        /// The players max power
        /// </summary>
        internal float MaxPower
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.MaxPower); }
        }

        /// <summary>
        /// The players level
        /// </summary>
        internal float Level
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddr + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.Level); }
        }

        /// <summary>
        /// Returns whether the player is alive or not.
        /// </summary>
        internal bool IsAlive
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
