using TMPro;
using UnityEngine;

public class SimpleStatisticsMenu : CanvasController {

    private Statistics statistics;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI ballCountUI;
    private TextMeshProUGUI critChanceUI;
    private TextMeshProUGUI critDamageUI;

    private PauseBackground pauseBackground;

    private void Awake() {
        statistics = null;
        pauseBackground = FindObjectOfType<PauseBackground>();
        damageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/Damage/Value");
        ballCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/BallCount/Value");
        critChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritChance/Value");
        critDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritDamage/Value");

    }

    public void UpdateStatisticsUI(Statistics statistics) {
        damageUI.text = statistics.balls.damage.ToString();
        critChanceUI.text = statistics.balls.critChance.ToString("0") + "%";
        critDamageUI.text = statistics.balls.critDamage.ToString("0.##") + "x";
        ballCountUI.text = statistics.balls.ballCount.ToString();
    }

    public void SetStatistics(Statistics _statistics) {
        statistics = _statistics;
        UpdateStatisticsUI(statistics);
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        LeanTween.move(gameObject, Vector3.zero, 0.15f)
           .setOnStart(() => gameObject.SetActive(true))
           .setIgnoreTimeScale(true)
           .setEase(showEaseType);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        LeanTween.move(gameObject, new Vector2(0f, -35f), 0.15f)
             .setIgnoreTimeScale(true)
             .setEase(hideEaseType)
             .setOnComplete(() => gameObject.SetActive(false));
    }
}
