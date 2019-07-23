using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent OnLongClick;

    private float holdDownTime = 0.15f;
    private float counter;
    private bool pointerDown;

    public void OnPointerUp(PointerEventData eventData) {
        pointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        pointerDown = true;
    }

    private void Update() {
        if (pointerDown)
            counter += Time.unscaledDeltaTime;

        if (!pointerDown)
            counter = 0f;

        if (counter >= holdDownTime) {
            OnLongClick?.Invoke();
            counter = 0f;
        }
    }
}
