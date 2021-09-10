using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    private void OnEnable() {
        EventManager.Instance.StartListeningWithIntParam(EventManager.Events.JumpPowerCalculated, SetupTopBarHeight);
    }
    private void OnDisable() {
        EventManager.Instance.StartListeningWithIntParam(EventManager.Events.JumpPowerCalculated, SetupTopBarHeight);
    }

    private void SetupTopBarHeight(int jumpPower) {
        var pos = transform.position;
        pos.y += jumpPower;
        transform.position = pos;
    }
}