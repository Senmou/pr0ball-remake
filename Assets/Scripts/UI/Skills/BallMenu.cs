using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private TextMeshProUGUI upgradePriceUI;

    private TextMeshProUGUI currentQuantity;
    private TextMeshProUGUI currentDamageUI;
    private TextMeshProUGUI currentCritChanceUI;
    private TextMeshProUGUI currentCritMultiplierUI;

    private TextMeshProUGUI nextQuantity;
    private TextMeshProUGUI nextDamageUI;
    private TextMeshProUGUI nextCritChanceUI;
    private TextMeshProUGUI nextCritMultiplierUI;

    private TextMeshProUGUI extraBallCountUI;
    private TextMeshProUGUI extraDamageUI;
    private TextMeshProUGUI extraCritChanceUI;
    private TextMeshProUGUI extraCritDamageUI;

    private MoveUI moveUI;
    private BallStats stats;
    private BallTypes ballTypes;
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextMeshProUGUI goldenPointsUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        ballTypes = FindObjectOfType<BallTypes>();
        stats = ballTypes.GetBall(BallColor.BLUE);

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");
        goldenPointsUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/GoldenScore/Value");

        upgradePriceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/Price/Value");

        currentDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Damage/Value");
        currentQuantity = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Quantity/Value");
        currentCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Crit/Value");
        currentCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/CritDamage/Value");

        nextDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Damage/Value");
        nextQuantity = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Quantity/Value");
        nextCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Crit/Value");
        nextCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/CritDamage/Value");

        extraDamageUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/Damage/Value");
        extraBallCountUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/Quantity/Value");
        extraCritChanceUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/CritChance/Value");
        extraCritDamageUI = transform.FindChild<TextMeshProUGUI>("GoldenStats/CurrentValues/CritDamage/Value");

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

        upgradePriceUI.text = stats.UpgradePrice.ToString();

        currentQuantity.text = stats.level.ToString();
        currentDamageUI.text = stats.BaseDamage.ToString();
        currentCritChanceUI.text = stats.CritChance.ToString() + "%";
        currentCritMultiplierUI.text = stats.CritDamageModifier.ToString() + "x";

        nextQuantity.text = stats.NextQuantity.ToString();
        nextDamageUI.text = stats.NextBaseDamage.ToString();
        nextCritChanceUI.text = stats.NextCritChance.ToString() + "%";
        nextCritMultiplierUI.text = stats.NextCritDamageModifier.ToString() + "x";

        extraBallCountUI.text = stats.extraBallCountLevel.ToString();
        extraDamageUI.text = stats.extraDamageLevel.ToString();
        extraCritChanceUI.text = stats.extraCritChanceLevel.ToString();
        extraCritDamageUI.text = stats.extraCritDamageLevel.ToString();
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
