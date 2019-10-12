using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackground : MonoBehaviour {

    private Image image;
    private float fadeTime = 0.5f;
    private float maxAlpha = 0.99f;
    private Button clickableBackground;
    private RectTransform rect;
    private GameStateController gameStateController;

    private void Awake() {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        clickableBackground = GetComponent<Button>();
        gameStateController = FindObjectOfType<GameStateController>();

        EventManager.StartListening("GamePaused", OnGamePaused);
        EventManager.StartListening("GameResumed", OnGameResumed);
    }

    private void Start() {
        OnAppStart();
    }

    public void TappedOnPauseBackground() {
        gameStateController.tappedOnPauseBackground = true;
    }

    public void Interactable(bool state) {
        clickableBackground.interactable = state;
    }

    // Used when showing the skillMenu, so the skillBar is not covered by the pauseBackground
    public void SetBottomMargin(float value) {
        rect.offsetMin = new Vector2(rect.offsetMin.x, value);
    }

    public void OnAppStart() {
        Interactable(false);
        image.enabled = true;
        Color startColor = image.color;
        startColor.a = 0f;
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
        Interactable(true);
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
        Interactable(false);
        image.enabled = false;
        yield return null;
    }
}
