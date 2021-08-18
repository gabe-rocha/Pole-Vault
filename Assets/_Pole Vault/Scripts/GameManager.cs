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
        // EventManager.Instance.StartListening(Data.Events.OnTimeRanOut, OnTimeRanOut);
    }

    private void OnDisable()
    {
        // EventManager.Instance.StopListening(Data.Events.OnTimeRanOut, OnTimeRanOut);
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            // EventManager.Instance.TriggerEvent(Data.Events.OnCorrectElementSelected);
        }
    }
}