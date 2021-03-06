﻿using System;
using System.Collections.Generic;
using System.Text;


namespace NZXTSharp
{

    /// <summary>
    /// Represents a generic RGB effect.
    /// </summary>
    public interface IEffect {

        /// <summary>
        /// The <see cref="IEffect"/>'s EffectByte.
        /// </summary>
        int EffectByte { get; }

        /// <summary>
        /// The name of the <see cref="IEffect"/>.
        /// </summary>
        string EffectName { get; }

        /// <summary>
        /// The <see cref="Channel"/> to set the <see cref="IEffect"/> on.
        /// </summary>
        IChannel Channel { get; set; }

        /// <summary>
        /// Checks to see if the <see cref="IEffect"/> is compatible with a given <see cref="NZXTDeviceType"/> <paramref name="Type"/>.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        bool IsCompatibleWith(NZXTDeviceType Type);

        /// <summary>
        /// Builds and returns the buffer queue needed to set the <see cref="IEffect"/>.
        /// </summary>
        /// <param name="Type">The <see cref="NZXTDeviceType"/> to build effect bytes for.</param>
        /// <param name="Channel">The <see cref="IChannel"/> to build effect bytes for.</param>
        /// <returns></returns>
        List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel);
    }
}
