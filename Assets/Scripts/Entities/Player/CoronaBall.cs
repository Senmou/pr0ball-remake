using MarchingBytes;
using UnityEngine;

public class CoronaBall : MonoBehaviour {

    private const string particleSystemPoolName = "OrangeParticleSystem_Pool";

    [SerializeField] private GameObject onHitParticleSystem;

    private EnemyController enemyController;

    [HideInInspector] public Rigidbody2D body;
    private float maxVelocity = 200f;

    private int damage;

    private void OnEnable() {
        body.gravityScale = 0f;
    }

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start() {
        transform.position = new Vector2(0f, 16f);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    public void SetDamage(int value) {
        damage = value;
    }

    public void MoveToTopMostEnemy() {
        BaseEnemy topMostEnemy = null;
        foreach (var enemy in enemyController.activeEnemies) {
            if (topMostEnemy == null) {
                topMostEnemy = enemy;
                continue;
            }

            if (enemy.transform.position.y > topMostEnemy.transform.position.y)
                topMostEnemy = enemy;
        }

        if (topMostEnemy) {
            Vector2 directionToEnemy = topMostEnemy.transform.position - transform.position;
            body.AddForce(directionToEnemy * 300f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        body.gravityScale = 5f;

        if (other.gameObject.CompareTag("Enemy")) {
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damage);
            Statistics.Instance.skills.skill_3.damageDealt += damage;

            if (PersistentData.instance.enableParticleSystems)
                EasyObjectPool.instance.GetObjectFromPool(particleSystemPoolName, transform.position, Quaternion.identity);
        }
    }
}
