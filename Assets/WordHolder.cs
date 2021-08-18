using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHolder : MonoBehaviour
{
    [SerializeField] private GameObject letterSpaceTemplate;

    private string wordDrawn;
    private List<string> listOfWords;

    private void OnEnable(){
        EventManager.Instance.StartListening(Data.Events.OnGameManagerReady, DrawAWord);
    }
    private void OnDisable(){
        EventManager.Instance.StopListening(Data.Events.OnGameManagerReady, DrawAWord);
    }

    private void DrawAWord(){
        listOfWords = Data.listOfWords;

        int rng = Random.Range(0, listOfWords.Count);
        wordDrawn = listOfWords[rng];

        InstantiateLetterSpaces();

        EventManager.Instance.TriggerEventWithStringParam(Data.Events.OnWordDrawn, wordDrawn);
    }

    private void InstantiateLetterSpaces()
    {
        int numLetters = wordDrawn.Length;

        float offset = 200f;    //TODO Magic Number
        float letterPositionX = (-numLetters/2f) * 200f; 
        Vector3 position = new Vector3(letterPositionX, 0, 0);
        
        for (var letter = 0; letter < numLetters; letter++)
        {
            var go = Instantiate(letterSpaceTemplate, gameObject.transform);
            go.GetComponent<RectTransform>().localPosition = position;
            letterPositionX += offset;
            position = new Vector3(letterPositionX, 0, 0);

            
        }
    }
}
