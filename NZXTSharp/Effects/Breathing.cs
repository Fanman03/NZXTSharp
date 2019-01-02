﻿using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Params;
using NZXTSharp.Exceptions;

// TOTEST
namespace NZXTSharp.Effects {
    class Breathing : IEffect {

        // Per Effect Fields
        private int _EffectByte = 0x07;
        private string _EffectName = "Breathing";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };
        
        public HexColor[] Colors;
        private Channel _Channel;
        private _03Param _Param1;
        private CISS _Param2;
        private int speed = 2;
        
        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public Breathing(HexColor[] Colors) {
            this.Colors = Colors;
            ValidateParams();
        }

        public Breathing(HexColor[] Colors, int Speed) {
            this.Colors = Colors;
            this.speed = Speed;
            ValidateParams();
        }

        private void ValidateParams() {
            if (this.Colors.Length > 15) {
                throw new TooManyColorsProvidedException();
            }

            if (speed > 4 || speed < 0) {
                throw new InvalidEffectSpeedException();
            }
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            List<byte[]> outList = new List<byte[]>();
            for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++) {
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x07, _Param1, new CISS(colorIndex, this.speed) };
                byte[] final = SettingsBytes.ConcatenateByteArr(Colors[colorIndex].Expanded());
                outList.Add(final);
            }
            return outList;
        }
    }
}
