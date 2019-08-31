using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    private MoveUI moveUI;

    private bool isVisible;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();

        EventManager.StartListening("BackButtonPressed", OnBackButtonPressed);
    }

    private void OnBackButtonPressed() {
        if (isVisible)
            FadeOut();
    }

    public void FadeIn() {
        isVisible = true;
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void FadeOut() {
        isVisible = false;
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
    }
}
