using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [HideInInspector] public bool visible;

    private MoveUI moveUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();

        visible = false;
    }

    public void Hide() {
        if (visible) {
            //Debug.Log("HIDE");
            GameController.instance.ResumeGame();
            visible = false;
            moveUI.FadeTo(new Vector2(30f, 0f), 0.5f);
        }
    }

    public void Show() {
        if (!visible) {
            //Debug.Log("SHOW");
            GameController.instance.PauseGame();
            pauseBackground.Interactable(false);
            visible = true;
            moveUI.FadeTo(Vector2.zero, 0.5f);
        }
    }
}
