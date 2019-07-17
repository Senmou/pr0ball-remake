using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    public void OnClickBackButton() {
        SceneManager.LoadScene(0);
    }
}
