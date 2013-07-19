/*
 * This file is part of the WoWObjectManager (C) 2013 Finn Grimpe
 * Copyright 2013 Finn Grimpe, All Rights Reserved
 * 
 * Github:  https://github.com/finndev/WoWObjectManager/
 * Website: http://finn.lu/
 * License: http://finn.lu/license
 *
 */

namespace WoWObjectManager
{
    /// <summary>
    /// All offsets are for patch 5.3.0 17055.
    /// Credits to the OwnedCore.com Memory section (http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-bots-programs/wow-memory-editing/)
    /// </summary>
    class Offsets
    {
        public enum General
        {
            WoWVersion = 0xC01497
        }

        public enum ObjectManager
        {
            clientConnection = 0xE3CB00,
            ObjectManager = 0x462C,
            FirstObject = 0xCC,
            NextObject = 0x34,
            LocalGUID = 0xE0
        }

        public enum Descriptors
        {
            Descriptor = 0x4,
            BaseHealth = 0x78,
            MaxHealth = 0x90,
            BasePower = 0x7C,
            MaxPower = 0x94,
            Level = 0xD0,
            DisplayId = 0x108
        }

        public enum WoWObject
        {
            Type = 0xC,
            GUID = 0x28
        }

        public enum WoWPlayerMe
        {
            Name = 0xE3CB40,
            GUID = 0xC8A7B0,
            TargetGUID = 0xCDC878,
            MouseOverGUID = 0xD50F38,
            ComboPoints = 0xD51009,
            PetGUID = 0xDBF660,
            PlayerClass = 0xE3CCBD,
            ContinentId = 0xA5E814,
            AreaId = 0xADFA68,
            ZoneText = 0xCDC844, //Orgrimmar
            SubZoneText = 0xCDC840 //The valley of honor 
        }

        public enum WoWUnit
        {
            X = 0x7F8,
            Y = X + 0x4,
            Z = Y + 0x4,
            NamePointer = 0x974,
            NameOffset = 0x6C
        }
    }

    //Special thanks to Apoc.
    public enum WoWClass
    {
        None = 0,
        Warrior = 1,
        Paladin = 2,
        Hunter = 3,
        Rogue = 4,
        Priest = 5,
        DeathKnight = 6,
        Shaman = 7,
        Mage = 8,
        Warlock = 9,
        Druid = 11,
    }

    public class Vector3
    {
        internal float X { get; set; }
        internal float Y { get; set; }
        internal float Z { get; set; }

        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
}
