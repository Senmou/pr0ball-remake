using System.Collections.Generic;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour {

    public Slider lifeTimeSlider;
    public Transform ballSpawnPoint;
    public TextMeshProUGUI maxBallCountUI;
    public TextMeshProUGUI currentBallCountUI;

    private int currentBallCount;
    private int maxBallCount = 20;
    private float maxLifeTime = 5f;

    public bool LifeTimeExeeded { get => lifeTime <= 0f; }
    public bool AllBallsShot { get => currentBallCount == maxBallCount; }

    private float lifeTime;
    private string poolName = "BallPool";
    private List<Ball> balls = new List<Ball>();

    private void Start() {
        InitBallPool();
        OnCycleFinish();
    }

    public void OnCycleFinish() {
        currentBallCount = 0;
        lifeTime = maxLifeTime;
        lifeTimeSlider.maxValue = maxLifeTime;
        UpdateLifeTimeSlider();
    }

    public void DrainLifeTime() {
        lifeTime -= Time.deltaTime;
        UpdateLifeTimeSlider();
    }

    public void Shoot() {
        InvokeRepeating("ShootBall", 0f, 0.2f);
    }

    private void ShootBall() {
        if (currentBallCount < maxBallCount) {
            currentBallCount++;
            UpdateBallCountUI();
        } else
            CancelInvoke();
    }

    private void UpdateLifeTimeSlider() {
        lifeTimeSlider.value = lifeTime;
    }

    private void UpdateBallCountUI() {
        maxBallCountUI.text = "/ " + maxBallCount.ToString();
        currentBallCountUI.text = currentBallCount.ToString();
    }

    private void InitBallPool() {
        for (int i = 0; i < 20; i++) {
            CreateBall();
        }
        ReturnAllToPool();
    }

    private void CreateBall() {
        Ball ball = EasyObjectPool.instance.GetObjectFromPool(poolName, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
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
