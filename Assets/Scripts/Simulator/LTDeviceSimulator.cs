using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

public class LTDeviceSimulator : MonoBehaviour {

    public LTSimulatedHMDState hmdDeviceState = new();
    public LTSimulatedControllerState leftControllerState = new();
    public LTSimulatedControllerState rightControllerState = new();

    LTSimulatedHMD hmdDevice = new();
    LTSimulatedController leftController = new();
    LTSimulatedController rightController = new();

    public VRInputState leftInputState = new();
    public VRInputState rightInputState = new();

    void Start() {
        hmdDeviceState.Reset();
        leftControllerState.Reset();
        rightControllerState.Reset();
        AddDevices();
    }

    void Update() {
        leftControllerState.isTracked = true;
        rightControllerState.isTracked = true;
        leftControllerState.trackingState = (int)(InputTrackingState.Position | InputTrackingState.Rotation);
        rightControllerState.trackingState = (int)(InputTrackingState.Position | InputTrackingState.Rotation);
        UpdateInput();
    }

    private void UpdateInput() {
        InputState.Change(hmdDevice, hmdDeviceState);
        UpdateInputController(leftController, leftControllerState, leftInputState);
        UpdateInputController(rightController, rightControllerState, rightInputState);
    }

    private void UpdateInputController(LTSimulatedController c, LTSimulatedControllerState ss, VRInputState s) {
        ushort buttons = s.buttons;
        ss.buttons = buttons;
        ss.grip = s.trigger.hand;
        ss.trigger = s.trigger.index;
        ss.primary2DAxis = s.axis2D.primary;
        InputState.Change(c, ss, InputUpdateType.Default);
    }

    private void AddDevices() {
        hmdDevice = InputSystem.AddDevice<LTSimulatedHMD>();
        leftController = InputSystem.AddDevice<LTSimulatedController>($"{nameof(LTSimulatedController)} - {UnityEngine.InputSystem.CommonUsages.LeftHand}");
        rightController = InputSystem.AddDevice<LTSimulatedController>($"{nameof(LTSimulatedController)} - {UnityEngine.InputSystem.CommonUsages.RightHand}");
        InputSystem.SetDeviceUsage(leftController, UnityEngine.InputSystem.CommonUsages.LeftHand);
        InputSystem.SetDeviceUsage(rightController, UnityEngine.InputSystem.CommonUsages.RightHand);
    }

    public void UpdateWithStream(VRState s) {
        hmdDeviceState.centerEyePosition = s.headset.position;
        hmdDeviceState.centerEyeRotation = s.headset.rotation;
        hmdDeviceState.devicePosition = hmdDeviceState.centerEyePosition;
        hmdDeviceState.deviceRotation = hmdDeviceState.centerEyeRotation;
        leftControllerState.devicePosition = s.leftHand.position;
        leftControllerState.deviceRotation = s.leftHand.rotation;
        rightControllerState.devicePosition = s.rightHand.position;
        rightControllerState.deviceRotation = s.rightHand.rotation;
        leftInputState = s.leftInput;
        rightInputState = s.rightInput;
    }
}
