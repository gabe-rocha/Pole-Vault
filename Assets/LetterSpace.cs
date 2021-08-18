using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LetterSpace : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtLetter;

    public void SetLetter(string letter){
        txtLetter.text = letter;
    }
}