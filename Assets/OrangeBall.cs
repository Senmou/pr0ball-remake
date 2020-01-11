using UnityEngine;

public class OrangeBall : MonoBehaviour {

    [SerializeField] private GameObject onHitParticleSystem;

    private AudioSource audioSource;

    [HideInInspector] public Rigidbody2D body;
    private float maxVelocity = 80f;

    private void OnEnable() {
        body.gravityScale = 0f;
    }

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        audioSource = GameObject.Find("SfxOrangeBallHit").GetComponent<AudioSource>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private void OnWaveCompleted() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        body.gravityScale = 5f;
        
        if (other.gameObject.CompareTag("Enemy")) {
            audioSource.PlayOneShot(audioSource.clip);
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(10);
            Instantiate(onHitParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
