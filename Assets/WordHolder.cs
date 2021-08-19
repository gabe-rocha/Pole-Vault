using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class WordHolder : MonoBehaviour
{
    [SerializeField] private Transform jumpTopPosition;
    [SerializeField] private GameObject letterSpaceTemplate;
    [SerializeField] private TextMeshProUGUI txtJumpPower;
    [SerializeField] private float backspacePower = -0.3f;
    [SerializeField] private float letterPower = 0.15f;

    private string wordDrawn, wordTyped;
    private List<string> listOfWords;
    private List<GameObject> listOfLetters = new List<GameObject>();
    private int currentLetterIndex = 0;
    private float jumpPower = 1f;
    private AudioSource audioSource;

    private void OnEnable(){
        EventManager.Instance.StartListening(Data.Events.OnGameManagerReady, DrawAWord);
        EventManager.Instance.StartListening(Data.Events.OnEnterPressed, CheckIfWordIsCorrect);
    }
    private void OnDisable(){
        EventManager.Instance.StopListening(Data.Events.OnGameManagerReady, DrawAWord);
        EventManager.Instance.StopListening(Data.Events.OnEnterPressed, CheckIfWordIsCorrect);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        txtJumpPower.text = jumpPower.ToString("0.00" + 'm');
    }

    private void CheckIfWordIsCorrect()
    {
        if(wordDrawn == wordTyped){
            EventManager.Instance.TriggerEventWithFloatParam(Data.Events.OnWordIsCorrect, jumpPower);
        }
    }

    private void DrawAWord(){
        listOfWords = Data.listOfWords;

        int rng = Random.Range(0, listOfWords.Count);
        wordDrawn = listOfWords[rng];        
        InstantiateLetterSpaces();
        PlayWordAudio();

        EventManager.Instance.TriggerEventWithStringParam(Data.Events.OnWordDrawn, wordDrawn);

        Debug.Log($"Word Drawn: {wordDrawn}");
    }

    private void PlayWordAudio()
    {
        AudioClip clip = Resources.Load<AudioClip>($"Spoken Words/{wordDrawn}");
        audioSource.PlayOneShot(clip);
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
            go.GetComponent<LetterSpace>().ListenToChangeEvent(OnLetterChanged);
            go.GetComponent<LetterSpace>().ListenToEndEvent(OnEndEdit);

            // go.GetComponent<RectTransform>().localScale *= 3f / numLetters;

            listOfLetters.Add(go);
            
            //Update position for next letter
            letterPositionX += offset;
            position = new Vector3(letterPositionX, 0, 0);
        }
    }

    private void OnLetterChanged(string str)
    {
        if(Input.GetKeyDown(KeyCode.Backspace)){
            // Debug.Log("Pressed Backspace");

            if(wordTyped.Length > 0){
                wordTyped = wordTyped.Remove(wordTyped.Length - 1, 1);
            }
            
            if(currentLetterIndex > 0){
                currentLetterIndex--;
                jumpPower += backspacePower;
            }

        }
        else if(!string.IsNullOrEmpty(str)) {
            // Debug.Log($"Letter: {str}");

            if(currentLetterIndex < listOfLetters.Count){
                wordTyped += str.ToLower();
                jumpPower += letterPower;
            }
            currentLetterIndex++;
        }

        Debug.Log($"Word Typed: {wordTyped}");
        UpdateUI();
        UpdateTopBar();
    }

    private void UpdateTopBar()
    {
        //set bar height
        var barPosition = jumpTopPosition.position;
        barPosition.y = jumpPower * 300;
        jumpTopPosition.position = barPosition;
    }

    private void UpdateUI()
    {
        txtJumpPower.text = jumpPower.ToString("0.00" + 'm');
    }

    private void OnEndEdit(string str){
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Pressed Enter");
            
            EventManager.Instance.TriggerEventWithFloatParam(Data.Events.JumpPowerCalculated, jumpPower);
            EventManager.Instance.TriggerEvent(Data.Events.OnEnterPressed);
        }
    }

    private void Update()
    {
        //keep current letter in focus - I do it in Update to make sure the player won't change the current letter with the mouse
        if(currentLetterIndex < listOfLetters.Count)
            listOfLetters[currentLetterIndex].GetComponent<LetterSpace>().SetFocus();
    }
}