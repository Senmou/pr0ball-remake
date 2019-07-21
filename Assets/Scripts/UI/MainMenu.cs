using UnityEngine;

public class MainMenu : MonoBehaviour {

    private MoveUI moveUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public void HideMainMenu() {
        Vector2 oldPos = transform.position;
        moveUI.FadeTo(oldPos + new Vector2(30f, 0f), 0.5f);
        EventManager.TriggerEvent("GameResumed");
    }
}
