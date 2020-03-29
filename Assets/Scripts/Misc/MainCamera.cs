using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform top;
    public Transform ground;
    public Transform wallLeft;
    public Transform wallRight;

    private void OnValidate() {
        ScaleCamera();
    }

    private void Awake() {
        EventManager.StartListening("ToggleBlackBackground", OnToggleBlackBackground);
    }

    private void Start() {
        // Called in Start(), so the level's walls are already set up, which happens in Awake()
        ScaleCamera();
    }

    private void OnToggleBlackBackground() {
        if (PersistentData.instance.blackBackground)
            Camera.main.backgroundColor = new Color32(0, 0, 0, 1);
        else
            Camera.main.backgroundColor = new Color32(22, 22, 24, 1);
    }

    private void ScaleCamera() {

        float spaceForUI = 8f;

        // Outer bounds of the walls
        float levelHeight = top.position.y - ground.position.y + top.localScale.y + ground.localScale.y + spaceForUI;
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