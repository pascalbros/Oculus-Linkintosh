using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using TMPro;

public class XRDeviceListener : MonoBehaviour {

    List<InputDevice> devices = new();

    void Start() {

    }

    void Update() {
        devices.Clear();
        if (GetWithCharacteristics(InputDeviceCharacteristics.Left) is InputDevice left) {
            devices.Add(left);
        }
        if (GetWithCharacteristics(InputDeviceCharacteristics.Right) is InputDevice right) {
            devices.Add(right);
        }
        if (Controller() is InputDevice controller) {
            devices.Add(controller);
        }
        var text = "";
        foreach (var item in devices) {
            bool trigger;
            bool touch;
            Vector2 axis;
            item.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
            item.TryGetFeatureValue(CommonUsages.primaryTouch, out touch);
            item.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis);
            text += $"{item.name} Trigger:{trigger}\n";
            text += $"{item.name} Touch:{touch}\n";
            text += $"{item.name} Axis:{axis}\n";
        }
        GetComponent<TextMeshProUGUI>().text = text;
    }

    InputDevice? GetWithCharacteristics(InputDeviceCharacteristics c) {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics characteristic = c | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristic, devices);
        return devices.Count == 0 ? null : devices[0];
    }

    InputDevice? Controller() {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics characteristic = InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristic, devices);
        return devices.Count == 0 ? null : devices[0];
    }
}