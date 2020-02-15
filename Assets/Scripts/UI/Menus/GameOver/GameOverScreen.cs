using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    private MoveUI moveUI;
    private GameStateController gameStateController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameStateController = FindObjectOfType<GameStateController>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            gameStateController.isGameOver = true;
        }
    }

    public void Show() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(0f, 40f), 0.5f);
    }
}
