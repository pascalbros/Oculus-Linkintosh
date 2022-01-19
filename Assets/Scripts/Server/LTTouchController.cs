using System.Collections;
using UnityEngine;

public enum LTTouchControllerType {
    Left, Right
}

public class LTTouchController : MonoBehaviour {
    public LTTouchControllerType controllerType;

    private Animator animator = null;
    private XRInputListener inputListener;
    private void Start() {
        animator = GetComponent<Animator>();
        if (inputListener == null) {
            inputListener = FindObjectOfType<XRInputListener>();
        }
    }

    private void Update() {
        if (inputListener == null) {
            return;
        }
        var state = controllerType == LTTouchControllerType.Left ? inputListener.leftState : inputListener.rightState;
        animator.SetFloat("Button 1", state.GetButton(LTControllerButton.PrimaryButton) ? 1 : 0);
        animator.SetFloat("Button 2", state.GetButton(LTControllerButton.SecondaryButton) ? 1 : 0);
        animator.SetFloat("Joy X", state.axis2D.primary.x);
        animator.SetFloat("Joy Y", state.axis2D.primary.y);
        animator.SetFloat("Grip", state.GetButton(LTControllerButton.GripButton) ? 1 : 0);
        animator.SetFloat("Trigger", state.GetButton(LTControllerButton.TriggerButton) ? 1 : 0);
    }
}