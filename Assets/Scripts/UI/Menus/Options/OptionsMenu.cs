using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    private MoveUI moveUI;
    private GameStateController gameStateController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        gameStateController = FindObjectOfType<GameStateController>();
    }
    
    public void OptionsButtonOnClick() {
        gameStateController.optionsButtonPressed = true;
    }

    public void CloseButtonOnClick() {
        gameStateController.optionsMenuCloseButtonPressed = true;
    }

    public void Show() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
        gameStateController.optionsButtonPressed = false;
    }
}
