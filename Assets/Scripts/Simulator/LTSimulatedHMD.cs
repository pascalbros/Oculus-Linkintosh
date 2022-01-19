using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using UnityEngine.Scripting;

[InputControlLayout(stateType = typeof(LTSimulatedHMDState), isGenericTypeOfDevice = false, displayName = "LT Simulated HMD")]
[Preserve]
public class LTSimulatedHMD : XRHMD {
}
