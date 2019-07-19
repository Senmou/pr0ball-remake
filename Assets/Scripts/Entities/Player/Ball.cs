using UnityEngine;

public class Ball : MonoBehaviour {

    public Cannon cannon;
    public BallConfig ballConfigDefault;

    public float damage = 1f;
    public float critChance = 0f;
    public float critDamageMultiplier = 1f;

    [HideInInspector] public Rigidbody2D body;

    private float startForce = 300f;
    private float maxVelocity = 50f;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GameObject.Find("SfxBounce").GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        cannon = FindObjectOfType<Cannon>();
        ballConfigDefault.Apply(this);
    }

    private void OnEnable() {
        body.AddForce(cannon.transform.up * startForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.Play();
    }
}
