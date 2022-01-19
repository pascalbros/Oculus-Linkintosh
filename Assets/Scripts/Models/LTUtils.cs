using UnityEngine;
using System.Collections;

public enum LTControllerButton {
    PrimaryButton,
    PrimaryTouch,
    SecondaryButton,
    SecondaryTouch,
    GripButton,
    TriggerButton,
    MenuButton,
    Primary2DAxisClick,
    Primary2DAxisTouch,
    Secondary2DAxisClick,
    Secondary2DAxisTouch,
    UserPresence,
}

public static class LTUtils {
    public static ushort Apply(ushort to, LTControllerButton button, bool state = true) {
        var buttons = to;
        var bit = 1 << (int)button;
        if (state)
            buttons |= (ushort)bit;
        else
            buttons &= (ushort)~bit;
        return buttons;
    }

    public static bool Get(ushort from, LTControllerButton button) {
        return (from & 1 << (int)button) != 0;
    }
}