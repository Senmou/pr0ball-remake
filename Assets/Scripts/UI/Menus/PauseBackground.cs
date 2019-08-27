using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackground : MonoBehaviour {

    private Image image;
    private float fadeTime = 0.5f;
    private float maxAlpha = 0.99f;
    private Button clickableBackground;

    private void Awake() {
        image = GetComponent<Image>();
        clickableBackground = GetComponent<Button>();
        EventManager.StartListening("GamePaused", OnGamePaused);
        EventManager.StartListening("GameResumed", OnGameResumed);
    }

    private void Start() {
        OnAppStart();
    }

    public void OnAppStart() {
        //clickableBackground.interactable = false;
        image.enabled = true;
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;
    }

    private void OnGamePaused() {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private void OnGameResumed() {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void EnableBackground() {
        image.enabled = true;
        //clickableBackground.interactable = true;
    }

    public void DisableBackground() {
        image.enabled = false;
        //clickableBackground.interactable = false;
    }

    private IEnumerator FadeIn() {
        //clickableBackground.interactable = true;
        image.enabled = true;

        Color color = image.color;

        float t = 0f;
        while (t < 1f) {
            color.a = Mathf.Lerp(color.a, maxAlpha, t);
            image.color = color;
            t += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator FadeOut() {
        //clickableBackground.interactable = false;

        Color color = image.color;

        float t = 0f;
        while (t < 1f) {
            color.a = Mathf.Lerp(color.a, 0f, t);
            image.color = color;
            t += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        image.enabled = false;
        yield return null;
    }
}
