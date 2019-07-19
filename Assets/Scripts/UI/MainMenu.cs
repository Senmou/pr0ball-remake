using UnityEngine;

public class MainMenu : MonoBehaviour {

    private UIMovement moveUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<UIMovement>();
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public void HideMainMenu() {
        moveUI.FadeTo(new Vector2(30f, 0f), 0.5f);
        EventManager.TriggerEvent("GameResumed");
    }
}
