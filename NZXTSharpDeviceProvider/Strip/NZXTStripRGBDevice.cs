﻿using System;
using System.Collections.Generic;
using System.Text;
using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    public class NZXTStripRGBDevice : NZXTRGBDevice<NZXTStripRGBDeviceInfo> {

        internal NZXTStripRGBDevice(NZXTStripRGBDeviceInfo info)
            : base(info) 
        { }

        protected override void InitializeLayout() {
            
        }

        protected override void UpdateLeds(IEnumerable<Led> ledsToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}