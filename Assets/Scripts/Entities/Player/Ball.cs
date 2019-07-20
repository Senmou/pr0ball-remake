using MarchingBytes;
using System.Collections;
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
    private Transform bezierMidPoint;
    private Transform bezierEndPoint;
    private BallController ballController;

    private void Awake() {
        audioSource = GameObject.Find("SfxBounce").GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        cannon = FindObjectOfType<Cannon>();
        ballController = FindObjectOfType<BallController>();
        bezierMidPoint = GameObject.FindGameObjectWithTag("BezierMidPoint").transform;
        bezierEndPoint = GameObject.FindGameObjectWithTag("BezierEndPoint").transform;
        ballConfigDefault.Apply(this);
    }

    private void OnEnable() {
        body.AddForce(cannon.transform.up * startForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    private void ReturnToPool() {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    public void Move(float timeToReachEndPoint, GameStateController controller) {
        StartCoroutine(MoveToPosition(timeToReachEndPoint, controller));
    }

    public IEnumerator MoveToPosition(float timeToReachEndPoint, GameStateController controller) {
        float t = 0f;
        Vector2 startPos = transform.position;
        Vector3 midPointTest = new Vector2(20f, startPos.y);
        Vector3 midPointOffset = new Vector2(Random.Range(-45f, 30f), 0f);
        while (t < 1f) {
            transform.position = Bezier(startPos, midPointTest + midPointOffset, bezierEndPoint.position, t);
            t += Time.deltaTime / timeToReachEndPoint;
            yield return null;
        }
        ReturnToPool();
        ballController.RemoveFromList(this);
        controller.cycleFinished = EasyObjectPool.instance.AllObjectsReturnedToPool(ballController.poolName, ballController.MaxBallCount);
        yield return null;
    }

    public Vector2 Bezier(Vector2 a, Vector2 b, Vector2 c, float t) {
        var ab = Vector2.Lerp(a, b, t);
        var bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.Play();
    }
}
