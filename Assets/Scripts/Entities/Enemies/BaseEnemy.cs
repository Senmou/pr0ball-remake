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

        EventManager.StartListening("WaveCompleted", MoveEnemy);
    }

    private void Start() {
        UpdateUI();
    }

    private void MoveEnemy() {
        transform.position += new Vector3(0f, 2.5f);
    }

    private void TakeDamage(int amount) {
        healthPoints -= amount;
        UpdateUI();
        if (healthPoints <= 0) {
            Destroy(gameObject);
            Benis.instance.IncScore(benisValue);
        }
    }

    private void UpdateUI() {
        healthPointUI.text = healthPoints.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        TakeDamage(1);
    }
}
