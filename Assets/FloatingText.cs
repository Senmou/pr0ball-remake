using UnityEngine;

public class FloatingText : MonoBehaviour {

    private MoveUI moveUI;
    private Canvas canvas;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        canvas = FindObjectOfType<Canvas>();
        moveUI.FadeTo(new Vector2(transform.position.x, transform.position.y + 1f), 0.8f);
        Destroy(gameObject, 0.9f);
    }
}
