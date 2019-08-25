using MarchingBytes;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public int currentHP;
    public int benisValue;

    [HideInInspector]
    public int maxHP;

    [HideInInspector]
    public Rigidbody2D body;

    private TextMeshProUGUI healthPointUI;

    protected void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        
        EventManager.StartListening("WaveCompleted", MoveEnemy);
    }

    private void Start() {
        UpdateUI();
    }

    public void SetData() {
        currentHP = maxHP;
        UpdateUI();
    }

    private void MoveEnemy() {
        transform.position += new Vector3(0f, 2.5f);
    }

    private void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            Benis.instance.IncScore(benisValue);
        }
    }

    public void UpdateUI() {
        healthPointUI.text = currentHP.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        TakeDamage(10);
    }
}
