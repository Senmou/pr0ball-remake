using MarchingBytes;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {

    [HideInInspector] public Cannon cannon;
    [HideInInspector] public Rigidbody2D body;

    private float startForce = 300f;
    private float maxVelocity = 50f;

    private new Collider2D collider;
    private AudioSource audioSource;
    private Transform bezierEndPoint;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    private BallController ballController;
    private EnemyController enemyController;
    private Sound sound;

    private Vector2 randomEnemyPos = Vector2.zero;

    protected void Awake() {
        sound = FindObjectOfType<Sound>();
        body = GetComponent<Rigidbody2D>();
        cannon = FindObjectOfType<Cannon>();
        collider = GetComponent<Collider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ballController = FindObjectOfType<BallController>();
        enemyController = FindObjectOfType<EnemyController>();
        audioSource = GameObject.Find("SfxBounce").GetComponent<AudioSource>();
        bezierEndPoint = GameObject.FindGameObjectWithTag("BallCounterIcon").transform;
    }

    private void OnEnable() {
        body.gravityScale = 0f;
        collider.enabled = true;
        body.AddForce(cannon.transform.up * startForce, ForceMode2D.Impulse);
    }

    public void SetColor(Color color) {
        spriteRenderer.color = color;
        trailRenderer.material.color = color;
    }

    public void DisableCollider() {
        collider.enabled = false;
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    public int Damage() => BallStats.Instance.ModifiedDamage();

    public void Move(float timeToReachEndPoint, PlayStateController controller) {
        StartCoroutine(MoveToPosition(timeToReachEndPoint, controller));
    }

    public IEnumerator MoveToPosition(float timeToReachEndPoint, PlayStateController controller) {
        float t = 0f;
        Vector2 startPos = transform.position;
        Vector3 bezierMidPoint = new Vector2(20f, startPos.y);
        Vector3 bezierMidPointOffset = new Vector2(Random.Range(-45f, 30f), 0f);
        Vector2 endPoint = bezierEndPoint.position;
        while (t < 1f) {
            transform.position = Bezier(startPos, bezierMidPoint + bezierMidPointOffset, endPoint, t);
            t += Time.deltaTime / timeToReachEndPoint;
            yield return null;
        }
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        ballController.RemoveFromList(this);

        if (ballController.BallCount == 0)
            controller.cycleFinished = true;

        yield return null;
    }

    private void SetNewRandomTarget() {
        randomEnemyPos = enemyController.GetRandomTarget();
    }

    private void MoveToEnemy() {
        Vector2 direction = (randomEnemyPos - (Vector2)transform.position).normalized;
        body.AddForce(direction * 20, ForceMode2D.Impulse);
    }

    public Vector2 Bezier(Vector2 a, Vector2 b, Vector2 c, float t) {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        body.gravityScale = 5f;

#if UNITY_ANDROID && !UNITY_EDITOR
        sound.Bounce();
#else
        audioSource.PlayOneShot(audioSource.clip);
#endif
        SetNewRandomTarget();
    }
}
