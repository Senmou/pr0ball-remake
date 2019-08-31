using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public void StartGameOnClick() {
        SceneManager.LoadScene(1);
    }
}
