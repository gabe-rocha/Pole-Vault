using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }
    
    private void OnEnable()
    {
        EventManager.Instance.StartListening(Data.Events.OnPlayerLanded, PlayerLanded);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(Data.Events.OnPlayerLanded, PlayerLanded);
    }

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = -1;

        } else {
            Destroy(this);
        }
    }

    void Start(){
        EventManager.Instance.TriggerEvent(Data.Events.OnGameManagerReady);
    }

    private void PlayerLanded(){
        Debug.Log("Player landed!");
    }
}