using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent OnLongClick;

    private float holdDownTimeLongClick = 0.15f;
    private float longClickCounter;
    private bool pointerDown;

    public void OnPointerUp(PointerEventData eventData) {
        pointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        pointerDown = true;
    }

    private void Update() {
        CheckForLongClick();
    }

    private void CheckForLongClick() {
        if (pointerDown)
            longClickCounter += Time.unscaledDeltaTime;

        if (!pointerDown)
            longClickCounter = 0f;

        if (longClickCounter >= holdDownTimeLongClick) {
            OnLongClick?.Invoke();
            longClickCounter = 0f;
        }
    }
}
