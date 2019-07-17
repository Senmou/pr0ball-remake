using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform wallLeft;
    public Transform wallRight;

    private void Update() {

        float levelWidth = wallRight.position.x - wallLeft.position.x + wallRight.localScale.x + wallLeft.localScale.x;

        float unitsPerPixel = levelWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        Camera.main.orthographicSize = desiredHalfHeight;
    }
}
