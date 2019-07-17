using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform top;
    public Transform ground;
    public Transform wallLeft;
    public Transform wallRight;

    private void Awake() {

        // Outer bounds of the walls
        float levelHeight = top.position.y - ground.position.y + top.localScale.y + ground.localScale.y;
        float levelWidth = wallRight.position.x - wallLeft.position.x + wallRight.localScale.x + wallLeft.localScale.x;

        float unitsPerPixelWidth = levelWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixelWidth * Screen.height;
        float desiredHalfWidth = 0.5f * levelHeight;

        float levelAspect = levelWidth / levelHeight;

        if (Camera.main.aspect < levelAspect) {
            Camera.main.orthographicSize = desiredHalfHeight;
        } else {
            Camera.main.orthographicSize = desiredHalfWidth;
        }
    }
}
