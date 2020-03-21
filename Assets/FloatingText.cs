using UnityEngine;
using TMPro;
using MarchingBytes;
using System.Collections;

public class FloatingText : MonoBehaviour {

    private MoveUI moveUI;
    private Canvas canvas;
    private TextMeshProUGUI value;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        canvas = FindObjectOfType<Canvas>();
        value = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (PersistentData.instance.isGameOver == true)
            Destroy(gameObject);
    }

    public void SetText(string text) {
        value.text = text;
    }

    public void ReturnToPoolAfter(float seconds) {
        moveUI.FadeTo(new Vector2(transform.position.x, transform.position.y + 1f), 0.8f);
        StartCoroutine(ReturnToPoolDelayed(seconds));
    }

    private IEnumerator ReturnToPoolDelayed(float seconds) {
        yield return new WaitForSeconds(seconds);
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}
