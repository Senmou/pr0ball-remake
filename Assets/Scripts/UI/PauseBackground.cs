using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackground : MonoBehaviour {

    private Image image;
    private float fadeTime = 0.5f;
    private float maxAlpha = 0.99f;

    private void Awake() {
        image = GetComponent<Image>();
        EventManager.StartListening("GamePaused", OnGamePaused);
        EventManager.StartListening("GameResumed", OnGameResumed);
    }

    private void Start() {
        OnAppStart();
    }

    public void OnAppStart() {
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

    private IEnumerator FadeIn() {
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
