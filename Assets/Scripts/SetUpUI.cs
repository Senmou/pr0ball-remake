using UnityEngine;

public class SetUpUI : MonoBehaviour {

    public Vector2 worldPos;
    public Vector2 sizeInUnits;

    private Canvas canvas;

    private void OnValidate() {
        SetPos();
    }

    private void Awake() {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    //private void Start() {
    //    SetPos();
    //    SetSize();
    //}

    // Todo: just for testing, switch back to Start()
    private void Update() {
        SetPos();
        SetSize();
    }

    private void SetPos() {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;
    }

    private void SetSize() {

        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // screen dimension in units
        float width = Mathf.Abs(lowerLeft.x - upperRight.x);
        float height = Mathf.Abs(lowerLeft.y - upperRight.y);

        // pixel per unit
        float ppu = Screen.width / width;

        // divide by the canvas' scaleFactor to negate it's automatic down scaling by the canvas scaler
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ppu / canvas.scaleFactor);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ppu / canvas.scaleFactor);

        rect.localScale = sizeInUnits;
    }
}
