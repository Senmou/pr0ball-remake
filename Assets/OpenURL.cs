using UnityEngine;

public class OpenURL : MonoBehaviour {

    public void OpenURLOnClick(string url) {
        Application.OpenURL(url);
    }
}
