using UnityEngine;

public class CanvasResizedWarning : MonoBehaviour {

    private void Update() {
        if (transform.localScale != new Vector3(1f, 1f, 1f))
            Debug.LogWarning("Canvas was resized! [" + transform.localScale.x + "|" + transform.localScale.y + "|" + transform.localScale.z + "]");
    }
}
