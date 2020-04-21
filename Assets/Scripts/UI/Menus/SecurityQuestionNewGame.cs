using UnityEngine;

public class SecurityQuestionNewGame : CanvasController {

    private RestartGame restartGame;

    private void Awake() {
        transform.position = new Vector2(0f, -3.5f);
        restartGame = FindObjectOfType<RestartGame>();
    }

    public void OnYesButtonClicked() {
        restartGame.StartNewGame();
    }

    public override void Show() {
        LeanTween.scale(gameObject, Vector3.one, 0.1f)
            .setIgnoreTimeScale(true)
            .setEase(showEaseType);
    }

    public override void Hide() {
        LeanTween.scale(gameObject, Vector3.zero, 0.15f)
            .setIgnoreTimeScale(true)
            .setEase(hideEaseType);
    }
}
