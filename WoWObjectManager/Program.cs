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
using System.Diagnostics;
using System.Linq;

namespace WoWObjectManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectManager.Initialize();
            if (!ObjectManager.Initialized)
            {
                Console.Read();
                return;
            }

            Console.WriteLine(Environment.NewLine + Environment.NewLine);

            Console.WriteLine(string.Format("ContinentId: {0}", ObjectManager.Me.ContinentId));
            Console.WriteLine(string.Format("AreaId: {0}", ObjectManager.Me.AreaId));
            Console.WriteLine(string.Format("ZoneText: {0}; SubZoneText: {1}", ObjectManager.Me.ZoneText, ObjectManager.Me.SubZoneText));
            Console.WriteLine(string.Format("Hey {0}!\r\nHP: {1}/{2} Mana: {8}/{9} \r\nX: {3} Y: {4} Z: {5}\r\nClass: {6}\r\nLevel: {7}", ObjectManager.Me.Name, ObjectManager.Me.BaseHealth, ObjectManager.Me.MaxHealth, ObjectManager.Me.Position.X, ObjectManager.Me.Position.Y, ObjectManager.Me.Position.Z, ObjectManager.Me.Class, ObjectManager.Me.Level, ObjectManager.Me.BasePower, ObjectManager.Me.MaxPower));

            ExampleGetTargetData();
            
            Console.Read();
        }

        /// <summary>
        /// Example of how to get data about the players target.
        /// </summary>
        internal static void ExampleGetTargetData()
        {
            ulong TargetGUID = ObjectManager.Me.TargetGUID;

            if (TargetGUID == 0)
            {
                Console.WriteLine("Put a WoWUnit (NPC/Monster) in your target first!");
                return;
            }
            if (!ObjectManager.WoWUnitList.ContainsKey(TargetGUID))
            {
                Console.WriteLine("Invalid WoWUnit.");
                return;
            }

            WoWUnit WoWUnit = ObjectManager.WoWUnitList[TargetGUID];
            Console.WriteLine(string.Format("[Target] GUID: {0} - X: {1} Y: {2} Z: {3}\r\nName: {4} \r\nHealth: {5}/{6} Power: {7}/{8} Level: {9}", WoWUnit.GUID, WoWUnit.Position.X, WoWUnit.Position.Y, WoWUnit.Position.Z, WoWUnit.Name, WoWUnit.BaseHealth, WoWUnit.MaxHealth, WoWUnit.BasePower, WoWUnit.MaxPower, WoWUnit.Level));
            Console.WriteLine(string.Format("DisplayId: {0}", WoWUnit.DisplayId));
        }
    }
}
