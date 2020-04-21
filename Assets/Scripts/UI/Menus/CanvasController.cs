using UnityEngine;

public class CanvasController : MonoBehaviour {

    public CanvasType canvasType;
    protected float showEaseDuration;
    protected float hideEaseDuration;
    protected LeanTweenType showEaseType;
    protected LeanTweenType hideEaseType;

    protected void Start() {
        showEaseDuration = 0.2f;
        hideEaseDuration = 0.3f;
        showEaseType = LeanTweenType.linear;
        hideEaseType = LeanTweenType.easeInBack;
    }

    public virtual void Show() {

    }

    public virtual void Hide() {

    }
}
