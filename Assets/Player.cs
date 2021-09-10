using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Transform jumpStartPosition, jumpTopPosition, jumpTopPositionLeft, jumpTopPositionRight, jumpEndPosition;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float fallSpeed = 0.5f;
    [SerializeField] private Animator animPole;
    [SerializeField] private Transform poleTransform;

    private Animator animPlayer;
    private float jumpPower;

    // private ParabolaController parabolaController;

    private void OnEnable() {
        EventManager.Instance.StartListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, RunAndJump);
    }
    private void OnDisable() {
        EventManager.Instance.StopListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, RunAndJump);
    }

    private void Awake() {
        // parabolaController = GetComponent<ParabolaController>();
        animPlayer = GetComponent<Animator>();
    }

    private void RunAndJump(float jumpPower) {
        this.jumpPower = jumpPower;
        StartCoroutine(RunAndJumpCor());
    }

    private IEnumerator RunAndJumpCor() {

        //set correct height for jump start height
        // var pos = jumpStartPosition.position;
        // pos.y = transform.position.y;
        // jumpStartPosition.position = pos;

        //set bar height
        // var barPosition = jumpTopPosition.position;
        // barPosition.y += jumpPower * 100;
        // jumpTopPosition.position = barPosition;

        // float safeOffset = 0.9f; //10%
        //Run
        Debug.Log("Running");
        animPlayer.SetTrigger("Run");
        animPole.SetTrigger("Run");
        var targetPos = jumpStartPosition.position;
        targetPos.y = transform.position.y;
        while (transform.position.x <= targetPos.x) {

            // transform.position = Vector3.MoveTowards(transform.position, targetPos, runSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        //jump
        Debug.Log("Jumping");
        animPlayer.SetTrigger("Jump");
        animPole.SetTrigger("Jump");

        targetPos = jumpTopPosition.position;
        targetPos.y = transform.position.y;
        while (transform.position.x <= targetPos.x) {

            transform.Translate(Vector3.right * runSpeed * 0.75f * Time.deltaTime, Space.World);
            yield return null;
        }

        poleTransform.parent = null;
        Debug.Log("Moving to Fall Position");
        targetPos = jumpEndPosition.position;
        targetPos.y = transform.position.y;
        while (transform.position.x <= targetPos.x) {

            // transform.position = Vector3.MoveTowards(transform.position, targetPos, runSpeed / 2 * Time.deltaTime);
            transform.Translate(Vector3.right * runSpeed / 2 * Time.deltaTime, Space.World);
            yield return null;
        }

        // parabolaController.RefreshTransforms(5f);
        // parabolaController.FollowParabola();

        // while (transform.position.y <= jumpTopPositionLeft.position.y * safeOffset) {
        //     transform.position = Vector3.MoveTowards(transform.position, jumpTopPositionLeft.position, jumpSpeed * Time.deltaTime);
        //     yield return null;
        // }
        // while (transform.position.x <= jumpTopPositionRight.position.x * safeOffset) {
        //     transform.position = Vector3.Slerp(transform.position, jumpTopPositionRight.position, jumpSpeed / 500 * Time.deltaTime);
        //     yield return null;
        // }

        // //fall
        // anim.SetTrigger("Fall");
        // while (transform.position.y >= jumpEndPosition.position.y * safeOffset) {
        //     transform.position = Vector3.MoveTowards(transform.position, jumpEndPosition.position, fallSpeed * Time.deltaTime);
        //     yield return null;
        // }

        // anim.SetTrigger("Land");
        EventManager.Instance.TriggerEvent(EventManager.Events.OnPlayerLanded);
    }
}