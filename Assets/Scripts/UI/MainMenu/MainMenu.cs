using UnityEngine;

public class MainMenu : MonoBehaviour {

    [HideInInspector] public bool visible;

    private MoveUI moveUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();

        visible = true;
    }

    public void Hide() {
        if (visible) {
            visible = false;
            Vector2 oldPos = transform.position;
            moveUI.FadeTo(oldPos + new Vector2(30f, 0f), 0.5f);
            GameController.instance.ResumeGame();
        }
    }

    public void Show() {
        if (!visible) {
            visible = true;
            Vector2 oldPos = transform.position;
            moveUI.FadeTo(oldPos + new Vector2(-30f, 0f), 0.5f);
            GameController.instance.PauseGame();
        }
    }
}
