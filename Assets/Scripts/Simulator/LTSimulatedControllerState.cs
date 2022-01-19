using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = 63)]
public struct LTSimulatedControllerState : IInputStateTypeInfo {

    public static FourCC formatId => new('L', 'T', 'S', 'C');

    public FourCC format => formatId;

    [InputControl(usage = "Primary2DAxis", aliases = new[] { "thumbstick", "joystick" })]
    [FieldOffset(0)]
    public Vector2 primary2DAxis;

    [InputControl(usage = "Trigger", layout = "Axis")]
    [FieldOffset(8)]
    public float trigger;

    [InputControl(usage = "Grip", layout = "Axis")]
    [FieldOffset(12)]
    public float grip;

    [InputControl(usage = "Secondary2DAxis")]
    [FieldOffset(16)]
    public Vector2 secondary2DAxis;

    [InputControl(name = nameof(LTSimulatedController.primaryButton), usage = "PrimaryButton", layout = "Button", bit = (uint)LTControllerButton.PrimaryButton)]
    [InputControl(name = nameof(LTSimulatedController.primaryTouch), usage = "PrimaryTouch", layout = "Button", bit = (uint)LTControllerButton.PrimaryTouch)]
    [InputControl(name = nameof(LTSimulatedController.secondaryButton), usage = "SecondaryButton", layout = "Button", bit = (uint)LTControllerButton.SecondaryButton)]
    [InputControl(name = nameof(LTSimulatedController.secondaryTouch), usage = "SecondaryTouch", layout = "Button", bit = (uint)LTControllerButton.SecondaryTouch)]
    [InputControl(name = nameof(LTSimulatedController.gripButton), usage = "GripButton", layout = "Button", bit = (uint)LTControllerButton.GripButton, alias = "gripPressed")]
    [InputControl(name = nameof(LTSimulatedController.triggerButton), usage = "TriggerButton", layout = "Button", bit = (uint)LTControllerButton.TriggerButton, alias = "triggerPressed")]
    [InputControl(name = nameof(LTSimulatedController.menuButton), usage = "MenuButton", layout = "Button", bit = (uint)LTControllerButton.MenuButton)]
    [InputControl(name = nameof(LTSimulatedController.primary2DAxisClick), usage = "Primary2DAxisClick", layout = "Button", bit = (uint)LTControllerButton.Primary2DAxisClick)]
    [InputControl(name = nameof(LTSimulatedController.primary2DAxisTouch), usage = "Primary2DAxisTouch", layout = "Button", bit = (uint)LTControllerButton.Primary2DAxisTouch)]
    [InputControl(name = nameof(LTSimulatedController.secondary2DAxisClick), usage = "Secondary2DAxisClick", layout = "Button", bit = (uint)LTControllerButton.Secondary2DAxisClick)]
    [InputControl(name = nameof(LTSimulatedController.secondary2DAxisTouch), usage = "Secondary2DAxisTouch", layout = "Button", bit = (uint)LTControllerButton.Secondary2DAxisTouch)]
    [InputControl(name = nameof(LTSimulatedController.userPresence), usage = "UserPresence", layout = "Button", bit = (uint)LTControllerButton.UserPresence)]
    [FieldOffset(24)]
    public ushort buttons;

    [InputControl(usage = "BatteryLevel", layout = "Axis")]
    [FieldOffset(26)]
    public float batteryLevel;

    [InputControl(usage = "TrackingState", layout = "Integer")]
    [FieldOffset(30)]
    public int trackingState;

    [InputControl(usage = "IsTracked", layout = "Button")]
    [FieldOffset(34)]
    public bool isTracked;

    [InputControl(usage = "DevicePosition")]
    [FieldOffset(35)]
    public Vector3 devicePosition;

    [InputControl(usage = "DeviceRotation")]
    [FieldOffset(47)]
    public Quaternion deviceRotation;

    public LTSimulatedControllerState WithButton(LTControllerButton button, bool state = true) {
        buttons = LTUtils.Apply(buttons, button, state);
        return this;
    }

    public void Reset() {
        primary2DAxis = default;
        trigger = default;
        grip = default;
        secondary2DAxis = default;
        buttons = default;
        batteryLevel = default;
        trackingState = default;
        isTracked = default;
        devicePosition = default;
        deviceRotation = Quaternion.identity;
    }
}