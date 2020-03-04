using System.Collections;
using UnityEngine;
using TMPro;

public class ErrorMessage : MonoBehaviour {

    public static ErrorMessage instance;

    private MoveUI moveUI;
    private TextMeshProUGUI messageUI;

    private void Awake() {
        instance = this;
        moveUI = GetComponent<MoveUI>();
        messageUI = transform.FindChild<TextMeshProUGUI>("Canvas/TextBackground/Text");

        GetComponentInChildren<Canvas>().sortingLayerName = "ErrorMessage";
    }

    public void Show(float duration, string text) {
        messageUI.text = text;
        StopAllCoroutines();
        StartCoroutine(FadeInOut(duration));
    }

    private void FadeIn() {
        moveUI.FadeTo(new Vector2(-7f, -12f), 0.5f);
    }

    private void FadeOut() {
        moveUI.FadeTo(new Vector2(-30f, -12f), 0.5f);
    }

    private IEnumerator FadeInOut(float duration) {
        FadeIn();
        yield return new WaitForSecondsRealtime(duration);
        FadeOut();
    }
}
