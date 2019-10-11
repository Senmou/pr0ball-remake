using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using MarchingBytes;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour {

    public LootDropTable ballLDT;

    [HideInInspector] public bool canShootAgain;

    private Transform spawnPoint;
    private Slider lifeTimeSlider;
    private TextMeshProUGUI maxBallCountUI;
    private TextMeshProUGUI currentBallCountUI;

    private float lifeTime;
    private int maxBallCount = 1;
    private float maxLifeTime = 2f;
    private float shootingRate = 0.1f;

    private List<Ball> balls = new List<Ball>();

    public int BallCount { get => balls.Count; }
    public int MaxBallCount { get => maxBallCount; }
    public bool LifeTimeExeeded { get => lifeTime <= 0f; }
    public bool AllBallsShot { get => BallCount == MaxBallCount; }
    
    private void OnValidate() {
        ballLDT.ValidateTable();
    }

    private void Awake() {

        spawnPoint = GameObject.Find("BallSpawnPoint").transform;
        lifeTimeSlider = GameObject.Find("LifeTimeSlider").GetComponent<Slider>();
        maxBallCountUI = GameObject.Find("MaxBallCount").GetComponent<TextMeshProUGUI>();
        currentBallCountUI = GameObject.Find("CurrentBallCount").GetComponent<TextMeshProUGUI>();

        SetLDIWeights();
        ballLDT.ValidateTable();
    }

    private void SetLDIWeights() {
        ballLDT.SetWeight("GreenBallPool", BallTypes.instance.GetBall(BallColor.GREEN).spawnChance);
        ballLDT.SetWeight("OrangeBallPool", BallTypes.instance.GetBall(BallColor.ORANGE).spawnChance);

        float blueBallSpawnChance = 100f - BallTypes.instance.GetBall(BallColor.GREEN).spawnChance - BallTypes.instance.GetBall(BallColor.ORANGE).spawnChance;
        ballLDT.SetWeight("BlueBallPool", blueBallSpawnChance);
    }

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

    public void CancelShooting() {
        CancelInvoke("ShootBall");
    }

    public void Shoot() {
        canShootAgain = false;
        InvokeRepeating("ShootBall", 0f, shootingRate);
    }

    private IEnumerator CollectBalls(PlayStateController controller) {

        Ball[] temp = new Ball[maxBallCount];
        balls.CopyTo(temp);

        foreach (Ball ball in temp) {
            if (ball == null) continue;
            ball.DisableCollider();
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

        string sourcePool = ballLDT.PickLootDropItem().poolName;
        GameObject go = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoint, Quaternion.identity);

        if (go != null) {
            Ball ball = go.GetComponent<Ball>();
            balls.Add(ball);
        }
    }
}
