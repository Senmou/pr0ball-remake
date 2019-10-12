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

        public TextMeshProUGUI nextLevelUI;
        public TextMeshProUGUI nextDamageUI;
        public TextMeshProUGUI nextCritChanceUI;
        public TextMeshProUGUI nextCritMultiplierUI;
    }

    private MoveUI moveUI;
    private BallStats blueBall;
    private BallTypes ballTypes;
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextFields blueBallUI = new TextFields();

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        ballTypes = FindObjectOfType<BallTypes>();
        blueBall = ballTypes.GetBall(BallColor.BLUE);

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");

        blueBallUI.upgradePriceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/Price/Value");
        blueBallUI.currentDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Damage/Value");
        blueBallUI.currentQuantity = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Quantity/Value");
        blueBallUI.currentCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Crit/Value");
        blueBallUI.currentCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/CritMultiplier/Value");
        blueBallUI.nextDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Damage/Value");
        blueBallUI.nextLevelUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Quantity/Value");
        blueBallUI.nextCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Crit/Value");
        blueBallUI.nextCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/CritMultiplier/Value");

        ballController = FindObjectOfType<BallController>();
        gameStateController = FindObjectOfType<GameStateController>();

        blueBall.level = PersistentData.instance.ballData.blueBallLevel;
        ballController.SetMaxBallCount(blueBall.Quantity);

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.blueBallLevel = blueBall.level;
    }

    private void UpdateUI() {
        scoreUI.text = Score.instance.score.ToString();

        blueBallUI.upgradePriceUI.text = blueBall.UpgradePrice.ToString();

        blueBallUI.currentQuantity.text = blueBall.level.ToString();
        blueBallUI.currentDamageUI.text = blueBall.BaseDamage.ToString();
        blueBallUI.currentCritChanceUI.text = blueBall.CritChance.ToString() + "%";
        blueBallUI.currentCritMultiplierUI.text = blueBall.CritDamageMultiplier.ToString() + "x";

        blueBallUI.nextLevelUI.text = blueBall.NextLevel.ToString();
        blueBallUI.nextDamageUI.text = blueBall.NextBaseDamage.ToString();
        blueBallUI.nextCritChanceUI.text = blueBall.NextCritChance.ToString() + "%";
        blueBallUI.nextCritMultiplierUI.text = blueBall.NextCritDamageMultiplier.ToString() + "x";
    }

    public void UpgradeBlueBallButtonOnClick() {
        if (Score.instance.PurchaseUpgrade(blueBall.UpgradePrice)) {
            blueBall.level++;
            ballController.SetMaxBallCount(blueBall.Quantity);
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
