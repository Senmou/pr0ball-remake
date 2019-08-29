using System.Collections.Generic;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;
using System.Collections;

public class BallController : MonoBehaviour {

    [HideInInspector] public string poolName = "BallPool";

    public Slider lifeTimeSlider;
    public Transform spawnPoint;
    public TextMeshProUGUI maxBallCountUI;
    public TextMeshProUGUI currentBallCountUI;

    public bool canShootAgain;

    private int maxBallCount = 1;
    private float maxLifeTime = 8f;
    private float shootingRate = 0.1f;

    public int BallCount { get => balls.Count; }
    public int MaxBallCount { get => maxBallCount; }
    public bool LifeTimeExeeded { get => lifeTime <= 0f; }
    public bool AllBallsShot { get => BallCount == MaxBallCount; }

    private float lifeTime;
    private List<Ball> balls = new List<Ball>();

    private void Start() {
        InitData();
        UpdateBallCountUI();
    }

    public void OnCycleFinish(PlayStateController controller) {
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
        canShootAgain = false;
        InvokeRepeating("ShootBall", 0f, shootingRate);
    }

    private IEnumerator CollectBalls(PlayStateController controller) {

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
        canShootAgain = BallCount == 0;
        UpdateBallCountUI();
    }

    private void InitData() {
        canShootAgain = true;
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
        currentBallCountUI.text = BallCount.ToString();
        maxBallCountUI.text = "/ " + maxBallCount.ToString();
    }

    private void CreateBall(Vector2 spawnPoint) {
        GameObject go = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPoint, Quaternion.identity);
        if (go != null) {
            Ball ball = go.GetComponent<Ball>();
            balls.Add(ball);
        }
    }
}
