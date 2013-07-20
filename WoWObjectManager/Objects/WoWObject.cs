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
    class WoWObject
    {
        public WoWObject(uint addr)
        {
            this.BaseAddress = addr;
        }

        public uint BaseAddress { get; set; }

        public uint Type
        {
            get
            {
                return ObjectManager.WoW.ReadUInt(this.BaseAddress + (Int32)Offsets.WoWObject.Type);
            }
        }

        public ulong Guid
        {
            get
            {
                return ObjectManager.WoW.ReadUInt64(this.BaseAddress + (Int32)Offsets.WoWObject.GUID);
            }
        }
    }
}
