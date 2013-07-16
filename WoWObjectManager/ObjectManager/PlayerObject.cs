using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWObjectManager
{
    class PlayerObject
    {
        internal uint BaseAddr;

        public PlayerObject(uint baseAddr)
        {
            BaseAddr = baseAddr;
        }

        internal UInt64 GUID
        {
            get
            {
                return Manager.WoW.ReadUInt64(BaseAddr + (Int32) Manager.Offsets.ObjectGUID);
            }
        }

        internal Vector3 Position
        {
            get
            {
                return new Vector3(
                    Manager.WoW.ReadFloat(BaseAddr + (Int32) Manager.Offsets.Object_X),
                    Manager.WoW.ReadFloat(BaseAddr + (Int32) Manager.Offsets.Object_Y),
                    Manager.WoW.ReadFloat(BaseAddr + (Int32) Manager.Offsets.Object_Z)
                    );
            }
        }
    }

}
