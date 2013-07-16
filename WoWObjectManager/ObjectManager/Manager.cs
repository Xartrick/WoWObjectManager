using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magic;
using System.Diagnostics;

namespace WoWObjectManager
{
    class Manager
    {
        internal static BlackMagic WoW;

        internal static IDictionary<ulong, PlayerObject> PlayerObjectList = new Dictionary<ulong, PlayerObject>();

        internal static uint ObjMgr, CurObj, NextObj;

        public enum Offsets
        {
            clientConnection = 0xE3CB00,
            ObjectManager = 0x462C,
            FirstObject = 0xCC,
            NextObject = 0x34,
            LocalGUID = 0xE0,
            
            //Object
            ObjectType = 0xC,
            ObjectGUID = 0x28,
            Object_X = 0x7F8,
            Object_Y = Object_X + 0x4,
            Object_Z = Object_Y + 0x4,
        }

        internal static void Initialize()
        {
            WoW = new BlackMagic(); //EDIT THIS!!!

            ObjMgr = WoW.ReadUInt(WoW.ReadUInt((uint) WoW.MainModule.BaseAddress + (uint)Offsets.clientConnection) + (uint)Offsets.ObjectManager);
            CurObj = WoW.ReadUInt(ObjMgr + (Int32)Offsets.FirstObject);

            UInt64 LocalGUID = WoW.ReadUInt64(ObjMgr + (Int32)Offsets.LocalGUID);
            Console.WriteLine(string.Format("Local GUID: {0}", LocalGUID));
            Console.WriteLine(Environment.NewLine);

            RefreshObjectManager();
        }

        internal static void RefreshObjectManager()
        {
            PlayerObjectList.Clear();

            while (CurObj != 0 && (CurObj & 1) == 0)
            {
                /*
                 * 1. Items
                 * 2. Players
                 * 3. NPCS
                 * 4. Containers
                 * 5. Corpses
                 * 6. Game Objects
                 * 7. Dynamic Objects
                 */

                uint ObjectType = WoW.ReadUInt((UInt32) CurObj + (Int32) Offsets.ObjectType);
                uint NextObj = WoW.ReadUInt((UInt32)CurObj + (Int32)Offsets.NextObject);

                //I hate switches.
                if (ObjectType == 3) //NPC
                {
                    PlayerObject PlayerObject = new PlayerObject(CurObj);
                    Console.WriteLine(string.Format("[NPC] GUID: {0} - X: {1} Y: {2} Z: {3}", PlayerObject.GUID, PlayerObject.Position.X, PlayerObject.Position.Y, PlayerObject.Position.Z));

                    PlayerObjectList.Add(PlayerObject.GUID, PlayerObject);
                }

                CurObj = NextObj;
            }

            Console.WriteLine("Stahp.");
        }
    }
}
