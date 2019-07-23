using UnityEngine;

public class StartButton : MonoBehaviour {

    public bool pressed;

    private GameStateController gameStateController;
    private MainMenu mainMenu;

    private void Awake() {
        gameStateController = FindObjectOfType<GameStateController>();
        mainMenu = FindObjectOfType<MainMenu>();
    }

    public void OnClick() {
        pressed = true;
        mainMenu.Hide();
    }
}
