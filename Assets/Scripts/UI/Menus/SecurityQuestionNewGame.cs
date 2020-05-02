using UnityEngine;

public class SecurityQuestionNewGame : CanvasController {

    private RestartGame restartGame;

    private void Awake() {
        restartGame = FindObjectOfType<RestartGame>();
    }

    public void OnYesButtonClicked() {
        restartGame.StartNewGame();
    }

    public override void Show() {
        transform.position = new Vector2(0f, 0f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f)
            .setOnStart(() => gameObject.SetActive(true))
            .setIgnoreTimeScale(true)
            .setEase(showEaseType);
    }

    public override void Hide() {
        LeanTween.scale(gameObject, Vector3.zero, 0.15f)
            .setIgnoreTimeScale(true)
            .setEase(hideEaseType)
            .setOnComplete(() => gameObject.SetActive(false));
    }
}
