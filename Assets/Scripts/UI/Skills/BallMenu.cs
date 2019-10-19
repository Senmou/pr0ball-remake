using UnityEngine;
using TMPro;

public class BallMenu : MonoBehaviour {

    private TextMeshProUGUI upgradePriceUI;

    private TextMeshProUGUI upgradeExtraDamagePriceUI;
    private TextMeshProUGUI upgradeExtraCritChancePriceUI;
    private TextMeshProUGUI upgradeExtraCritDamagePriceUI;
    private TextMeshProUGUI upgradeExtraBallCountPriceUI;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI critChanceUI;
    private TextMeshProUGUI critDamageUI;
    private TextMeshProUGUI ballCountUI;

    private TextMeshProUGUI upgradeDamageUI;
    private TextMeshProUGUI upgradeCritChanceUI;
    private TextMeshProUGUI upgradeCritDamageUI;
    private TextMeshProUGUI upgradeBallCountUI;

    private TextMeshProUGUI extraDamageUI;
    private TextMeshProUGUI extraCritChanceUI;
    private TextMeshProUGUI extraCritDamageUI;
    private TextMeshProUGUI extraBallCountUI;

    private TextMeshProUGUI upgradeExtraDamageUI;
    private TextMeshProUGUI upgradeExtraCritChanceUI;
    private TextMeshProUGUI upgradeExtraCritDamageUI;
    private TextMeshProUGUI upgradeExtraBallCountUI;

    private TextMeshProUGUI totalDamageUI;
    private TextMeshProUGUI totalCritChanceUI;
    private TextMeshProUGUI totalCritDamageUI;
    private TextMeshProUGUI totalBallCountUI;

    private MoveUI moveUI;
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextMeshProUGUI extraScoreUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");
        extraScoreUI = transform.FindChild<TextMeshProUGUI>("ExtraScore/Value");

        upgradePriceUI = transform.FindChild<TextMeshProUGUI>("UpgradeButton/Price/Value");

        upgradeExtraDamagePriceUI = transform.FindChild<TextMeshProUGUI>("ExtraUpgradeButtons/Damage/Price");
        upgradeExtraCritChancePriceUI = transform.FindChild<TextMeshProUGUI>("ExtraUpgradeButtons/CritChance/Price");
        upgradeExtraCritDamagePriceUI = transform.FindChild<TextMeshProUGUI>("ExtraUpgradeButtons/CritDamage/Price");
        upgradeExtraBallCountPriceUI = transform.FindChild<TextMeshProUGUI>("ExtraUpgradeButtons/BallCount/Price");

        damageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/Damage/Value");
        critChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritChance/Value");
        critDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritDamage/Value");
        ballCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/BallCount/Value");

        extraDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentExtraStats/Damage/Value");
        extraCritChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentExtraStats/CritChance/Value");
        extraCritDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentExtraStats/CritDamage/Value");
        extraBallCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentExtraStats/BallCount/Value");

        upgradeDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/Damage/Value");
        upgradeCritChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/CritChance/Value");
        upgradeCritDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/CritDamage/Value");
        upgradeBallCountUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/BallCount/Value");

        upgradeExtraDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeExtraStats/Damage/Value");
        upgradeExtraCritChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeExtraStats/CritChance/Value");
        upgradeExtraCritDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeExtraStats/CritDamage/Value");
        upgradeExtraBallCountUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeExtraStats/BallCount/Value");

        totalDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/TotalCurrentStats/Damage/Value");
        totalCritChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/TotalCurrentStats/CritChance/Value");
        totalCritDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/TotalCurrentStats/CritDamage/Value");
        totalBallCountUI = transform.FindChild<TextMeshProUGUI>("Stats/TotalCurrentStats/BallCount/Value");

        ballController = FindObjectOfType<BallController>();
        gameStateController = FindObjectOfType<GameStateController>();

        BallStats.Instance.level = PersistentData.instance.ballData.level;
        BallStats.Instance.extraBallCountLevel = PersistentData.instance.ballData.extraBallCountLevel;
        BallStats.Instance.extraDamageLevel = PersistentData.instance.ballData.extraDamageLevel;
        BallStats.Instance.extraCritChanceLevel = PersistentData.instance.ballData.extraCritChanceLevel;
        BallStats.Instance.extraCritDamageLevel = PersistentData.instance.ballData.extraCritDamageLevel;

        BallStats.Instance.damage = PersistentData.instance.ballData.damage;
        BallStats.Instance.critChance = PersistentData.instance.ballData.critChance;
        BallStats.Instance.critDamage = PersistentData.instance.ballData.critDamage;
        BallStats.Instance.ballCount = PersistentData.instance.ballData.ballCount;

