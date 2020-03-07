using MarchingBytes;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour {

    public Color enemyColor;
    [SerializeField] private Color uniColor;
    [SerializeField] protected EnemyHP hp;
    [SerializeField] private new GameObject particleSystem;

    [HideInInspector] public int maxHP;
    [HideInInspector] public int currentHP;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public bool canTakeDamageFromSkill;
    [HideInInspector] public CurrentLevelData.EntityType entityType;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI healthPointUI;
    private EnemyController enemyController;

    private int benisValue = 1;

    protected void OnEnable() {
        maxHP = hp.MaxHP;
    }

    protected void Awake() {
        animator = GetComponent<Animator>();
        body = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        canTakeDamageFromSkill = true;

        EventManager.StartListening("ToggleUniColor", OnToggleUniColor);
    }

    protected void Start() {
        UpdateUI();
        ApplyColor();
    }

    public Color GetColor() => spriteRenderer.color;

    protected void OnToggleUniColor() {
        ApplyColor();
    }

    protected void ApplyColor() {
        if (PersistentData.instance.uniColor)
            spriteRenderer.color = uniColor;
        else
            spriteRenderer.color = enemyColor;
    }

    public void SetData(int? hp = null) {
        if (hp != null)
            currentHP = (int)hp;
        else
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

            if (PersistentData.instance.uniColor)
                deathParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.SetColor(Shader.PropertyToID("Color_65DE3E46"), uniColor);
            else
                deathParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.SetColor(Shader.PropertyToID("Color_65DE3E46"), enemyColor);

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

    public void Kill() {
        TakeDamage(currentHP);
    }

    protected virtual void OnDeath() {
        Statistics.Instance.enemies.killed++;
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
        Statistics.Instance.balls.damageDealt += ball.Damage();
    }
}
