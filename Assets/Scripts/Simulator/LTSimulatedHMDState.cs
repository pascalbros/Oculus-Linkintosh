using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = 117)]
public struct LTSimulatedHMDState : IInputStateTypeInfo {
    /// <summary>
    /// Memory format identifier for <see cref="XRSimulatedHMDState"/>.
    /// </summary>
    /// <seealso cref="InputStateBlock.format"/>
    public static FourCC formatId => new FourCC('X', 'R', 'S', 'H');

    /// <summary>
    /// See <a href="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/api/UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo.html">IInputStateTypeInfo</a>.format.
    /// </summary>
    public FourCC format => formatId;

    /// <summary>
    /// The position of the center eye on this device.
    /// </summary>
    [InputControl(usage = "CenterEyePosition")]
    [FieldOffset(56)]
    public Vector3 centerEyePosition;

    /// <summary>
    /// The rotation of the center eye on this device.
    /// </summary>
    [InputControl(usage = "CenterEyeRotation")]
    [FieldOffset(68)]
    public Quaternion centerEyeRotation;

    /// <summary>
    /// The position of the device.
    /// </summary>
    [InputControl(usage = "DevicePosition")]
    [FieldOffset(89)]
    public Vector3 devicePosition;

    /// <summary>
    /// The rotation of this device.
    /// </summary>
    [InputControl(usage = "DeviceRotation")]
    [FieldOffset(101)]
    public Quaternion deviceRotation;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    public void Reset() {
        centerEyePosition = default;
        centerEyeRotation = default;
        devicePosition = default;
        deviceRotation = default;
    }
}