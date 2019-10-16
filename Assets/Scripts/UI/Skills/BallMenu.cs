using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private const string maxValue = " (MAX)";

    struct TextFields {
        public TextMeshProUGUI upgradePriceUI;

        public TextMeshProUGUI currentQuantity;
        public TextMeshProUGUI currentDamageUI;
        public TextMeshProUGUI currentCritChanceUI;
        public TextMeshProUGUI currentCritMultiplierUI;

        public TextMeshProUGUI nextQuantity;
        public TextMeshProUGUI nextDamageUI;
        public TextMeshProUGUI nextCritChanceUI;
        public TextMeshProUGUI nextCritMultiplierUI;

        public TextMeshProUGUI extraBallCountUI;
        public TextMeshProUGUI extraDamageUI;
        public TextMeshProUGUI extraCritChanceUI;
        public TextMeshProUGUI extraCritDamageUI;
    }

    private MoveUI moveUI;
    private BallStats stats;
    private BallTypes ballTypes;
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextMeshProUGUI goldenPointsUI;
    private TextFields ui = new TextFields();

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        ballTypes = FindObjectOfType<BallTypes>();
        stats = ballTypes.GetBall(BallColor.BLUE);

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");
        goldenPointsUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/GoldenScore/Value");

        ui.upgradePriceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/Price/Value");

        ui.currentDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Damage/Value");
        ui.currentQuantity = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Quantity/Value");
        ui.currentCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Crit/Value");
        ui.currentCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/CritDamage/Value");

        ui.nextDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Damage/Value");
        ui.nextQuantity = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Quantity/Value");
        ui.nextCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Crit/Value");
        ui.nextCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/CritDamage/Value");

        ui.extraDamageUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/Damage/Value");
        ui.extraBallCountUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/Quantity/Value");
        ui.extraCritChanceUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/CritChance/Value");
        ui.extraCritDamageUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/CritDamage/Value");

        ballController = FindObjectOfType<BallController>();
        gameStateController = FindObjectOfType<GameStateController>();

        stats.level = PersistentData.instance.ballData.blueBallLevel;
        stats.extraBallCountLevel = PersistentData.instance.ballData.extraBallCountLevel;
        stats.extraDamageLevel = PersistentData.instance.ballData.extraDamageLevel;
        stats.extraCritChanceLevel = PersistentData.instance.ballData.extraCritChanceLevel;
        stats.extraCritDamageLevel = PersistentData.instance.ballData.extraCritDamageLevel;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void Start() {
        ballController.SetMaxBallCount(stats.Quantity);
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.blueBallLevel = stats.level;
        PersistentData.instance.ballData.extraBallCountLevel = stats.extraBallCountLevel;
        PersistentData.instance.ballData.extraDamageLevel = stats.extraDamageLevel;
        PersistentData.instance.ballData.extraCritChanceLevel = stats.extraCritChanceLevel;
        PersistentData.instance.ballData.extraCritDamageLevel = stats.extraCritDamageLevel;
    }

    private void UpdateUI() {
        scoreUI.text = Score.instance.score.ToString();
        goldenPointsUI.text = Score.instance.goldenPoints.ToString();

        ui.upgradePriceUI.text = stats.UpgradePrice.ToString();

        ui.currentQuantity.text = stats.level.ToString();
        ui.currentDamageUI.text = stats.BaseDamage.ToString();
        ui.currentCritChanceUI.text = stats.CritChance.ToString() + "%";
        ui.currentCritMultiplierUI.text = stats.CritDamageModifier.ToString() + "x";

        ui.nextQuantity.text = stats.NextQuantity.ToString();
        ui.nextDamageUI.text = stats.NextBaseDamage.ToString();
        ui.nextCritChanceUI.text = stats.NextCritChance.ToString() + "%";
        ui.nextCritMultiplierUI.text = stats.NextCritDamageModifier.ToString() + "x";

        ui.extraBallCountUI.text = stats.extraBallCountLevel.ToString();
        ui.extraDamageUI.text = stats.extraDamageLevel.ToString();
        ui.extraCritChanceUI.text = stats.extraCritChanceLevel.ToString();
        ui.extraCritDamageUI.text = stats.extraCritDamageLevel.ToString();
    }

    public void AddExtraBallOnClick() {
        if (Score.instance.PurchaseGoldenUpgrade(stats.CalcUpgradePriceExtraBallCount())) {
            stats.extraBallCountLevel++;
            ballController.SetMaxBallCount(stats.Quantity);
        }
        UpdateUI();
    }

    public void UpgradeBallsOnClick() {
        if (Score.instance.PurchaseUpgrade(stats.UpgradePrice)) {
            stats.level++;
            ballController.SetMaxBallCount(stats.Quantity);
        }
        UpdateUI();
    }

    public void BallMenuButtonOnClick() {
        gameStateController.ballMenuButtonPressed = true;
        UpdateUI();
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
