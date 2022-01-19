using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = 117)]
public struct LTSimulatedHMDState : IInputStateTypeInfo {

    public static FourCC formatId => new FourCC('L', 'T', 'S', 'H');

    public FourCC format => formatId;

    [InputControl(usage = "CenterEyePosition")]
    [FieldOffset(56)]
    public Vector3 centerEyePosition;

    [InputControl(usage = "CenterEyeRotation")]
    [FieldOffset(68)]
    public Quaternion centerEyeRotation;

    /// <summary>
    /// The position of the device.
    /// </summary>
    [InputControl(usage = "DevicePosition")]
    [FieldOffset(89)]
    public Vector3 devicePosition;

    [InputControl(usage = "DeviceRotation")]
    [FieldOffset(101)]
    public Quaternion deviceRotation;

    public void Reset() {
        centerEyePosition = default;
        centerEyeRotation = default;
        devicePosition = default;
        deviceRotation = default;
    }
}