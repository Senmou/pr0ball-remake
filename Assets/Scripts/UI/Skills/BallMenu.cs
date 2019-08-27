using UnityEngine;

public class BallMenu : MonoBehaviour {

    private PauseBackground pauseBackground;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    public void Show() {
        pauseBackground.EnableBackground();
        gameObject.SetActive(true);
    }

    public void Hide() {
        pauseBackground.DisableBackground();
        gameObject.SetActive(false);
    }
}
