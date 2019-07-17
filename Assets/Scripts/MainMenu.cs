using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void OnClickStartButton() {
        SceneManager.LoadScene(1);
    }
}
