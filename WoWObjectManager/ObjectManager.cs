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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace WoWObjectManager
{
    class ObjectManager
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
        /// The local player object
        /// </summary>
        internal static WoWPlayerMe Me { get; set; }

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
                WoW = new BlackMagic((from Process p in Process.GetProcesses() where p.ProcessName == "Wow" select p.Id).First());
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
                 * 3. NPCS / Monsters
                 * 4. Containers
                 * 5. Corpses
                 * 6. Game Objects
                 * 7. Dynamic Objects
                 */

                WoWObject obj = new WoWObject(CurObj);
                uint NextObj = WoW.ReadUInt(CurObj + (Int32)Offsets.ObjectManager.NextObject);

                if (obj.Guid == PlayerGUID)
                    Me = new WoWPlayerMe(CurObj);

                switch (obj.Type)
                {
                    case 3:
                        WoWUnit WoWUnit = new WoWUnit(CurObj);
                        Console.WriteLine(string.Format("[WoWUnit] GUID: {0} - X: {1} Y: {2} Z: {3}\r\nName: {4} \r\nHealth: {5}/{6} Power: {7}/{8} Level: {9}", WoWUnit.Guid, WoWUnit.Position.X, WoWUnit.Position.Y, WoWUnit.Position.Z, WoWUnit.Name, WoWUnit.BaseHealth, WoWUnit.MaxHealth, WoWUnit.BasePower, WoWUnit.MaxPower, WoWUnit.Level));
                        WoWUnitList.Add(WoWUnit.Guid, WoWUnit);
                        break;
                }

                CurObj = NextObj;
            }
        }
    }
}
