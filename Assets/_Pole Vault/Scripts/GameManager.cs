using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform mainCanvas;
    private static GameManager instance;
    public static GameManager Instance { get => instance; private set => instance = value; }
    internal int playerJustHitBall = 0, playerOneScore = 0, playerTwoScore = 0, playerServing = 1;
    private int playerWhoJustScored;
    private bool playerOneWon = false, playerTwoWon = false, isCountingDown = true;
    private float jumpHeight;

    private void OnEnable() {
        EventManager.Instance.StartListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, OnWordIsCorrect);
    }

    internal string GetHeight() {
        return jumpHeight.ToString("0.00");
    }

    private void OnDisable() {
        EventManager.Instance.StopListeningWithFloatParam(EventManager.Events.OnWordIsCorrect, OnWordIsCorrect);
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            // DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = -1;

        } else {
            Destroy(this);
        }
    }

    private void OnWordIsCorrect(float height) {
        jumpHeight = height;
        EventManager.Instance.TriggerEvent(EventManager.Events.GameEnded);
    }

    internal int GetWinner() {
        return playerOneWon ? 1 : 2;
    }

    IEnumerator Start() {
        yield return new WaitForSeconds(1f); //wait everything set themselves up
        EventManager.Instance.TriggerEvent(EventManager.Events.GameManagerReady);
        // EventManager.Instance.TriggerEvent(EventManager.Events.GetReadyForSetBegin);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            EventManager.Instance.TriggerEvent(EventManager.Events.OnWordIsCorrect);
        }
    }

    // private void OnPlayerHitBall(int playerNumber) {
    //     playerJustHitBall = playerNumber;
    // }
    // private void OnBallHitGround(bool leftSide) {

    //     // if (leftSide)
    //     // Debug.Log("Ball hit left side");
    //     // else
    //     // Debug.Log("Ball hit right side");

    //     if (playerJustHitBall == 1 && !leftSide || playerJustHitBall == 2 && !leftSide) {
    //         playerOneScore++;
    //         playerServing = 1;
    //         playerWhoJustScored = 1;

    //     } else if (playerJustHitBall == 2 && leftSide || playerJustHitBall == 1 && leftSide) {
    //         playerTwoScore++;
    //         playerServing = 2;
    //         playerWhoJustScored = 2;
    //     }

    //     if (playerOneScore >= Data.MatchScoreTarget) {
    //         playerOneWon = true;
    //         EventManager.Instance.TriggerEvent(EventManager.Events.MatchEnded);
    //     } else if (playerTwoScore >= Data.MatchScoreTarget) {
    //         playerTwoWon = true;
    //         EventManager.Instance.TriggerEvent(EventManager.Events.MatchEnded);
    //     } else {
    //         StartCoroutine(StartNextSetCor());
    //     }

    // }

    // private IEnumerator StartNextSetCor() {
    //     yield return new WaitForSeconds(3f);
    //     EventManager.Instance.TriggerEvent(EventManager.Events.GetReadyForSetBegin);
    // }

    // private void OnCountDownOver() {
    //     isCountingDown = false;
    //     EventManager.Instance.TriggerEvent(EventManager.Events.StartSet);
    // }
    // private void OnBallIsInPosition() {
    //     if (isCountingDown) {
    //         return;
    //     }

    //     EventManager.Instance.TriggerEvent(EventManager.Events.StartSet);
    // }
    // internal int GetCurrentBallServer() {
    //     return playerServing;
    // }

    // internal int GetPlayerWhoJustScored() {
    //     return playerWhoJustScored;
    // }
}