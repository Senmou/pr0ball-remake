using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreButton : MonoBehaviour {

    public void ShowHighscoreTable() {
        SceneManager.LoadScene(1);
    }
}
