using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public Transform ballSpawnPoint;
    public TextMeshProUGUI maxBallCountUI;
    public TextMeshProUGUI currentBallCountUI;
    public Slider lifeTimeSlider;

    public int maxBallCount;
    public int currentBallCount;
    public float maxBallLifeTime;

    private float ballLifeTime;

    private bool AllBallsShot {
        get => currentBallCount == maxBallCount;
    }

    private void Start() {
        lifeTimeSlider.maxValue = maxBallLifeTime;
        lifeTimeSlider.value = lifeTimeSlider.maxValue;
        ballLifeTime = maxBallLifeTime;
    }

    private void Update() {
        maxBallCountUI.text = "/ " + maxBallCount.ToString();
        currentBallCountUI.text = currentBallCount.ToString();

        if (Input.GetMouseButtonUp(0)) {
            Shoot();
        }

        if (AllBallsShot) {
            ballLifeTime -= Time.deltaTime;

            if(ballLifeTime <= 0f) {
                Debug.Log("Ball life time exeeded");
                currentBallCount = 0;
                ballLifeTime = maxBallLifeTime;
            }
        }

        lifeTimeSlider.value = ballLifeTime;
    }

    public void Shoot() {
        InvokeRepeating("ShootBall", 0f, 0.2f);
    }

    private void ShootBall() {
        if (currentBallCount < maxBallCount) {

            currentBallCount++;
        } else
            CancelInvoke();
    }
}
