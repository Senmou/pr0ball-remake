using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {

    private MoveUI moveUI;
    private Canvas canvas;
    private TextMeshProUGUI value;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        canvas = FindObjectOfType<Canvas>();
        value = GetComponent<TextMeshProUGUI>();
        moveUI.FadeTo(new Vector2(transform.position.x, transform.position.y + 1f), 0.8f);
        Destroy(gameObject, 0.9f);
    }

    public void SetText(string text) {
        value.text = text;
    }
}
