using System;
using UnityEngine;

[Serializable]
public class VRItemTransform {
    public Vector3 position = Vector3.zero;
    public Vector3 eulerAngles = Vector3.zero;

    public void Update(Transform t) {
        position = t.localPosition;
        eulerAngles = t.localEulerAngles;
    }
}

[Serializable]
public class VRTransform
{
    public VRItemTransform headset = new();
    public VRItemTransform rightHand = new();
    public VRItemTransform leftHand = new();

    VRItemTransform[] items;

    public void Setup() {
        items = new VRItemTransform[3] { headset, rightHand, leftHand};
    }

    public void Update(Transform[] t) {
        for (int i = 0; i < t.Length; i++) {
            items[i].Update(t[i]);
        }
    }
}