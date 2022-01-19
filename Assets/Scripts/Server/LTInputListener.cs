using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class LTInputListener : MonoBehaviour {

    public VRInputState leftState = new();
    public VRInputState rightState = new();

    InputDevice? leftHand;
    InputDevice? rightHand;

    void Update() {
        if (leftHand == null || rightHand == null) {
            if (GetWithCharacteristics(InputDeviceCharacteristics.Left) is InputDevice left) {
                leftHand = left;
            }
            if (GetWithCharacteristics(InputDeviceCharacteristics.Right) is InputDevice right) {
                rightHand = right;
            }
        } else {
            UpdateInputStateFromDevice((InputDevice)leftHand, leftState);
            UpdateInputStateFromDevice((InputDevice)rightHand, rightState);
        }
    }

    void UpdateInputStateFromDevice(InputDevice d, VRInputState s) {
        //Trigger
        d.TryGetFeatureValue(CommonUsages.grip, out s.trigger.grip);
        d.TryGetFeatureValue(CommonUsages.trigger, out s.trigger.index);
        //Analog
        d.TryGetFeatureValue(CommonUsages.primary2DAxis, out s.axis2D.primary);
        //Buttons
        UpdateInput(d, LTControllerButton.PrimaryButton, CommonUsages.primaryButton, s);
        UpdateInput(d, LTControllerButton.SecondaryButton, CommonUsages.secondaryButton, s);
        UpdateInput(d, LTControllerButton.MenuButton, CommonUsages.menuButton, s);
        UpdateInput(d, LTControllerButton.Primary2DAxisClick, CommonUsages.primary2DAxisClick, s);
        UpdateInput(d, LTControllerButton.Primary2DAxisTouch, CommonUsages.primary2DAxisTouch, s);
        UpdateInput(d, LTControllerButton.PrimaryTouch, CommonUsages.primaryTouch, s);
        UpdateInput(d, LTControllerButton.SecondaryTouch, CommonUsages.secondaryTouch, s);
        UpdateInput(d, LTControllerButton.GripButton, CommonUsages.secondaryTouch, s);
        //Bool value of the triggers
        s.SetButton(LTControllerButton.GripButton, s.trigger.grip > 0);
        s.SetButton(LTControllerButton.TriggerButton, s.trigger.index > 0);
        //User presence
        s.SetButton(LTControllerButton.UserPresence, s.buttons != 0 || s.trigger.grip > 0 || s.trigger.index > 0 || !s.axis2D.primary.Equals(Vector3.zero));
    }

    private void UpdateInput(InputDevice device, LTControllerButton type, InputFeatureUsage<bool> button, VRInputState state) {
        device.TryGetFeatureValue(button, out bool value);
        state.SetButton(type, value);
    }

    InputDevice? GetWithCharacteristics(InputDeviceCharacteristics c) {
        List<InputDevice> devices = new();
        InputDeviceCharacteristics characteristic = c | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristic, devices);
        return devices.Count == 0 ? null : devices[0];
    }
}