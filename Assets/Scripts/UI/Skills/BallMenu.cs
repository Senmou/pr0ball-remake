using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private MoveUI moveUI;
    private BallStats ballStats;
    private TextMeshProUGUI damage;
    private TextMeshProUGUI critChance;
    private TextMeshProUGUI critMultiplier;

    private GameStateController gameStateController;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        ballStats = FindObjectOfType<BallStats>();
        damage = transform.FindChild<TextMeshProUGUI>("Damage/Value");
        critChance = transform.FindChild<TextMeshProUGUI>("Crit/Value");
        critMultiplier = transform.FindChild<TextMeshProUGUI>("CritMultiplier/Value");
        gameStateController = FindObjectOfType<GameStateController>();
    }

    private void UpdateUI() {
        damage.text = ballStats.Damage().ToString();
        critChance.text = ballStats.CritChance.ToString();
        critMultiplier.text = ballStats.CritDamageMultiplier.ToString();
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
