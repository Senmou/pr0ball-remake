using MarchingBytes;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    [HideInInspector] public long maxHP;
    [HideInInspector] public long currentHP;
    [HideInInspector] public int benisValue;
    [HideInInspector] public Rigidbody2D body;

    private PlayerHP playerHP;
    private Transform deadline;
    private TextMeshProUGUI healthPointUI;
    private EnemyController enemyController;

    protected void Awake() {
        playerHP = FindObjectOfType<PlayerHP>();
        body = GetComponentInChildren<Rigidbody2D>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        deadline = GameObject.Find("Deadline").transform;
    }

    private void Start() {
        UpdateUI();
    }

    private void Update() {
        if (transform.position.y >= deadline.position.y) {
            ReturnToPool(this);
            playerHP.TakeDamage(1);
        }
    }
    
    public void SetData() {
        currentHP = maxHP;
        UpdateUI();
    }

    private void ReturnToPool(BaseEnemy enemy) {
        enemyController.activeEnemies.Remove(enemy);

        if (gameObject.activeSelf)
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    private void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            ReturnToPool(this);
            Score.instance.IncScore(benisValue);
        }
    }

    public void UpdateUI() {
        healthPointUI.text = currentHP.ToStringFormatted();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        TakeDamage(ball.Damage());
    }
}
