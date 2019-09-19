using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private MoveUI moveUI;
    private TextMeshProUGUI damage;
    private TextMeshProUGUI critChance;
    private TextMeshProUGUI critMultiplier;
    private GameStateController gameStateController;
    private TextMeshProUGUI blueBallSpawnChanceUI;
    private TextMeshProUGUI greenBallSpawnChanceUI;
    private TextMeshProUGUI orangeBallSpawnChanceUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        damage = transform.FindChild<TextMeshProUGUI>("Damage/Value");
        critChance = transform.FindChild<TextMeshProUGUI>("Crit/Value");
        critMultiplier = transform.FindChild<TextMeshProUGUI>("CritMultiplier/Value");
        gameStateController = FindObjectOfType<GameStateController>();

        blueBallSpawnChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/SpawnChance");
        greenBallSpawnChanceUI = transform.FindChild<TextMeshProUGUI>("GreenBall/SpawnChance");
        orangeBallSpawnChanceUI = transform.FindChild<TextMeshProUGUI>("OrangeBall/SpawnChance");
    }

    private void UpdateUI() {
        blueBallSpawnChanceUI.text = BallTypes.instance.GetBallStats(BallType.BLUE).spawnChance + "%";
        greenBallSpawnChanceUI.text = BallTypes.instance.GetBallStats(BallType.GREEN).spawnChance + "%";
        orangeBallSpawnChanceUI.text = BallTypes.instance.GetBallStats(BallType.ORANGE).spawnChance + "%";
    }

    private void Update() {
        UpdateUI();
    }

    public void BallMenuButtonOnClick() {
        gameStateController.ballMenuButtonPressed = true;
    }

    public void CloseButtonOnClick() {
        gameStateController.ballMenuCloseButtonPressed = true;
    }

    public void Show() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
        gameStateController.optionsButtonPressed = false;
    }
}
