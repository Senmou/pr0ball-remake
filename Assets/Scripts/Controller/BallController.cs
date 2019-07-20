using System.Collections.Generic;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class BallController : MonoBehaviour {

    [HideInInspector] public string poolName = "BallPool";

    public Slider lifeTimeSlider;
    public Transform spawnPoint;
    public TextMeshProUGUI maxBallCountUI;
    public TextMeshProUGUI currentBallCountUI;

    private int maxBallCount = 20;
    private float maxLifeTime = 5f;
    private float shootingRate = 0.08f;

    private int BallCount { get => balls.Count; }
    public int MaxBallCount { get => maxBallCount; }
    public bool LifeTimeExeeded { get => lifeTime <= 0f; }
    public bool AllBallsShot { get => BallCount == MaxBallCount; }

    private float lifeTime;
    private List<Ball> balls = new List<Ball>();

    private void Start() {
        InitData();
        UpdateBallCountUI();
    }

    public void OnCycleFinish(GameStateController controller) {
        lifeTime = maxLifeTime;
        lifeTimeSlider.maxValue = maxLifeTime;
        UpdateLifeTimeSlider();
        StartCoroutine(CollectBalls(controller));
    }

    public void DrainLifeTime() {
        lifeTime -= Time.deltaTime;
        UpdateLifeTimeSlider();
    }

    public void Shoot() {
        InvokeRepeating("ShootBall", 0f, shootingRate);
    }

    private IEnumerator CollectBalls(GameStateController controller) {

        Ball[] temp = new Ball[maxBallCount];
        balls.CopyTo(temp);

        foreach (Ball ball in temp) {
            ball.Move(Random.Range(0.2f, 0.5f), controller);
            yield return null;
        }
        yield return null;
    }

    public void RemoveFromList(Ball ball) {
        balls.Remove(ball);
        UpdateBallCountUI();
    }

    private void InitData() {
        lifeTime = maxLifeTime;
        lifeTimeSlider.maxValue = maxLifeTime;
        UpdateLifeTimeSlider();
    }

    private void ShootBall() {
        if (BallCount < maxBallCount) {
            CreateBall(spawnPoint.position);
            UpdateBallCountUI();
        } else
            CancelInvoke();
    }

    private void UpdateLifeTimeSlider() {
        lifeTimeSlider.value = lifeTime;
    }

    private void UpdateBallCountUI() {
        maxBallCountUI.text = "/ " + maxBallCount.ToString();
        currentBallCountUI.text = BallCount.ToString();
    }

    private void CreateBall(Vector2 spawnPoint) {
        GameObject go = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPoint, Quaternion.identity);
        if (go != null) {
            Ball ball = go.GetComponent<Ball>();
            balls.Add(ball);
        }
    }

    private void ReturnAllToPool() {
        foreach (Ball ball in balls) {
            EasyObjectPool.instance.ReturnObjectToPool(ball.gameObject);
        }
        balls.Clear();
    }

    public bool IsPointerOverUIObject() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
