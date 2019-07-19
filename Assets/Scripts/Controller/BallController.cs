using System.Collections.Generic;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
