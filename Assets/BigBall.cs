using MarchingBytes;
using UnityEngine;

public class BigBall : MonoBehaviour {

    private const string particleSystemPoolName = "OrangeParticleSystem_Pool";

    [SerializeField] private GameObject onHitParticleSystem;

    private AudioSource audioSource;
    private EnemyController enemyController;

    [HideInInspector] public Rigidbody2D body;
    private float maxVelocity = 200f;

    private TrailRenderer trailRenderer;
    private int damage;

    private void OnEnable() {
        body.gravityScale = 0f;
    }

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        enemyController = FindObjectOfType<EnemyController>();
        trailRenderer.material.color = new Color(1, 0.4829951f, 0f, 1f); // orange
        audioSource = GameObject.Find("SfxOrangeBallHit").GetComponent<AudioSource>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private void Start() {
        transform.position = new Vector2(0f, 16f);
        //body.AddForce(new Vector2(Random.value, Random.value) * 300f, ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    public void SetDamage(int value) {
        damage = value;
    }

    private void OnWaveCompleted() {
        Destroy(gameObject);
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
            //audioSource.PlayOneShot(audioSource.clip);
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damage);
            EasyObjectPool.instance.GetObjectFromPool(particleSystemPoolName, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        }
    }
}
