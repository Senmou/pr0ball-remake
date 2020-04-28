using MarchingBytes;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour {

    private const string deathParticlePoolName = "DeathParticleSystem_Pool";

    public Color enemyColor;
    [SerializeField] private Color uniColor;
    [SerializeField] protected EnemyHP hp;

    [HideInInspector] public int maxHP;
    [HideInInspector] public int currentHP;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public bool canTakeDamageFromSkill;
    [HideInInspector] public bool reachedStartingPosition;
    [HideInInspector] public CurrentLevelData.EntityType entityType;

    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI healthPointUI;
    private EnemyController enemyController;

    private int benisValue = 1;
    
    protected void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        canTakeDamageFromSkill = true;

        transform.FindChild<Canvas>("HealthPoints/Canvas").worldCamera = Camera.main;

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

    protected RemoteConfig.RemoteHealthMultiplier GetRemoteHealthMultiplier() => RemoteConfig.instance.remoteHealthMultiplier;

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
            SpawnParticleSystem(ball);
            ReturnToPool(this);
            Score.instance.IncScore(benisValue);
            OnDeath();
        }
    }

    public void TakeDamage(int amount, bool shouldIncScore = true) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            SpawnParticleSystem();
            ReturnToPool(this);
            if (shouldIncScore)
                Score.instance.IncScore(benisValue);
            OnDeath();
        }
    }

    private void SpawnParticleSystem(Ball ball = null) {
        if (PersistentData.instance.enableParticleSystems) {
            PooledParticleSystem deathParticles = EasyObjectPool.instance.GetObjectFromPool(deathParticlePoolName, transform.position, Quaternion.identity).GetComponent<PooledParticleSystem>();
            Color particleColor = (PersistentData.instance.uniColor) ? uniColor : enemyColor;
            deathParticles.SetColor(particleColor);

            if (ball) {
                var particleVelocity = deathParticles.particleSystem.velocityOverLifetime;
                particleVelocity.x = (ball.transform.position.x < transform.position.x) ? 10f : -10f;
            }
        }
    }

    public void Kill(bool shouldIncScore = true) {
        TakeDamage(currentHP, shouldIncScore);
    }

    protected virtual void OnDeath() {
        Statistics.Instance.enemies.killed++;
        EventManager.TriggerEvent("EnemyDied");
    }

    public void UpdateUI() {
        healthPointUI.text = currentHP.ToStringFormatted();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        LeanTween.scale(gameObject, new Vector2(1.85f, 1.85f), 0.05f).setOnComplete(() => {
            LeanTween.scale(gameObject, new Vector2(2f, 2f), 0.05f);
        });
        LeanTween.rotateZ(gameObject, 7.5f, 0.05f).setOnComplete(() => {
            LeanTween.rotateZ(gameObject, -7.5f, 0.05f).setOnComplete(() => {
                LeanTween.rotateZ(gameObject, 0f, 0.05f);
            });
        });

        TakeDamage(ball);
        Statistics.Instance.balls.damageDealt += ball.Damage();
    }
}
