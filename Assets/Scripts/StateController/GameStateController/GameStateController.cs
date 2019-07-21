using UnityEngine;

public class GameStateController : StateController {

    [HideInInspector] public StartButton startButton;
    [HideInInspector] public PlayStateController playStateController;

    private void Awake() {
        playStateController = FindObjectOfType<PlayStateController>();
        startButton = FindObjectOfType<StartButton>();
    }
}
