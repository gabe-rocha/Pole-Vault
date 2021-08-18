using System.Collections;
using System.Collections.Generic;

public static class Data
{
    public enum Events
    {
        OnTableLoaded,
        OnGameStarted,
        OnGamePaused,
        OnGameUnpaused,
        OnCorrectElementSelected,
        OnGameOver,
        OnTableReady,
        OnElementSelected,
        OnTimeRanOut,
        OnDrawerReady,
        OnElementDrawed,
        OnGameManagerReady,
        OnTimerStarted,
        OnWordDrawn,
    }

    public static float gameDuration = 30f;

    public static List<string> listOfWords = new List<string>(){
        "hello",
        "world",
        "stuff",
        "animal",
        "aliens"
    };
}