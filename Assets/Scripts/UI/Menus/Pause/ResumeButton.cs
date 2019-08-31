using UnityEngine;

public class ResumeButton : MonoBehaviour {

    private GameStateController gameStateController;

    private void Awake() {
        gameStateController = FindObjectOfType<GameStateController>();
    }

    public void ResumeOnClick() {
        gameStateController.resumeButtonPressed = true;
    }
}
