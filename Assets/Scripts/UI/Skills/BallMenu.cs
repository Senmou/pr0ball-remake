using UnityEngine;

public class BallMenu : MonoBehaviour {

    private PauseBackground pauseBackground;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public void Show() {
        GameController.instance.PauseGame();
        gameObject.SetActive(true);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        gameObject.SetActive(false);
    }
}