        BallStats.Instance.extraDamage = PersistentData.instance.ballData.extraDamage;
        BallStats.Instance.extraCritChance = PersistentData.instance.ballData.extraCritChance;
        BallStats.Instance.extraCritDamage = PersistentData.instance.ballData.extraCritDamage;
        BallStats.Instance.extraBallCount = PersistentData.instance.ballData.extraBallCount;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void Start() {
        ballController.SetMaxBallCount(BallStats.Instance.TotalBallCount);
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.level = BallStats.Instance.level;
        PersistentData.instance.ballData.extraBallCountLevel = BallStats.Instance.extraBallCountLevel;
        PersistentData.instance.ballData.extraDamageLevel = BallStats.Instance.extraDamageLevel;
        PersistentData.instance.ballData.extraCritChanceLevel = BallStats.Instance.extraCritChanceLevel;
        PersistentData.instance.ballData.extraCritDamageLevel = BallStats.Instance.extraCritDamageLevel;

        PersistentData.instance.ballData.damage = BallStats.Instance.damage;
        PersistentData.instance.ballData.critChance = BallStats.Instance.critChance;
        PersistentData.instance.ballData.critDamage = BallStats.Instance.critDamage;
        PersistentData.instance.ballData.ballCount = BallStats.Instance.ballCount;

        PersistentData.instance.ballData.extraDamage = BallStats.Instance.extraDamage;
        PersistentData.instance.ballData.extraCritChance = BallStats.Instance.extraCritChance;
        PersistentData.instance.ballData.extraCritDamage = BallStats.Instance.extraCritDamage;
        PersistentData.instance.ballData.extraBallCount = BallStats.Instance.extraBallCount;
    }

    private void UpdateUI() {
        scoreUI.text = Score.instance.score.ToStringFormatted();
        extraScoreUI.text = Score.instance.extraScore.ToStringFormatted();

        // Stats
        upgradePriceUI.text = BallStats.Instance.UpgradePrice.ToString();

        damageUI.text = BallStats.Instance.damage.ToString();
        critChanceUI.text = BallStats.Instance.critChance.ToString("0.00") + "%";
        critDamageUI.text = BallStats.Instance.critDamage.ToString("0.00") + "x";
        ballCountUI.text = BallStats.Instance.ballCount.ToString();

        upgradeDamageUI.text = "(+" + BallStats.Instance.UpgradeDamage.ToString() + ")";
        upgradeCritChanceUI.text = "(+" + BallStats.Instance.UpgradeCritChance.ToString("0.00") + "%)";
        upgradeCritDamageUI.text = "(+" + BallStats.Instance.UpgradeCritDamage.ToString("0.00") + "x)";
        upgradeBallCountUI.text = "(+" + BallStats.Instance.UpgradeBallCount.ToString() + ")";

        // Extra stats
        extraDamageUI.text = BallStats.Instance.extraDamage.ToString();
        extraCritChanceUI.text = BallStats.Instance.extraCritChance.ToString("0.00") + "%";
        extraCritDamageUI.text = BallStats.Instance.extraCritDamage.ToString("0.00") + "x";
        extraBallCountUI.text = BallStats.Instance.extraBallCount.ToString();

        upgradeExtraDamageUI.text = "(+" + BallStats.Instance.UpgradeExtraDamage.ToString() + ")";
        upgradeExtraCritChanceUI.text = "(+" + BallStats.Instance.UpgradeExtraCritChance.ToString("0.00") + "%)";
        upgradeExtraCritDamageUI.text = "(+" + BallStats.Instance.UpgradeExtraCritDamage.ToString("0.00") + "x)";
        upgradeExtraBallCountUI.text = "(+" + BallStats.Instance.UpgradeExtraBallCount.ToString() + ")";

        upgradeExtraDamagePriceUI.text = BallStats.Instance.ExtraDamageUpgradePrice.ToString();
        upgradeExtraCritChancePriceUI.text = BallStats.Instance.ExtraCritChanceUpgradePrice.ToString();
        upgradeExtraCritDamagePriceUI.text = BallStats.Instance.ExtraCritDamageUpgradePrice.ToString();
        upgradeExtraBallCountPriceUI.text = BallStats.Instance.ExtraBallCountUpgradePrice.ToString();

        // Total stats
        totalDamageUI.text = BallStats.Instance.TotalDamage.ToString();
        totalCritChanceUI.text = BallStats.Instance.TotalCritChance.ToString("0.00") + "%";
        totalCritDamageUI.text = BallStats.Instance.TotalCritDamage.ToString("0.00") + "x";
        totalBallCountUI.text = BallStats.Instance.TotalBallCount.ToString();
    }

    public void AddExtraDamageOnClick() {
        if (Score.instance.PurchaseExtraUpgrade(BallStats.Instance.ExtraDamageUpgradePrice))
            BallStats.Instance.AddExtraDamage();
        UpdateUI();
    }

    public void AddExtraCritChanceOnClick() {
        if (Score.instance.PurchaseExtraUpgrade(BallStats.Instance.ExtraCritChanceUpgradePrice))
            BallStats.Instance.AddExtraCritChance();
        UpdateUI();
    }

    public void AddExtraCritDamageOnClick() {
        if (Score.instance.PurchaseExtraUpgrade(BallStats.Instance.ExtraCritDamageUpgradePrice))
            BallStats.Instance.AddExtraCritDamage();
        UpdateUI();
    }

    public void AddExtraBallCountOnClick() {
        if (Score.instance.PurchaseExtraUpgrade(BallStats.Instance.ExtraBallCountUpgradePrice)) {
            BallStats.Instance.AddExtraBallCount();
            ballController.SetMaxBallCount(BallStats.Instance.TotalBallCount);
        }
        UpdateUI();
    }

    public void UpgradeBallsOnClick() {
        if (Score.instance.PurchaseUpgrade(BallStats.Instance.UpgradePrice)) {
            BallStats.Instance.AddStats();
            ballController.SetMaxBallCount(BallStats.Instance.TotalBallCount);
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

    public void ResetData() {
        BallStats.Instance.ResetStats();
        UpdateUI();
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
