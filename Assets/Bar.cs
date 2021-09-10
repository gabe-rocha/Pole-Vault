using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    [SerializeField] private Transform topBarTransform;

    private void OnEnable() {
        EventManager.Instance.StartListeningWithIntParam(EventManager.Events.JumpPowerCalculated, SetupTopBarHeight);
    }
    private void OnDisable() {
        EventManager.Instance.StartListeningWithIntParam(EventManager.Events.JumpPowerCalculated, SetupTopBarHeight);
    }

    private void SetupTopBarHeight(int jumpPower) {
        var pos = topBarTransform.position;
        pos.y += jumpPower;
        topBarTransform.position = pos;
    }
}