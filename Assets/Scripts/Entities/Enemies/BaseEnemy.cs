using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public int healthPoints;
    public int benisValue;

    public Rigidbody2D body;

    private TextMeshProUGUI healthPointUI;

    private void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateUI();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        TakeDamage(1);
    }

    private void TakeDamage(int amount) {
        healthPoints -= amount;
        UpdateUI();
        if (healthPoints <= 0)
            Destroy(gameObject);
    }

    private void UpdateUI() {
        healthPointUI.text = healthPoints.ToString();
    }
}
