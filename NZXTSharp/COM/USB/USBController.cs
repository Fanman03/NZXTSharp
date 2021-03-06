﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

using HidLibrary;

using NZXTSharp.Exceptions;

namespace NZXTSharp.COM {
    public class USBController
    { 

        private NZXTDeviceType _Type;
        private HIDDeviceID _ID;
        private int CurrProductID;
        private readonly HIDDeviceID _VendorID = HIDDeviceID.VendorID;
        private HidReport _LastReport;
        private bool _IsAttached = false;
        private HidDevice _Device;

        /// <summary>
        /// The type of device the <see cref="USBController"/> is connected to.
        /// </summary>
        public NZXTDeviceType Type { get => _Type; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentProductID { get => CurrProductID; }

        /// <summary>
        /// The <see cref="HIDDeviceID"/> of the <see cref="HidDevice"/> the <see cref="USBController"/> is connected to.
        /// </summary>
        public HIDDeviceID DeviceID { get => _ID; }

        /// <summary>
        /// The last <see cref="HidReport"/> received from the connected <see cref="HidDevice"/>.
        /// </summary>
        public HidReport LastReport { get => _LastReport; }

        /// <summary>
        /// Whether or not the <see cref="USBController"/> is currently connected to its <see cref="HidDevice"/>.
        /// </summary>
        public bool IsAttached { get => IsAttached; }

        
        /// <summary>
        /// Constructs a <see cref="USBController"/> and attempts to connect to a given <see cref="NZXTDeviceType"/>.
        /// </summary>
        /// <param name="Type"></param>
        public USBController(NZXTDeviceType Type) {
            this._Type = Type;
            ResolveDeviceID();
            Initialize();
        }

        /// <summary>
        /// Initializes the connection to a <see cref="HidDevice"/>.
        /// </summary>
        public void Initialize()
        {
            HidDevice _device = HidDevices.Enumerate((int)_VendorID, (int)_ID).FirstOrDefault();
            _Device = _device;
            _Device.OpenDevice();

            _Device.Inserted += DeviceAttachedHandler;
            _Device.Removed += DeviceRemovedHandler;

            _Device.ReadReport(OnReport);
        }

        public void SimulWrite(byte[][] Buffer)
        {
            foreach (byte[] command in Buffer)
            {
                _Device.Write(command);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Report"></param>
        internal void OnReport(HidReport Report)
        {
            this._LastReport = Report;
            _Device.ReadReport(OnReport);
        }

        /// <summary>
        /// Disposes of the current <see cref="USBController"/> instance.
        /// </summary>
        public void Dispose()
        {
            _Device.CloseDevice();
        }

        /// <summary>
        /// Writes a given <paramref name="Buffer"/> to the <see cref="HidDevice"/> the <see cref="USBController"/> is connected to.
        /// </summary>
        /// <param name="Buffer"></param>
        public void Write(byte[] Buffer)
        {
            _Device.WriteAsync(Buffer);
        }

        /// <summary>
        /// Triggers when the <see cref="USBController"/> connects to a new device.
        /// </summary>
        internal void DeviceAttachedHandler()
        {
            this._IsAttached = true;
        }

        /// <summary>
        /// Triggers when the <see cref="HidDevice"/> the <see cref="USBController"/> is connected to disconnects.
        /// </summary>
        internal void DeviceRemovedHandler()
        {
            this._IsAttached = false;
        }

        /// <summary>
        /// Resolves the <see cref="HIDDeviceID"/> matching a <see cref="NZXTDeviceType"/>.
        /// </summary>
        private void ResolveDeviceID()
        {
            switch (_Type) // TODO: Find better way
            {
                // Kraken Devices
                case NZXTDeviceType.KrakenM:
                    this._ID = HIDDeviceID.KrakenM;
                    break;
                case NZXTDeviceType.KrakenM22:
                    this._ID = HIDDeviceID.KrakenM;
                    break;
                case NZXTDeviceType.KrakenX:
                    this._ID = HIDDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX42:
                    this._ID = HIDDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX52:
                    this._ID = HIDDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX62:
                    this._ID = HIDDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX72:
                    this._ID = HIDDeviceID.KrakenX;
                    break;

                // Hue Devices
                case NZXTDeviceType.Hue2:
                    this._ID = HIDDeviceID.Hue2;
                    break;
                case NZXTDeviceType.HueAmbient:
                    this._ID = HIDDeviceID.HueAmbient;
                    break;

                // Grid Devices
                case NZXTDeviceType.GridV3:
                    this._ID = HIDDeviceID.GridV3;
                    break;

                // Motherboards
                case NZXTDeviceType.N7:
                    this._ID = HIDDeviceID.N7;
                    break;
                case NZXTDeviceType.N7_Z390:
                    this._ID = HIDDeviceID.N7_Z390;
                    break;

                // Misc
                case NZXTDeviceType.H7Lumi:
                    this._ID = HIDDeviceID.H7Lumi;
                    break;
                case NZXTDeviceType.SmartDevice:
                    this._ID = HIDDeviceID.SmartDevice;
                    break;

                default:
                    throw new IncompatibleDeviceTypeException();
            }
        }
        

        public string[] ScanForDevices()
        {
            // TODO
            return new[] { "" };
        }
    }
}
