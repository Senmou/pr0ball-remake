using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackground : MonoBehaviour {

    private Image image;
    private float fadeTime = 0.5f;
    private float maxAlpha = 0.85f;

    private void Awake() {
        image = GetComponent<Image>();
        EventManager.StartListening("GamePaused", OnGamePaused);
        EventManager.StartListening("GameResumed", OnGameResumed);
        image.raycastTarget = false;
    }

    private void OnGamePaused() {
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(maxAlpha));
        image.raycastTarget = true;
    }

    private void OnGameResumed() {
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(0f));
        image.raycastTarget = false;
    }

    private IEnumerator FadeAlpha(float alpha) {

        Color color = image.color;

        float t = 0f;
        while (t < 1f) {
            color.a = Mathf.Lerp(color.a, alpha, t);
            image.color = color;
            t += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        yield return null;
    }
}
