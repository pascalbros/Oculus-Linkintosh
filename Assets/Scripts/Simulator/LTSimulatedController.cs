using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Scripting;

[InputControlLayout(stateType = typeof(LTSimulatedControllerState), commonUsages = new[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = false, displayName = "LT Simulated Controller"), Preserve]
public class LTSimulatedController : XRController {

    public Vector2Control primary2DAxis { get; private set; }
    public AxisControl trigger { get; private set; }
    public AxisControl grip { get; private set; }
    public Vector2Control secondary2DAxis { get; private set; }
    public ButtonControl primaryButton { get; private set; }
    public ButtonControl primaryTouch { get; private set; }
    public ButtonControl secondaryButton { get; private set; }
    public ButtonControl secondaryTouch { get; private set; }
    public ButtonControl gripButton { get; private set; }
    public ButtonControl triggerButton { get; private set; }
    public ButtonControl menuButton { get; private set; }
    public ButtonControl primary2DAxisClick { get; private set; }
    public ButtonControl primary2DAxisTouch { get; private set; }
    public ButtonControl secondary2DAxisClick { get; private set; }
    public ButtonControl secondary2DAxisTouch { get; private set; }
    public AxisControl batteryLevel { get; private set; }
    public ButtonControl userPresence { get; private set; }

    protected override void FinishSetup() {
        base.FinishSetup();
        primary2DAxis = GetChildControl<Vector2Control>(nameof(primary2DAxis));
        trigger = GetChildControl<AxisControl>(nameof(trigger));
        grip = GetChildControl<AxisControl>(nameof(grip));
        secondary2DAxis = GetChildControl<Vector2Control>(nameof(secondary2DAxis));
        primaryButton = GetChildControl<ButtonControl>(nameof(primaryButton));
        primaryTouch = GetChildControl<ButtonControl>(nameof(primaryTouch));
        secondaryButton = GetChildControl<ButtonControl>(nameof(secondaryButton));
        secondaryTouch = GetChildControl<ButtonControl>(nameof(secondaryTouch));
        gripButton = GetChildControl<ButtonControl>(nameof(gripButton));
        triggerButton = GetChildControl<ButtonControl>(nameof(triggerButton));
        menuButton = GetChildControl<ButtonControl>(nameof(menuButton));
        primary2DAxisClick = GetChildControl<ButtonControl>(nameof(primary2DAxisClick));
        primary2DAxisTouch = GetChildControl<ButtonControl>(nameof(primary2DAxisTouch));
        secondary2DAxisClick = GetChildControl<ButtonControl>(nameof(secondary2DAxisClick));
        secondary2DAxisTouch = GetChildControl<ButtonControl>(nameof(secondary2DAxisTouch));
        batteryLevel = GetChildControl<AxisControl>(nameof(batteryLevel));
        userPresence = GetChildControl<ButtonControl>(nameof(userPresence));
    }
}
