using System.Collections;
using UnityEngine;

public class UIMovement : MonoBehaviour {

    public Vector2 worldPos;
    public Vector2 sizeInUnits;

    private Canvas canvas;

    private void OnValidate() {
        SetPos(worldPos);
    }

    private void Awake() {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    private void Start() {
        SetPos(worldPos);
        SetSize();
    }

    public void SetPos(Vector2 targetPos) {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetPos);
        transform.position = screenPos;
    }

    public void FadeTo(Vector2 targetPos, float timeToFade) {
        StopAllCoroutines();
        StartCoroutine(FadePosition(targetPos, timeToFade));
    }

    private IEnumerator FadePosition(Vector2 targetPos, float timeToFade) {
        Vector2 screenTargetPos = Camera.main.WorldToScreenPoint(targetPos);

        float t = 0f;
        while (t < 1f) {
            transform.position = Vector2.Lerp(transform.position, screenTargetPos, t);
            t += Time.unscaledDeltaTime / timeToFade;
            yield return null;
        }
        yield return null;
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
