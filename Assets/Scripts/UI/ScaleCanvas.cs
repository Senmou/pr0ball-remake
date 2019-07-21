using UnityEngine;

public class ScaleCanvas : MonoBehaviour {

    public LevelScale level;

    private RectTransform rect;
    private float canvasWidth;
    private float canvasHeight;
    private float uiSpace = 4f;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        canvasWidth = level.levelWidth + 2f * level.wallThickness;
        canvasHeight = level.levelHeight + 2f * level.wallThickness + 2f * uiSpace;

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvasWidth);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, canvasHeight);
    }
}
