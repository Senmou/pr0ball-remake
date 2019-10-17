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
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextMeshProUGUI goldenPointsUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();

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

        BallStats.Instance.level = PersistentData.instance.ballData.blueBallLevel;
        BallStats.Instance.extraBallCountLevel = PersistentData.instance.ballData.extraBallCountLevel;
        BallStats.Instance.extraDamageLevel = PersistentData.instance.ballData.extraDamageLevel;
        BallStats.Instance.extraCritChanceLevel = PersistentData.instance.ballData.extraCritChanceLevel;
        BallStats.Instance.extraCritDamageLevel = PersistentData.instance.ballData.extraCritDamageLevel;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void Start() {
        ballController.SetMaxBallCount(BallStats.Instance.Quantity);
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.blueBallLevel = BallStats.Instance.level;
        PersistentData.instance.ballData.extraBallCountLevel = BallStats.Instance.extraBallCountLevel;
        PersistentData.instance.ballData.extraDamageLevel = BallStats.Instance.extraDamageLevel;
        PersistentData.instance.ballData.extraCritChanceLevel = BallStats.Instance.extraCritChanceLevel;
        PersistentData.instance.ballData.extraCritDamageLevel = BallStats.Instance.extraCritDamageLevel;
    }

    private void UpdateUI() {
        scoreUI.text = Score.instance.score.ToString();
        goldenPointsUI.text = Score.instance.goldenPoints.ToString();

        upgradePriceUI.text = BallStats.Instance.UpgradePrice.ToString();

        currentQuantity.text = BallStats.Instance.level.ToString();
        currentDamageUI.text = BallStats.Instance.BaseDamage.ToString();
        currentCritChanceUI.text = BallStats.Instance.CritChance.ToString() + "%";
        currentCritMultiplierUI.text = BallStats.Instance.CritDamageModifier.ToString() + "x";

        nextQuantity.text = BallStats.Instance.NextQuantity.ToString();
        nextDamageUI.text = BallStats.Instance.NextBaseDamage.ToString();
        nextCritChanceUI.text = BallStats.Instance.NextCritChance.ToString() + "%";
        nextCritMultiplierUI.text = BallStats.Instance.NextCritDamageModifier.ToString() + "x";

        extraBallCountUI.text = BallStats.Instance.extraBallCountLevel.ToString();
        extraDamageUI.text = BallStats.Instance.extraDamageLevel.ToString();
        extraCritChanceUI.text = BallStats.Instance.extraCritChanceLevel.ToString();
        extraCritDamageUI.text = BallStats.Instance.extraCritDamageLevel.ToString();
    }

    public void AddExtraBallOnClick() {
        if (Score.instance.PurchaseGoldenUpgrade(BallStats.Instance.CalcUpgradePriceExtraBallCount())) {
            BallStats.Instance.extraBallCountLevel++;
            ballController.SetMaxBallCount(BallStats.Instance.Quantity);
        }
        UpdateUI();
    }

    public void UpgradeBallsOnClick() {
        if (Score.instance.PurchaseUpgrade(BallStats.Instance.UpgradePrice)) {
            BallStats.Instance.level++;
            ballController.SetMaxBallCount(BallStats.Instance.Quantity);
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
