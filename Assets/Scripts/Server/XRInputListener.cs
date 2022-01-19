using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class XRInputListener : MonoBehaviour {

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
        UpdateInput(d, LTControllerButton.PrimaryButton, CommonUsages.primaryButton, s);
        UpdateInput(d, LTControllerButton.SecondaryButton, CommonUsages.secondaryButton, s);
        UpdateInput(d, LTControllerButton.MenuButton, CommonUsages.menuButton, s);
        UpdateInput(d, LTControllerButton.Primary2DAxisClick, CommonUsages.primary2DAxisClick, s);
        UpdateInput(d, LTControllerButton.Primary2DAxisTouch, CommonUsages.primary2DAxisTouch, s);
        UpdateInput(d, LTControllerButton.PrimaryTouch, CommonUsages.primaryTouch, s);
        UpdateInput(d, LTControllerButton.SecondaryTouch, CommonUsages.secondaryTouch, s);
        UpdateInput(d, LTControllerButton.UserPresence, CommonUsages.userPresence, s);
        //Trigger
        d.TryGetFeatureValue(CommonUsages.grip, out s.trigger.hand);
        d.TryGetFeatureValue(CommonUsages.trigger, out s.trigger.index);
        //Analog
        d.TryGetFeatureValue(CommonUsages.primary2DAxis, out s.axis2D.primary);
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