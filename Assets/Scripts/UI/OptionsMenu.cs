using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    private MoveUI moveUI;
    private MainMenu mainMenu;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        mainMenu = FindObjectOfType<MainMenu>();
    }

    public void OnClickBackButton() {
        SceneManager.LoadScene(0);
    }

    public void FadeIn() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void FadeOut() {
        if (!mainMenu.visible) {
            GameController.instance.ResumeGame();
            moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
        }
    }
}
