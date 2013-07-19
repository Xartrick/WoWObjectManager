/*
 * This file is part of the WoWObjectManager (C) 2013 Finn Grimpe
 * Copyright 2013 Finn Grimpe, All Rights Reserved
 * 
 * Github:  https://github.com/finndev/WoWObjectManager/
 * Website: http://finn.lu/
 * License: http://finn.lu/license
 *
 */

using Magic;
using System;
using System.Collections.Generic;

namespace WoWObjectManager
{
    class Manager
    {
        /// <summary>
        /// BlackMagic instance.
        /// </summary>
        internal static BlackMagic WoW { get; set; }

        /// <summary>
        /// A list of all units.
        /// </summary>
        internal static IDictionary<ulong, WoWUnit> WoWUnitList = new Dictionary<ulong, WoWUnit>();

        /// <summary>
        /// Storage 
        /// </summary>
        internal static uint CurObj;

        /// <summary>
        /// The local's player GUID
        /// </summary>
        internal static ulong PlayerGUID { get; set; }

        /// <summary>
        /// The local's player base address
        /// </summary>
        internal static uint PlayerBaseAddr { get; set; }

        /// <summary>
        /// Returns whether the ObjectManager is initialized or not
        /// </summary>
        internal static bool Initialized { get; set; }

        /// <summary>
        /// Initialize the ObjectManager and attaches BlackMagic to the processId.
        /// </summary>
        internal static void Initialize()
        {
            if (Initialized)
                return;

            try
            {
                WoW = new BlackMagic(7640);
                uint ObjMgr = WoW.ReadUInt(WoW.ReadUInt((uint)WoW.MainModule.BaseAddress + (uint)Offsets.ObjectManager.clientConnection) + (uint)Offsets.ObjectManager.ObjectManager);
                CurObj = WoW.ReadUInt(ObjMgr + (Int32)Offsets.ObjectManager.FirstObject);

                PlayerGUID = WoW.ReadUInt64(ObjMgr + (Int32)Offsets.ObjectManager.LocalGUID);

                Initialized = true;
                Pulse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Couldn't initialize the ObjectManager. New patch available? Invalid process id?");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Pulses the Objectmanager and refreshes every object it currently holds
        /// </summary>
        internal static void Pulse()
        {
            if (!Initialized)
                return;

            if (WoWUnitList.Count > 0)
                WoWUnitList.Clear();

            while (CurObj != 0 && (CurObj & 1) == 0)
            {
                /*
                 * 1. Items
                 * 2. Players
                 * 4. Containers
                 * 5. Corpses
                 * 6. Game Objects
                 * 7. Dynamic Objects
                 */

                uint ObjectType = WoW.ReadUInt(CurObj + (Int32) Offsets.WoWObject.Type);
                uint NextObj = WoW.ReadUInt(CurObj + (Int32)Offsets.ObjectManager.NextObject);
                ulong GUID = WoW.ReadUInt64(CurObj + (Int32)Offsets.WoWObject.GUID);

                if (GUID == PlayerGUID) //No clue how to do that in a different way. Appreciate helpt.
                    PlayerBaseAddr = CurObj;

                //I hate switches.
                if (ObjectType == 3) //NPCs
                {
                    WoWUnit WoWUnit = new WoWUnit(CurObj);
                    Console.WriteLine(string.Format("[WoWUnit] GUID: {0} - X: {1} Y: {2} Z: {3}\r\nName: {4} \r\nHealth: {5}/{6} Power: {7}/{8} Level: {9}", WoWUnit.GUID, WoWUnit.Position.X, WoWUnit.Position.Y, WoWUnit.Position.Z, WoWUnit.Name, WoWUnit.BaseHealth, WoWUnit.MaxHealth, WoWUnit.BasePower, WoWUnit.MaxPower, WoWUnit.Level));

                    WoWUnitList.Add(WoWUnit.GUID, WoWUnit);
                }

                CurObj = NextObj;
            }
        }
    }
}
