using System.Collections;
using UnityEngine;

public class ErrorMessage : MonoBehaviour {

    public static ErrorMessage instance;

    private MoveUI moveUI;

    private void Awake() {
        instance = this;
        moveUI = GetComponent<MoveUI>();
    }

    public void Show(float duration) {
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
        yield return new WaitForSeconds(duration);
        FadeOut();
    }
}
