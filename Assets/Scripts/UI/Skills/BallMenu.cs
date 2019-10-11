using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private const string maxValue = " (MAX)";

    struct TextFields {
        public TextMeshProUGUI upgradePriceUI;

        public TextMeshProUGUI currentLevelUI;
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
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;
    private TextFields blueBallUI = new TextFields();

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        blueBall = BallTypes.instance.GetBall(BallColor.BLUE);

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");

        blueBallUI.upgradePriceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/Price/Value");
        blueBallUI.currentLevelUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Level/Value");
        blueBallUI.currentDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Damage/Value");
        blueBallUI.currentCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/Crit/Value");
        blueBallUI.currentCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/CurrentStats/CritMultiplier/Value");
        blueBallUI.nextLevelUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Level/Value");
        blueBallUI.nextDamageUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Damage/Value");
        blueBallUI.nextCritChanceUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/Crit/Value");
        blueBallUI.nextCritMultiplierUI = transform.FindChild<TextMeshProUGUI>("BlueBall/NextLevelStats/CritMultiplier/Value");

        gameStateController = FindObjectOfType<GameStateController>();

        blueBall.level = PersistentData.instance.ballData.blueBallLevel;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.blueBallLevel = blueBall.level;
    }

    private void UpdateUI() {
        scoreUI.text = Score.instance.score.ToString();

        blueBallUI.upgradePriceUI.text = blueBall.UpgradePrice.ToString();

        blueBallUI.currentLevelUI.text = blueBall.level.ToString();
        blueBallUI.currentDamageUI.text = blueBall.BaseDamage.ToString();
        blueBallUI.currentCritChanceUI.text = blueBall.CritChance.ToString() + "%";
        blueBallUI.currentCritMultiplierUI.text = blueBall.CritDamageMultiplier.ToString() + "x";

        blueBallUI.nextLevelUI.text = blueBall.NextLevel.ToString();
        blueBallUI.nextDamageUI.text = blueBall.NextBaseDamage.ToString();
        blueBallUI.nextCritChanceUI.text = blueBall.NextCritChance.ToString() + "%";
        blueBallUI.nextCritMultiplierUI.text = blueBall.NextCritDamageMultiplier.ToString() + "x";
    }

    private void Update() {
        UpdateUI();
    }

    public void UpgradeBlueBallButtonOnClick() {
        if (Score.instance.PurchaseUpgrade(blueBall.UpgradePrice)) {
            blueBall.level++;
        }
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
