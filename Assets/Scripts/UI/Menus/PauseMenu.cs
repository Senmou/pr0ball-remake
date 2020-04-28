using UnityEngine;

public class PauseMenu : CanvasController {

    private PauseBackground pauseBackground;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public override void Hide() {
        LeanTween.move(gameObject, new Vector2(30f, 0f), 0.15f)
            .setIgnoreTimeScale(true)
            .setEase(showEaseType)
            .setOnComplete(() => {
                pauseBackground.disableInteractability = false;
                gameObject.SetActive(false);
            });
    }

    public override void Show() {
        LeanTween.move(gameObject, Vector2.zero, 0.15f)
            .setOnStart(() => gameObject.SetActive(true))
            .setIgnoreTimeScale(true)
            .setEase(showEaseType)
            .setOnComplete(() => {
                pauseBackground.disableInteractability = true;
            });
    }
}
