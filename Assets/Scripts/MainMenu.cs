using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void OnClickStartButton() {
        SceneManager.LoadScene(1);
    }

    public void OnClickOptionsButton() {
        SceneManager.LoadScene(2);
    }
}
