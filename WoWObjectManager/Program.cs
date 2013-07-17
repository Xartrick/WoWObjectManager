using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWObjectManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager.Initialize();

            Console.WriteLine("=== Running examples ===");
            GetTargetXPosition();
            //GetAllNPCs();


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
            if (!Manager.PlayerObjectList.ContainsKey(TargetGUID))
            {
                Console.WriteLine("Invalid NPC");
                return;
            }

            NPCObject NPCObject = Manager.PlayerObjectList[TargetGUID]; //This is from the cache

            Console.WriteLine(string.Format("[Target] GUID: {0} - X: {1} Y: {2} Z: {3}", NPCObject.GUID, NPCObject.Position.X, NPCObject.Position.Y, NPCObject.Position.Z));
        }

        internal static void GetAllNPCs()
        {
            foreach (ulong GUID in Manager.PlayerObjectList.Keys)
            {
                NPCObject NPCObject = Manager.PlayerObjectList[GUID];
                Console.WriteLine(string.Format("[NPC] GUID: {0} - X: {1} Y: {2} Z: {3}", NPCObject.GUID, NPCObject.Position.X, NPCObject.Position.Y, NPCObject.Position.Z));
            }
        }
    }
}
