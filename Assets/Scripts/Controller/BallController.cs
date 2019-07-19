using System.Collections.Generic;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour {

    public Slider lifeTimeSlider;
    public Transform spawnPoint;
    public TextMeshProUGUI maxBallCountUI;
    public TextMeshProUGUI currentBallCountUI;

    private int maxBallCount = 20;
    private float maxLifeTime = 5f;
    private float shootingRate = 0.1f;

    private int BallCount { get => balls.Count; }
    public bool LifeTimeExeeded { get => lifeTime <= 0f; }
    public bool AllBallsShot { get => BallCount == maxBallCount; }

    private float lifeTime;
    private string poolName = "BallPool";
    private List<Ball> balls = new List<Ball>();

    private void Start() {
        InitBallPool();
        OnCycleFinish();
    }

    public void OnCycleFinish() {
        ReturnAllToPool();
        lifeTime = maxLifeTime;
        lifeTimeSlider.maxValue = maxLifeTime;
        UpdateLifeTimeSlider();
    }

    public void DrainLifeTime() {
        lifeTime -= Time.deltaTime;
        UpdateLifeTimeSlider();
    }

    public void Shoot() {
        InvokeRepeating("ShootBall", 0f, shootingRate);
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

    private void InitBallPool() {
        for (int i = 0; i < 20; i++) {
            CreateBall(Vector2.zero);
        }
        ReturnAllToPool();
    }

    private void CreateBall(Vector2 spawnPoint) {
        Ball ball = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPoint, Quaternion.identity).GetComponent<Ball>();
        if (ball)
            balls.Add(ball);
    }

    private void ReturnAllToPool() {
        foreach (Ball ball in balls) {
            EasyObjectPool.instance.ReturnObjectToPool(ball.gameObject);
        }
        balls.Clear();
    }
}
