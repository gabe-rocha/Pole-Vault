using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Transform jumpStartPosition, jumpTopPosition, jumpTopPositionLeft, jumpTopPositionRight, jumpEndPosition;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float fallSpeed = 0.5f;
    private Animator anim;
    private float jumpPower;

    private void OnEnable() {
        EventManager.Instance.StartListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, RunAndJump);
    }
    private void OnDisable() {
        EventManager.Instance.StopListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, RunAndJump);
    }

    private void RunAndJump(float jumpPower) {
        this.jumpPower = jumpPower;
        StartCoroutine(RunAndJumpCor());
    }

    private IEnumerator RunAndJumpCor() {

        //set correct height for jump start height
        var pos = jumpStartPosition.position;
        pos.y = transform.position.y;
        jumpStartPosition.position = pos;

        //set bar height
        // var barPosition = jumpTopPosition.position;
        // barPosition.y += jumpPower * 100;
        // jumpTopPosition.position = barPosition;

        float safeOffset = 0.9f; //10%
        //Run
        anim.SetTrigger("Run");
        while (transform.position.x <= jumpStartPosition.position.x * safeOffset) {

            transform.position = Vector3.MoveTowards(transform.position, jumpStartPosition.position, runSpeed * Time.deltaTime);
            yield return null;
        }

        //jump
        anim.SetTrigger("Jump");

        while (transform.position.y <= jumpTopPositionLeft.position.y * safeOffset) {
            transform.position = Vector3.MoveTowards(transform.position, jumpTopPositionLeft.position, jumpSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.x <= jumpTopPositionRight.position.x * safeOffset) {
            transform.position = Vector3.Slerp(transform.position, jumpTopPositionRight.position, jumpSpeed / 500 * Time.deltaTime);
            yield return null;
        }

        //fall
        anim.SetTrigger("Fall");
        while (transform.position.y >= jumpEndPosition.position.y * safeOffset) {
            transform.position = Vector3.MoveTowards(transform.position, jumpEndPosition.position, fallSpeed * Time.deltaTime);
            yield return null;
        }

        anim.SetTrigger("Land");
        EventManager.Instance.TriggerEvent(EventManager.Events.OnPlayerLanded);
    }

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}