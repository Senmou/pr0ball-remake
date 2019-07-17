using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform top;
    public Transform ground;
    public Transform wallLeft;
    public Transform wallRight;

    private void Update() {

        float levelHeight = top.position.y - ground.position.y + top.localScale.y + ground.localScale.y;
        float levelWidth = wallRight.position.x - wallLeft.position.x + wallRight.localScale.x + wallLeft.localScale.x;

        float unitsPerPixelWidth = levelWidth / Screen.width;
        float unitsPerPixelHeight = levelHeight / Screen.height;

        float desiredHalfHeight = 0.5f * unitsPerPixelWidth * Screen.height;
        float desiredHalfWidth = 0.5f * levelHeight;

        

        float verticalExtend = Camera.main.orthographicSize * 2f;
        float horizontalExtend = 2f * Camera.main.orthographicSize * Screen.width / Screen.height;

        float levelAspect = levelWidth / levelHeight;

        if(Camera.main.aspect < levelAspect) {
            Camera.main.orthographicSize = desiredHalfHeight;
        } else {
            Camera.main.orthographicSize = desiredHalfWidth;
        }

        Debug.Log("levelAspect: " + levelAspect);
        Debug.Log("aspect: " + Camera.main.aspect);
    }
}
