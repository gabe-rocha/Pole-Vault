using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpHeightDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI txtWhiteOutline, txtBlackOutline, txtPhrase;

    private Animator anim;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.OnPlayerLanded, PlayerLanded);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.OnPlayerLanded, PlayerLanded);

    }

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void PlayerLanded() {
        StartCoroutine(WaitAndShow());
    }

    private IEnumerator WaitAndShow() {
        yield return new WaitForSeconds(Data.DelayBeforeShowingFinalText);

        txtPhrase.text = $"You Jumped {GameManager.Instance.GetHeight()}m High!";
        txtBlackOutline.text = txtPhrase.text;
        txtWhiteOutline.text = txtPhrase.text;

        anim.SetTrigger("Show");
    }
}