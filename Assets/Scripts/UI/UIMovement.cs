using System.Collections;
using UnityEngine;

public class UIMovement : MonoBehaviour {

    public Vector2 worldPos;
    public Vector2 sizeInUnits;
    public bool updatePosition;

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

    private void Update() {
        if (updatePosition && gameObject.activeSelf)
            SetPos(worldPos);
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

        // multiplied with two, because the world coords are from -xy to +xy
        Vector2 dimensionsInUnits = 2f * Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // pixel per unit
        float ppu = Screen.width / dimensionsInUnits.x;

        // used to scale down nested objects, which are scaled up by their parents
        Vector2 globalToLocalScaleFactor = GetGlobalToLocalScaleFactor(transform);

        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ppu / globalToLocalScaleFactor.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ppu / globalToLocalScaleFactor.y);

        rect.localScale = sizeInUnits;
    }

    private Vector3 GetGlobalToLocalScaleFactor(Transform t) {
        Vector3 factor = Vector3.one;

        while (true) {
            Transform tParent = t.parent;

            if (tParent != null) {
                factor.x *= tParent.localScale.x;
                factor.y *= tParent.localScale.y;
                factor.z *= tParent.localScale.z;

                t = tParent;
            } else {
                return factor;
            }
        }
    }

    private Vector3 GetWorldPosOffset(Transform t) {
        Vector3 offset = Vector3.zero;

        while (true) {
            Transform tParent = t.parent;
            if (tParent != null) {
                offset += Camera.main.ScreenToWorldPoint(tParent.position);
                t = tParent;
            } else
                return offset;
        }
    }
}
