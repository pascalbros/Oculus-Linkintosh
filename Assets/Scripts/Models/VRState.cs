using System;
using UnityEngine;

[Serializable]
public class VRInputState {

    [Serializable]
    public class Axis2D {
        public Vector2 primary = default;
    }

    [Serializable]
    public class Trigger {
        public float index = default;
        public float grip = default;
    }

    public ushort buttons;
    public Touch touch = new();
    public Axis2D axis2D = new();
    public Trigger trigger = new();

    public void SetButton(LTControllerButton button, bool state) {
        buttons = LTUtils.Apply(buttons, button, state);
    }

    public bool GetButton(LTControllerButton button) {
        return LTUtils.Get(buttons, button);
    }
}

[Serializable]
public class VRItemState {
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    public void UpdateTransform(Transform t) {
        position = t.localPosition;
        rotation = t.localRotation;
    }
}

[Serializable]
public class VRState {
    public VRItemState headset = new();
    public VRItemState rightHand = new();
    public VRItemState leftHand = new();
    public VRInputState leftInput = new();
    public VRInputState rightInput = new();
}