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
    class Program
    {
        static void Main(string[] args)
        {
            Manager.Initialize();
            if (!Manager.Initialized)
                return;

            Console.WriteLine(Environment.NewLine + Environment.NewLine);

            Console.WriteLine(string.Format("ContinentId: {0}", Objects.WoWPlayerMe.ContinentId));
            Console.WriteLine(string.Format("AreaId: {0}", Objects.WoWPlayerMe.AreaId));
            Console.WriteLine(string.Format("ZoneText: {0}; SubZoneText: {1}", Objects.WoWPlayerMe.ZoneText, Objects.WoWPlayerMe.SubZoneText));
            Console.WriteLine(string.Format("Hey {0}!\r\nHP: {1}/{2} Mana: {8}/{9} \r\nX: {3} Y: {4} Z: {5}\r\nClass: {6}\r\nLevel: {7}", Objects.WoWPlayerMe.Name, Objects.WoWPlayerMe.BaseHealth, Objects.WoWPlayerMe.MaxHealth, Objects.WoWPlayerMe.Position.X, Objects.WoWPlayerMe.Position.Y, Objects.WoWPlayerMe.Position.Z, Objects.WoWPlayerMe.Class, Objects.WoWPlayerMe.Level, Objects.WoWPlayerMe.BasePower, Objects.WoWPlayerMe.MaxPower));
            Console.Read();
        }

        internal static void GetTargetXPosition()
        {
            UInt64 TargetGUID = Manager.WoW.ReadUInt64((uint) Manager.WoW.MainModule.BaseAddress + (Int32) 0xCDC878);

            if (TargetGUID == 0)
            {
                Console.WriteLine("Put a NPC in your target first!");
                return;
            }
            if (!Manager.WoWUnitList.ContainsKey(TargetGUID))
            {
                Console.WriteLine("Invalid NPC");
                return;
            }

            WoWUnit NPCObject = Manager.WoWUnitList[TargetGUID]; //This is from the cache

            Console.WriteLine(string.Format("[Target] GUID: {0} - X: {1} Y: {2} Z: {3}", NPCObject.GUID, NPCObject.Position.X, NPCObject.Position.Y, NPCObject.Position.Z));
        }

        internal static void GetAllNPCs()
        {
            foreach (ulong GUID in Manager.WoWUnitList.Keys)
            {
                WoWUnit NPCObject = Manager.WoWUnitList[GUID];
                Console.WriteLine(string.Format("[NPC] GUID: {0} - X: {1} Y: {2} Z: {3}", NPCObject.GUID, NPCObject.Position.X, NPCObject.Position.Y, NPCObject.Position.Z));
            }
        }
    }
}
