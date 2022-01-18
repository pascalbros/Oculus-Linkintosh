using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

public class LTDeviceSimulator : MonoBehaviour {

    LTSimulatedHMDState hmdDeviceState = new();
    LTSimulatedControllerState leftControllerState = new();
    LTSimulatedControllerState rightControllerState = new();

    LTSimulatedHMD hmdDevice = new();
    LTSimulatedController leftController = new();
    LTSimulatedController rightController = new();
    void Start() {
        hmdDeviceState.Reset();
        leftControllerState.Reset();
        rightControllerState.Reset();
        AddDevices();
    }

    // Update is called once per frame
    void Update() {
        leftControllerState.isTracked = true;
        rightControllerState.isTracked = true;
        leftControllerState.trackingState = (int)(InputTrackingState.Position | InputTrackingState.Rotation);
        rightControllerState.trackingState = (int)(InputTrackingState.Position | InputTrackingState.Rotation);

        InputState.Change(hmdDevice.centerEyePosition, hmdDeviceState.centerEyePosition);
        InputState.Change(hmdDevice.centerEyeRotation, hmdDeviceState.centerEyeRotation);
        InputState.Change(hmdDevice.devicePosition, hmdDeviceState.devicePosition);
        InputState.Change(hmdDevice.deviceRotation, hmdDeviceState.deviceRotation);
        //InputState.Change(hmdDevice, hmdDeviceState);
        InputState.Change(leftController, leftControllerState);
        InputState.Change(rightController, rightControllerState);
    }

    protected void AddDevices() {
        hmdDevice = InputSystem.AddDevice<LTSimulatedHMD>();
        leftController = InputSystem.AddDevice<LTSimulatedController>($"{nameof(LTSimulatedController)} - {UnityEngine.InputSystem.CommonUsages.LeftHand}");
        rightController = InputSystem.AddDevice<LTSimulatedController>($"{nameof(LTSimulatedController)} - {UnityEngine.InputSystem.CommonUsages.RightHand}");
        InputSystem.SetDeviceUsage(leftController, UnityEngine.InputSystem.CommonUsages.LeftHand);
        InputSystem.SetDeviceUsage(rightController, UnityEngine.InputSystem.CommonUsages.RightHand);
    }

    public void UpdateWithStream(VRTransform t) {
        hmdDeviceState.centerEyePosition = t.headset.position;
        hmdDeviceState.centerEyeRotation = Quaternion.Euler(t.headset.eulerAngles);
        hmdDeviceState.devicePosition = hmdDeviceState.centerEyePosition;
        hmdDeviceState.deviceRotation = hmdDeviceState.centerEyeRotation;
        leftControllerState.devicePosition = t.leftHand.position;
        leftControllerState.deviceRotation = Quaternion.Euler(t.leftHand.eulerAngles);
        rightControllerState.devicePosition = t.rightHand.position;
        rightControllerState.deviceRotation = Quaternion.Euler(t.rightHand.eulerAngles);
    }
}
