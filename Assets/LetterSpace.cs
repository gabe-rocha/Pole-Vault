using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LetterSpace : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtLetter;
    [SerializeField] private TMP_InputField inputField;

    public void SetLetter(string letter){
        inputField.text = letter;
    }

    public void SetFocus(){
        inputField.ActivateInputField();
    }

    public void ListenToChangeEvent(UnityAction<string> Callback){
        inputField.onValueChanged.AddListener(Callback);
    }

    public void ListenToEndEvent(UnityAction<string> Callback){
        inputField.onEndEdit.AddListener(Callback); //this is to detect the Enter keys
    }
}