using UnityEngine.UI;
using UnityEngine;

public class PauseBackground : MonoBehaviour {

    public float fadeTime;

    public bool disableInteractability;

    private Image image;
    private float maxAlpha = 0.99f;
    private Button clickableBackground;
    private RectTransform rect;

    private void Awake() {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        clickableBackground = GetComponent<Button>();

        EventManager.StartListening("GamePaused", OnGamePaused);
        EventManager.StartListening("GameResumed", OnGameResumed);
    }

    private void Start() {
        OnAppStart();
    }

    // Used when showing the skillMenu, so the skillBar is not covered by the pauseBackground
    public void SetBottomMargin(float value) {
        rect.offsetMin = new Vector2(rect.offsetMin.x, value);
    }

    public void SetAlpha(float value) {
        StopAllCoroutines();
        Color color = image.color;
        color.a = value;
        image.color = color;
    }

    public void OnAppStart() {
        clickableBackground.interactable = false;
        image.enabled = true;
        Color startColor = image.color;
        startColor.a = 0f;
        image.color = startColor;
    }

    private void OnGamePaused() {
        FadeIn();
    }

    private void OnGameResumed() {
        FadeOut();
    }

    private void FadeIn() {
        clickableBackground.interactable = true;
        if (disableInteractability)
            clickableBackground.interactable = false;

        Color color = image.color;

        LeanTween.value(image.color.a, 1f, fadeTime)
            .setOnStart(() => image.enabled = true)
            .setEase(LeanTweenType.easeOutExpo)
            .setIgnoreTimeScale(true)
            .setOnUpdate((alpha) => {
                color.a = alpha;
                image.color = color;
            });
    }
    
    private void FadeOut() {

        Color color = image.color;

        LeanTween.value(image.color.a, 0f, fadeTime)
             .setEase(LeanTweenType.easeOutExpo)
             .setIgnoreTimeScale(true)
             .setOnUpdate((alpha) => {
                 color.a = alpha;
                 image.color = color;
             })
            .setOnComplete(() => {
                clickableBackground.interactable = false;
                image.enabled = false;
            });
    }
}
