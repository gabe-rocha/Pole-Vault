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
        OnEnterPressed,
        OnWordIsCorrect,
        OnPlayerLanded,
        JumpPowerCalculated,
    }

    public static float gameDuration = 30f;

    public static List<string> listOfWords = new List<string>(){
        "alpha",
        // "asdf",
        // "asd",
        // "asdfasdfasdf",
        // "qwerqwerqwerqwer"
    };
}