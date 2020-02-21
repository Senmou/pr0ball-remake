using MarchingBytes;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour {

    public Color particleColor;
    [SerializeField] private GameObject particleSystem;

    [HideInInspector] public int maxHP;
    [HideInInspector] public int currentHP;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public bool canTakeDamageFromSkill;

    private Animator animator;
    private TextMeshProUGUI healthPointUI;
    private EnemyController enemyController;

    private int benisValue = 1;

    protected void Awake() {
        animator = GetComponent<Animator>();
        body = GetComponentInChildren<Rigidbody2D>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        canTakeDamageFromSkill = true;
    }

    private void Start() {
        UpdateUI();
    }

    // Helper function for adding healthPoints to enemies after "step" levels
    protected int HP(int hp, int step) {
        return (LevelData.Level / step) * hp;
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

    public void TakeDamage(Ball ball) {
        int amount = ball.Damage();
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            ParticleSystem deathParticles = Instantiate(particleSystem, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var particleVelocity = deathParticles.velocityOverLifetime;
            particleVelocity.x = (ball.transform.position.x < transform.position.x) ? 10f : -10f;

            ReturnToPool(this);
            Score.instance.IncScore(benisValue);
            OnDeath();
        }
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            Instantiate(particleSystem, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            ReturnToPool(this);
            Score.instance.IncScore(benisValue);
            OnDeath();
        }
    }

    protected virtual void OnDeath() {
        EventManager.TriggerEvent("EnemyDied");
    }

    public void UpdateUI() {
        healthPointUI.text = currentHP.ToStringFormatted();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        animator.SetTrigger("hit");

        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        TakeDamage(ball);
    }
}
