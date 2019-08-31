using MarchingBytes;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public long currentHP;
    public int benisValue;

    [HideInInspector]
    public long maxHP;

    [HideInInspector]
    public Rigidbody2D body;

    protected LevelController levelController;

    private EnemyController enemyController;
    private TextMeshProUGUI healthPointUI;

    protected void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        levelController = FindObjectOfType<LevelController>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        
        EventManager.StartListening("WaveCompleted", MoveEnemy);
        EventManager.StartListening("ReachedBossLevel", Despawn);
    }

    private void Start() {
        UpdateUI();
    }

    private void Despawn() {
        enemyController.activeEnemies.Remove(this);
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    public void SetData() {
        currentHP = maxHP;
        UpdateUI();
    }

    private void MoveEnemy() {
        transform.position += new Vector3(0f, 2f);
    }

    private void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            enemyController.activeEnemies.Remove(this);
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            Benis.instance.IncScore(benisValue);
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
