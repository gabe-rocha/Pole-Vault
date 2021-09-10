using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data {

    public static float gameDuration = 30f;

    internal static readonly float DelayBeforeShowingFinalText = 0.5f;
    internal static readonly float DelayBeforeShowingScoredText = 1f;
    internal static readonly float DelayBeforeHidingScoredText = 2f;
    internal static readonly float DelayBeforeShowingFinalButtons = 5f;
    internal static readonly float fadeOutSpeed = 1f;

    internal static readonly Vector3 midTopOfTheScreen = new Vector3(0f, 4.21f, 0f);

    internal static readonly int MatchScoreTarget = 2;
    internal static readonly int CountdownValue = 2;

    internal static List<string> listOfWords = new List<string>() {
        "Alpha",
    };
}